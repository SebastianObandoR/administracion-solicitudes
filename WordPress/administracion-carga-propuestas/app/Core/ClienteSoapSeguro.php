<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

final class ClienteSoapSeguro
{
    private const MODELOS = 'http://schemas.datacontract.org/2004/07/AdministracionSolicitudes.WebService.Modelos';

    public function ejecutar(string $operacion, array $argumentos): object
    {
        if (!function_exists('publicasa_wcf_post')) {
            throw new \RuntimeException('Activa el plugin de solicitudes que define publicasa_wcf_post().');
        }

        $parametros = $argumentos[0] ?? [];
        $xml = '';

        foreach ($parametros as $nombre => $valor) {
            $xml .= '<' . $nombre . '>';

            if ($nombre === 'filas') {
                foreach ($valor['FilaDesgloseExcel'] as $fila) {
                    $xml .= '<m:FilaDesgloseExcel xmlns:m="' . self::MODELOS . '">';
                    foreach (['Anio', 'Horas', 'Iva', 'Mes', 'Monto', 'NumeroFila',
                              'Observaciones', 'PorcentajeCobro', 'Total'] as $campo) {
                        $xml .= '<m:' . $campo . '>'
                            . $this->escapar((string) ($fila[$campo] ?? ''))
                            . '</m:' . $campo . '>';
                    }
                    $xml .= '</m:FilaDesgloseExcel>';
                }
            } else {
                $xml .= $this->escapar((string) $valor);
            }

            $xml .= '</' . $nombre . '>';
        }

        $respuesta = \publicasa_wcf_post($operacion, $xml);
        if (is_wp_error($respuesta)) {
            throw new \RuntimeException($respuesta->get_error_message());
        }

        if (!is_string($respuesta) || stripos($respuesta, '<!DOCTYPE') !== false) {
            throw new \RuntimeException('Respuesta WCF no válida.');
        }

        $documento = @simplexml_load_string($respuesta, \SimpleXMLElement::class, LIBXML_NONET);
        if ($documento === false) {
            throw new \RuntimeException('Respuesta XML de WCF no válida.');
        }

        $documento->registerXPathNamespace('s', 'http://schemas.xmlsoap.org/soap/envelope/');
        $documento->registerXPathNamespace('w', 'http://tempuri.org/');
        $nodos = $documento->xpath('/s:Envelope/s:Body/w:' . $operacion . 'Response/w:' . $operacion . 'Result');
        if (count($nodos ?: []) !== 1) {
            throw new \RuntimeException('La respuesta WCF no contiene el resultado esperado.');
        }

        return (object) [$operacion . 'Result' => $this->valor($nodos[0])];
    }

    private function escapar(string $valor): string
    {
        return htmlspecialchars($valor, ENT_XML1 | ENT_QUOTES | ENT_SUBSTITUTE, 'UTF-8');
    }

    private function valor(\SimpleXMLElement $nodo)
    {
        $nil = (string) $nodo->attributes('http://www.w3.org/2001/XMLSchema-instance')->nil;
        if ($nil === 'true' || $nil === '1') {
            return null;
        }

        $hijos = $nodo->xpath('./*') ?: [];
        if ($hijos === []) {
            $valor = (string) $nodo;
            if (in_array($nodo->getName(), [
                'EsCorrecto', 'RegistrarErrorImportacionResult', 'RegistrarErrorConsultaParticipacionesResult'
            ], true)) {
                if (!in_array($valor, ['true', 'false', '1', '0'], true)) {
                    throw new \RuntimeException('Valor booleano de WCF no válido.');
                }

                return $valor === 'true' || $valor === '1';
            }

            return $valor;
        }

        $resultado = new \stdClass();
        foreach ($hijos as $hijo) {
            $nombre = $hijo->getName();
            $valor = $this->valor($hijo);
            if (!property_exists($resultado, $nombre)) {
                $resultado->$nombre = $valor;
            } elseif (is_array($resultado->$nombre)) {
                $resultado->{$nombre}[] = $valor;
            } else {
                $resultado->$nombre = [$resultado->$nombre, $valor];
            }
        }

        return $resultado;
    }
}
