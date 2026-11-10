<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

use PublicaSA\CargaPropuestas\Repositories\ImportacionPropuestaCliente;

final class RegistroErrorCarga
{
    public static function registrar(\Throwable $excepcion): string
    {
        $referencia = bin2hex(random_bytes(16));
        error_log('[EXT5 ' . $referencia . '] ' . $excepcion->getMessage());

        try {
            $registrado = (new ImportacionPropuestaCliente())->registrarError(
                $referencia,
                $excepcion->getMessage()
            );

            if (!$registrado) {
                error_log('[EXT5] No se confirmó el registro en errorlog: ' . $referencia);
            }
        } catch (\Throwable $falloRegistro) {
            error_log('[EXT5] No se pudo escribir en errorlog: ' . $falloRegistro->getMessage());
        }

        return $referencia;
    }
}
