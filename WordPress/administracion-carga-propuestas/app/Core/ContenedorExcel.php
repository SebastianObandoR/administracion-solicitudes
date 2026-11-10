<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

final class ContenedorExcel
{
    private $archivo;
    private array $entradas = [];
    private int $bytesLeidos = 0;
    private const MAX_BYTES = 33554432;

    public function __construct(string $ruta)
    {
        $biblioteca = ABSPATH . 'wp-admin/includes/class-pclzip.php';
        if (!function_exists('gzinflate') || !is_readable($biblioteca)) {
            throw new \RuntimeException('La lectura de Excel requiere PclZip de WordPress y zlib.');
        }

        require_once $biblioteca;
        $this->archivo = new \PclZip($ruta);
        $entradas = $this->archivo->listContent();
        if (!is_array($entradas)) {
            throw new ArchivoExcelInvalido('Seleccione un libro Excel válido en formato .xlsx o .xlsm.');
        }

        foreach ($entradas as $entrada) {
            if (isset($this->entradas[$entrada['filename']])) {
                throw new ArchivoExcelInvalido('El libro contiene entradas duplicadas.');
            }
            $this->entradas[$entrada['filename']] = $entrada;
        }
    }

    public function locateName(string $nombre): bool
    {
        return isset($this->entradas[$nombre]);
    }

    public function getFromName(string $nombre)
    {
        $entrada = $this->entradas[$nombre] ?? null;
        if ($entrada === null) {
            return false;
        }

        if ($entrada['size'] > self::MAX_BYTES - $this->bytesLeidos) {
            throw new ArchivoExcelInvalido('El contenido interno del Excel supera el límite de lectura.');
        }

        $resultado = $this->archivo->extract(
            PCLZIP_OPT_BY_INDEX,
            $entrada['index'],
            PCLZIP_OPT_EXTRACT_AS_STRING
        );

        if (!is_array($resultado) || !isset($resultado[0]['content'])
            || $resultado[0]['status'] !== 'ok') {
            throw new ArchivoExcelInvalido('No se pudo leer una parte del libro Excel.');
        }

        $contenido = $resultado[0]['content'];
        $this->bytesLeidos += strlen($contenido);
        if ($this->bytesLeidos > self::MAX_BYTES) {
            throw new ArchivoExcelInvalido('El contenido interno del Excel supera el límite de lectura.');
        }

        return $contenido;
    }
}
