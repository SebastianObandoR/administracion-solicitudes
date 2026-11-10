<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

final class MapeadorColumnasExcel
{
    private const CAMPOS = [
        'Mes',
        'Anio',
        'Horas',
        'Monto',
        'Iva',
        'Total',
        'Observaciones',
        'PorcentajeCobro'
    ];

    public function convertir(array $filas): array
    {
        $resultado = [];
        $mapa = array_flip(self::CAMPOS);

        foreach ($filas as $indice => $fila) {
            if (!array_filter($fila, static fn($valor) => trim((string) $valor) !== '')) {
                continue;
            }

            $encabezados = $this->encabezados($fila);
            if ($encabezados !== null) {
                $mapa = $encabezados;
                continue;
            }

            $dato = ['NumeroFila' => $indice + 1];

            foreach (self::CAMPOS as $campo) {
                $dato[$campo] = (string) ($fila[$mapa[$campo]] ?? '');
            }

            $resultado[] = $dato;
        }

        return $resultado;
    }

    private function encabezados(array $fila): ?array
    {
        $nombres = [
            'mes' => 'Mes',
            'ano' => 'Anio',
            'anio' => 'Anio',
            'horas' => 'Horas',
            'monto' => 'Monto',
            'iva' => 'Iva',
            'total' => 'Total',
            'observaciones' => 'Observaciones',
            'porcentajedecobro' => 'PorcentajeCobro'
        ];
        $mapa = [];

        foreach ($fila as $indice => $nombre) {
            $clave = $this->normalizar((string) $nombre);
            if (isset($nombres[$clave])) {
                $mapa[$nombres[$clave]] = $indice;
            }
        }

        return count($mapa) === count(self::CAMPOS) ? $mapa : null;
    }

    private function normalizar(string $nombre): string
    {
        $nombre = strtr(
            mb_strtolower(trim($nombre)),
            [
                'á' => 'a',
                'é' => 'e',
                'í' => 'i',
                'ó' => 'o',
                'ú' => 'u',
                'ñ' => 'n'
            ]
        );

        return preg_replace('/\s+/u', '', $nombre);
    }
}
