<?php
/**
 * Plugin Name: Administracion Consulta de Participaciones
 * Description: Consulta de solicitudes por cédula jurídica del proveedor.
 * Version: 1.0.0
 * Author: PublicaSA
 */

declare(strict_types=1);

if (!defined('ABSPATH')) {
    exit;
}

require_once __DIR__ . '/app/bootstrap.php';

add_action('plugins_loaded', static function (): void {
    $controlador = new \PublicaSA\ConsultaParticipaciones\Controllers\ConsultaParticipacionesControlador(
        __DIR__
    );

    add_action('template_redirect', [$controlador, 'procesar']);
    add_shortcode('consultar_participaciones', [$controlador, 'mostrar']);
});

add_action('wp_enqueue_scripts', static function (): void {
    if (
        is_singular()
        && has_shortcode(
            (string) get_post_field('post_content', get_queried_object_id()),
            'consultar_participaciones'
        )
    ) {
        wp_enqueue_style(
            'publicasa-participaciones',
            plugins_url('public/assets/css/participaciones.css', __FILE__),
            [],
            '1.0.0'
        );
    }
});
