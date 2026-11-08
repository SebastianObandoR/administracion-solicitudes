<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Controllers;

use PublicaSA\CargaPropuestas\Core\ContextoCargaPropuesta;
use PublicaSA\CargaPropuestas\Core\RegistroErrorCarga;

final class IntegracionParticipacionControlador
{
    private string $error = '';

    public function __construct(private ContextoCargaPropuesta $contexto)
    {
    }

    public function preparar(): void
    {
        if (
            !is_singular()
            || !has_shortcode(
                (string) get_post_field('post_content', get_queried_object_id()),
                'participar_solicitud'
            )
        ) {
            return;
        }

        nocache_headers();

        if (!defined('DONOTCACHEPAGE')) {
            define('DONOTCACHEPAGE', true);
        }

        try {
            $this->contexto->cargar();
            $idSolicitud = absint($_GET['idSolicitud'] ?? 0);

            if (($_GET['enviado'] ?? '') === '1' && $idSolicitud > 0) {
                $this->contexto->eliminar($idSolicitud);
            }
        } catch (\Throwable $excepcion) {
            $referencia = RegistroErrorCarga::registrar($excepcion);
            $this->error = 'No fue posible recuperar la carga de Excel. Referencia: ' . $referencia;
        }
    }

    public function recuperarEstado(array $estado, int $idSolicitud): array
    {
        if ($this->error !== '') {
            $estado['errores'][] = $this->error;
            return $estado;
        }

        if (($_SERVER['REQUEST_METHOD'] ?? '') !== 'GET' || $estado['enviado']) {
            return $estado;
        }

        $datos = $this->contexto->obtener($idSolicitud);
        if (!$datos) {
            return $estado;
        }

        $estado['valores'] = $datos['valores'];
        $estado['pedir_contacto'] = $datos['pedir_contacto'];

        return $estado;
    }

    public function recuperarFilas(array $filas, int $idSolicitud): array
    {
        if ($this->error !== '') {
            return $filas;
        }

        $datos = $this->contexto->obtener($idSolicitud);

        return $datos['valores']['filas'] ?? $filas;
    }
}
