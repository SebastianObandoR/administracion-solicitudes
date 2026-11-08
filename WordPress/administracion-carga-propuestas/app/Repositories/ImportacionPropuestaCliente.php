<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Repositories;

use PublicaSA\CargaPropuestas\Core\ClienteSoapSeguro;

final class ImportacionPropuestaCliente
{

    private function wsdl(): string
    {
        return defined('PUBLICASA_SOLICITUDES_WSDL')
            ? PUBLICASA_SOLICITUDES_WSDL
            : 'http://localhost:56784/ServicioSolicitudes.svc?wsdl';
    }

    public function cargar(array $filas): array
    {
        $respuesta = (new ClienteSoapSeguro())->ejecutar(
            $this->wsdl(),
            'CargarDesglosesPropuesta',
            [['filas' => ['FilaDesgloseExcel' => $filas]]]
        );
        $resultado = $respuesta->CargarDesglosesPropuestaResult ?? null;
        if (!is_object($resultado) || !isset($resultado->EsCorrecto)) {
            throw new \RuntimeException('Respuesta WCF no válida.');
        }

        $errores = $this->lista($resultado->Errores->string ?? []);
        $desgloses = $this->lista($resultado->Desgloses->FilaDesgloseExcel ?? []);
        return [
            'correcto' => (bool) $resultado->EsCorrecto,
            'errores' => $errores,
            'desgloses' => $desgloses
        ];
    }

    public function registrarError(string $referencia, string $detalle): bool
    {
        $respuesta = (new ClienteSoapSeguro())->ejecutar(
            $this->wsdl(),
            'RegistrarErrorImportacion',
            [['referencia' => $referencia, 'mensaje' => $detalle]]
        );
        return ($respuesta->RegistrarErrorImportacionResult ?? false) === true;
    }

    private function lista(mixed $valor): array
    {
        if ($valor === null) {
            return [];
        }

        return is_array($valor) ? $valor : [$valor];
    }
}
