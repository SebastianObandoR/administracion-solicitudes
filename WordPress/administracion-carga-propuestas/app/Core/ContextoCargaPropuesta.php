<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

final class ContextoCargaPropuesta
{
    private ?array $datos = null;

    public function cargar(): void
    {
        if ($this->datos !== null) {
            return;
        }

        $propia = $this->abrirSesion();
        $this->datos = $_SESSION['publicasa_ext5'] ?? [];

        if ($propia) {
            session_write_close();
        }
    }

    public function obtener(int $idSolicitud): array
    {
        $this->cargar();

        return $this->datos[$idSolicitud] ?? [];
    }

    public function guardar(int $idSolicitud, array $datos): void
    {
        $propia = $this->abrirSesion();
        $_SESSION['publicasa_ext5'][$idSolicitud] = $datos;
        $this->datos = $_SESSION['publicasa_ext5'];

        if ($propia) {
            session_write_close();
        }
    }

    public function eliminar(int $idSolicitud): void
    {
        $propia = $this->abrirSesion();
        unset($_SESSION['publicasa_ext5'][$idSolicitud]);
        $this->datos = $_SESSION['publicasa_ext5'] ?? [];

        if ($propia) {
            session_write_close();
        }
    }

    private function abrirSesion(): bool
    {
        if (session_status() === PHP_SESSION_ACTIVE) {
            return false;
        }

        if (headers_sent()) {
            throw new \RuntimeException('No fue posible iniciar el intercambio de datos con EXT4.');
        }

        if (!session_start([
            'cookie_httponly' => true,
            'cookie_secure' => is_ssl(),
            'cookie_samesite' => 'Lax'
        ])) {
            throw new \RuntimeException('No fue posible iniciar la sesión de carga.');
        }

        return true;
    }
}
