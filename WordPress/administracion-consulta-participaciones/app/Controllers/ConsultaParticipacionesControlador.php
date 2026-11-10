<?php
declare(strict_types=1);

namespace PublicaSA\ConsultaParticipaciones\Controllers;

use PublicaSA\ConsultaParticipaciones\Repositories\ParticipacionesCliente;

final class ConsultaParticipacionesControlador
{
    private string $cedulaJuridica = '';
    private string $mensaje = '';
    private bool $esError = false;
    private array $solicitudes = [];

    private string $directorio;

    public function __construct(string $directorio)
    {
        $this->directorio = $directorio;
    }

    public function procesar(): void
    {
        if (
            !is_singular()
            || !has_shortcode(
                (string) get_post_field('post_content', get_queried_object_id()),
                'consultar_participaciones'
            )
        ) {
            return;
        }

        nocache_headers();

        if (!defined('DONOTCACHEPAGE')) {
            define('DONOTCACHEPAGE', true);
        }

        if (($_SERVER['REQUEST_METHOD'] ?? '') !== 'POST') {
            return;
        }

        $entrada = $_POST['cedula_juridica'] ?? '';
        $this->cedulaJuridica = is_string($entrada) ? trim(wp_unslash($entrada)) : '';

        if ($this->cedulaJuridica === '') {
            $this->mensaje = 'La cédula jurídica es requerida.';
            $this->esError = true;
            return;
        }

        try {
            $nonce = $_POST['_wpnonce'] ?? '';
            if (!is_string($nonce) || !wp_verify_nonce(wp_unslash($nonce), 'consultar_participaciones')) {
                $this->mensaje = 'El formulario venció. Recargue la página.';
                $this->esError = true;
                return;
            }

            $resultado = (new ParticipacionesCliente())->consultar($this->cedulaJuridica);
            $this->mensaje = $resultado['mensaje'];
            $this->esError = !$resultado['correcto'];
            $this->solicitudes = $resultado['correcto'] ? $resultado['solicitudes'] : [];
        } catch (\Throwable $excepcion) {
            $referencia = bin2hex(random_bytes(16));
            error_log('[EXT6 ' . $referencia . '] ' . $excepcion->getMessage());

            try {
                $registrado = (new ParticipacionesCliente())->registrarError(
                    $referencia,
                    $excepcion->getMessage()
                );

                if (!$registrado) {
                    error_log('[EXT6] No se confirmó el registro en errorlog: ' . $referencia);
                }
            } catch (\Throwable $falloRegistro) {
                error_log('[EXT6] No se pudo escribir en errorlog: ' . $falloRegistro->getMessage());
            }

            $this->mensaje = 'No se pudo realizar la consulta.';
            $this->esError = true;
        }
    }

    public function mostrar(): string
    {
        $cedulaJuridica = $this->cedulaJuridica;
        $mensaje = $this->mensaje;
        $esError = $this->esError;
        $solicitudes = $this->solicitudes;

        ob_start();
        require $this->directorio . '/app/Views/consultar-participaciones.php';

        return (string) ob_get_clean();
    }
}
