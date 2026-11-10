<?php
/**
 * Plugin Name: Administracion Carga de Propuestas
 * Description: Carga y muestra los desgloses de una propuesta desde Excel.
 * Version: 1.0.0
 * Author: PublicaSA
 */

declare(strict_types=1);

if (!defined('ABSPATH')) {
    exit;
}

require_once __DIR__ . '/app/bootstrap.php';

add_action('plugins_loaded', static function (): void {
    $contexto = new \PublicaSA\CargaPropuestas\Core\ContextoCargaPropuesta();
    $controlador = new \PublicaSA\CargaPropuestas\Controllers\CargaPropuestaControlador(
        __DIR__,
        $contexto
    );
    $integracion = new \PublicaSA\CargaPropuestas\Controllers\IntegracionParticipacionControlador(
        $contexto
    );

    add_action('template_redirect', [$integracion, 'preparar'], 5);
    add_filter('publicasa_ext4_estado_precargado', [$integracion, 'recuperarEstado'], 10, 2);
    add_filter('publicasa_ext4_filas_precargadas', [$integracion, 'recuperarFilas'], 10, 2);
    add_action('template_redirect', [$controlador, 'procesar']);
    add_shortcode('cargar_propuesta_excel', [$controlador, 'mostrar']);
});
add_action('wp_enqueue_scripts', static function (): void {
    if (
        is_singular()
        && has_shortcode(
            (string) get_post_field('post_content', get_queried_object_id()),
            'cargar_propuesta_excel'
        )
    ) {
        wp_enqueue_style(
            'publicasa-carga-propuestas',
            plugins_url('public/assets/css/carga-propuestas.css', __FILE__),
            [],
            '3.0.1'
        );
    }
});
