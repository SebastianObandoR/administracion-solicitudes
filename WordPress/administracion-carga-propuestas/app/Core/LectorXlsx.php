<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

final class LectorXlsx
{
    public function leer(string $ruta): array
    {
        if (!class_exists(\ZipArchive::class)) {
            throw new \RuntimeException('La extensión ZIP de PHP debe estar habilitada.');
        }

        $zip = new \ZipArchive();
        if ($zip->open($ruta, \ZipArchive::RDONLY) !== true) {
            throw new ArchivoExcelInvalido('Seleccione un libro Excel válido en formato .xlsx o .xlsm.');
        }

        try {
            $libro = $this->xml($zip, 'xl/workbook.xml');
            $relaciones = $this->xml($zip, 'xl/_rels/workbook.xml.rels');
            $textos = $this->textosCompartidos($zip);
            $hojas = $libro->xpath('//*[local-name()="sheets"]/*[local-name()="sheet"]') ?: [];
            $filas = [];

            foreach ($hojas as $hoja) {
                $atributos = $hoja->attributes(
                    'http://schemas.openxmlformats.org/officeDocument/2006/relationships'
                );
                $id = (string) $atributos['id'];
                $entrada = $this->ubicacionHoja($relaciones, $id);

                if ($entrada !== null) {
                    foreach ($this->leerHoja($zip, $entrada, $textos) as $fila) {
                        $filas[] = $fila;
                    }
                }
            }

            return $filas;
        } finally {
            $zip->close();
        }
    }

    private function ubicacionHoja(\SimpleXMLElement $relaciones, string $id): ?string
    {
        foreach ($relaciones->children() as $relacion) {
            if (
                (string) $relacion['Id'] !== $id
                || !str_ends_with((string) $relacion['Type'], '/worksheet')
            ) {
                continue;
            }

            $destino = (string) $relacion['Target'];

            return str_starts_with($destino, '/')
                ? ltrim($destino, '/')
                : 'xl/' . $destino;
        }

        return null;
    }

    private function leerHoja(\ZipArchive $zip, string $entrada, array $textos): array
    {
        $xml = $this->xml($zip, $entrada);
        $nodos = $xml->xpath('//*[local-name()="sheetData"]/*[local-name()="row"]') ?: [];
        $filas = [];

        foreach ($nodos as $nodo) {
            $fila = [];
            $siguienteColumna = 0;

            foreach ($nodo->xpath('./*[local-name()="c"]') ?: [] as $celda) {
                $columna = $siguienteColumna;

                if (preg_match('/^([A-Z]+)/', (string) $celda['r'], $partes)) {
                    $columna = $this->indiceColumna($partes[1]);
                }

                $fila[$columna] = $this->valor($celda, $textos);
                $siguienteColumna = $columna + 1;
            }

            $filas[] = $fila;
        }

        return $filas;
    }

    private function indiceColumna(string $letras): int
    {
        $indice = 0;

        foreach (str_split($letras) as $letra) {
            $indice = $indice * 26 + ord($letra) - ord('A') + 1;
        }

        return $indice - 1;
    }

    private function textosCompartidos(\ZipArchive $zip): array
    {
        if ($zip->locateName('xl/sharedStrings.xml') === false) {
            return [];
        }

        $xml = $this->xml($zip, 'xl/sharedStrings.xml');
        $resultado = [];

        foreach ($xml->xpath('/*[local-name()="sst"]/*[local-name()="si"]') ?: [] as $texto) {
            $resultado[] = $this->texto($texto);
        }

        return $resultado;
    }

    private function valor(\SimpleXMLElement $celda, array $textos): string
    {
        $tipo = (string) $celda['t'];
        $valores = $celda->xpath('./*[local-name()="v"]') ?: [];
        $valor = isset($valores[0]) ? (string) $valores[0] : '';

        if ($tipo === 's') {
            return $textos[(int) $valor] ?? '';
        }

        if ($tipo === 'inlineStr') {
            return $this->texto($celda);
        }

        return $valor;
    }

    private function texto(\SimpleXMLElement $xml): string
    {
        $texto = '';

        foreach ($xml->xpath('.//*[local-name()="t"]') ?: [] as $nodo) {
            $texto .= (string) $nodo;
        }

        return $texto;
    }

    private function xml(\ZipArchive $zip, string $entrada): \SimpleXMLElement
    {
        $contenido = $zip->getFromName($entrada);
        if ($contenido === false) {
            throw new ArchivoExcelInvalido('El archivo no contiene un libro Excel legible.');
        }

        $anterior = libxml_use_internal_errors(true);

        try {
            $xml = simplexml_load_string(
                $contenido,
                \SimpleXMLElement::class,
                LIBXML_NONET | LIBXML_NOBLANKS
            );

            if ($xml === false) {
                throw new ArchivoExcelInvalido('No fue posible leer el contenido del Excel.');
            }

            return $xml;
        } finally {
            libxml_clear_errors();
            libxml_use_internal_errors($anterior);
        }
    }
}
