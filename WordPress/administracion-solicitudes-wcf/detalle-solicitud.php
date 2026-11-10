<?php

if (!defined('ABSPATH')) {
    exit;
}

if (!defined('PUBLICASA_EXT3_METODO_DETALLE_WCF')) {
    // Espera ObtenerDetalleSolicitudPublicada(int idSolicitud), con null si no está publicada o vigente.
    define('PUBLICASA_EXT3_METODO_DETALLE_WCF', 'ObtenerDetalleSolicitudPublicada');
}

function publicasa_ext3_url_solicitudes_publicadas()
{
    $pagina = get_page_by_path('solicitudes-publicadas');

    return $pagina ? get_permalink($pagina) : home_url('/');
}

function publicasa_ext3_redirigir_a_solicitudes_publicadas()
{
    wp_safe_redirect(publicasa_ext3_url_solicitudes_publicadas());
    exit;
}

function publicasa_ext3_procesar_detalle()
{
    global $post;

    if (!$post || !has_shortcode($post->post_content, 'detalle_solicitud')) {
        return;
    }

    $id_solicitud = isset($_GET['idSolicitud'])
        ? absint(wp_unslash($_GET['idSolicitud']))
        : 0;

    if ($id_solicitud === 0) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }

    try {
        $wsdl = 'http://localhost:56784/ServicioSolicitudes.svc?wsdl';
        $cliente = new SoapClient($wsdl, [
            'exceptions' => true,
            'trace' => false,
            'cache_wsdl' => WSDL_CACHE_NONE
        ]);

        $respuesta = $cliente->__soapCall(
            PUBLICASA_EXT3_METODO_DETALLE_WCF,
            [['idSolicitud' => $id_solicitud]]
        );
    } catch (Throwable $exception) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }

    $nombre_resultado = PUBLICASA_EXT3_METODO_DETALLE_WCF . 'Result';
    $solicitud = isset($respuesta->{$nombre_resultado})
        ? $respuesta->{$nombre_resultado}
        : $respuesta;

    if (
        !is_object($solicitud)
        || !isset($solicitud->Publicada)
        || !filter_var($solicitud->Publicada, FILTER_VALIDATE_BOOLEAN)
        || publicasa_ext3_valor_es_nulo($solicitud->FechaVencimientoPublicacion ?? null)
    ) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }

    $fecha_vencimiento = publicasa_ext3_fecha_timestamp($solicitud->FechaVencimientoPublicacion ?? null);
    $fecha_actual = strtotime(wp_date('Y-m-d'));

    if ($fecha_vencimiento === false || $fecha_actual === false || $fecha_vencimiento < $fecha_actual) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }

    $GLOBALS['publicasa_ext3_solicitud'] = $solicitud;
}

add_action('template_redirect', 'publicasa_ext3_procesar_detalle');

function publicasa_ext3_fecha_timestamp($valor)
{
    if (publicasa_ext3_valor_es_nulo($valor)) {
        return false;
    }

    return strtotime((string) $valor);
}

function publicasa_ext3_valor_es_nulo($valor)
{
    return $valor === null
        || trim((string) $valor) === ''
        || strpos((string) $valor, '0001-01-01') === 0;
}

function publicasa_ext3_valor_o_no_indicado($valor, $fecha = false)
{
    if (publicasa_ext3_valor_es_nulo($valor)) {
        return '<em>&lt;No indicado&gt;</em>';
    }

    if ($fecha) {
        $marca_tiempo = publicasa_ext3_fecha_timestamp($valor);

        if ($marca_tiempo === false) {
            return '<em>&lt;No indicado&gt;</em>';
        }

        $valor = gmdate('d/m/Y', $marca_tiempo);
    }

    return esc_html((string) $valor);
}

function publicasa_shortcode_detalle_solicitud()
{
    if (empty($GLOBALS['publicasa_ext3_solicitud'])) {
        return '';
    }

    $solicitud = $GLOBALS['publicasa_ext3_solicitud'];
    $pagina_regresar = get_page_by_path('solicitudes-publicadas');
    $url_regresar = $pagina_regresar ? get_permalink($pagina_regresar) : home_url('/');

    // EXT4: cuando exista la página con slug "participar", se agrega id_solicitud a su enlace.
    $pagina_participar = get_page_by_path('participar');
    $url_participar = $pagina_participar
        ? add_query_arg('idSolicitud', absint($solicitud->IdSolicitud), get_permalink($pagina_participar))
        : '#';

    $datos = [
        'Consecutivo de oficio' => [$solicitud->ConsecutivoOficio ?? null, false],
        'Documento de respuesta' => [$solicitud->DocumentoRespuesta ?? null, false],
        'Documento de inicio' => [$solicitud->DocumentoInicio ?? null, false],
        'Título de la solicitud' => [$solicitud->Titulo ?? null, false],
        'Descripción' => [$solicitud->Descripcion ?? null, false],
        'Representante' => [$solicitud->NombreRepresentante ?? null, false],
        'Observaciones' => [$solicitud->Observaciones ?? null, false],
        'Estado de la solicitud' => [$solicitud->NombreEstado ?? null, false],
        'Fecha de ingreso' => [$solicitud->FechaIngreso ?? null, true],
        'Fecha de respuesta' => [$solicitud->FechaRespuesta ?? null, true],
        'Fecha de inicio' => [$solicitud->FechaInicio ?? null, true],
        'Fecha de vencimiento de la publicación' => [$solicitud->FechaVencimientoPublicacion ?? null, true],
    ];

    ob_start();
    ?>

    <style>
        .publicasa-ext3-tarjeta {
            width: 100%;
            max-width: 1100px;
            margin: 20px auto 24px;
            padding: 30px;
            box-sizing: border-box;
            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 14px;
            box-shadow: 0 10px 30px rgba(15, 23, 42, 0.06);
        }

        .publicasa-ext3-encabezado {
            margin: 0 0 24px;
            color: #64748b;
            font-size: 16px;
            line-height: 1.6;
        }

        .publicasa-ext3-tabla-contenedor {
            width: 100%;
            overflow-x: auto;
            border: 1px solid #e2e8f0;
            border-radius: 10px;
        }

        .publicasa-ext3-tabla {
            width: 100%;
            margin: 0;
            border-collapse: collapse;
            background: #ffffff;
        }

        .publicasa-ext3-tabla thead {
            background: #147d73;
        }

        .publicasa-ext3-tabla th,
        .publicasa-ext3-tabla td {
            padding: 15px 18px;
            font-size: 15px;
            text-align: left;
            border: none;
        }

        .publicasa-ext3-tabla thead th {
            color: #ffffff;
            font-weight: 600;
        }

        .publicasa-ext3-tabla tbody th,
        .publicasa-ext3-tabla tbody td {
            color: #334155;
            border-bottom: 1px solid #e2e8f0;
        }

        .publicasa-ext3-tabla tbody th {
            font-weight: 600;
        }

        .publicasa-ext3-tabla tbody tr:nth-child(even) {
            background: #f8fafc;
        }

        .publicasa-ext3-tabla tbody tr:last-child > * {
            border-bottom: none;
        }

        .publicasa-ext3-acciones {
            display: flex;
            justify-content: flex-end;
            gap: 12px;
            width: 100%;
            max-width: 1100px;
            margin: 0 auto 40px;
        }

        .publicasa-ext3-boton {
            display: inline-block;
            padding: 10px 18px;
            border: 1px solid #147d73;
            border-radius: 6px;
            font-size: 15px;
            line-height: 1.5;
            text-decoration: none;
        }

        .publicasa-ext3-boton-participar {
            background: #147d73;
            color: #ffffff;
        }

        .publicasa-ext3-boton-regresar {
            background: #ffffff;
            color: #147d73;
        }

        @media (max-width: 768px) {
            .publicasa-ext3-tarjeta {
                margin-top: 10px;
                padding: 20px;
            }

            .publicasa-ext3-tabla th,
            .publicasa-ext3-tabla td {
                padding: 12px;
                font-size: 14px;
            }

            .publicasa-ext3-acciones {
                padding: 0 12px;
                box-sizing: border-box;
            }
        }
    </style>

    <section class="publicasa-ext3-tarjeta">
        <p class="publicasa-ext3-encabezado"><?php echo esc_html('Detalle de la solicitud'); ?></p>

        <div class="publicasa-ext3-tabla-contenedor">
            <table class="publicasa-ext3-tabla">
                <thead>
                    <tr>
                        <th scope="col"><?php echo esc_html('Etiqueta'); ?></th>
                        <th scope="col"><?php echo esc_html('Valor'); ?></th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($datos as $etiqueta => $dato): ?>
                        <tr>
                            <th scope="row"><?php echo esc_html($etiqueta); ?></th>
                            <td><?php echo wp_kses(publicasa_ext3_valor_o_no_indicado($dato[0], $dato[1]), ['em' => []]); ?></td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        </div>
    </section>

    <nav class="publicasa-ext3-acciones" aria-label="<?php echo esc_attr('Acciones de la solicitud'); ?>">
        <a class="publicasa-ext3-boton publicasa-ext3-boton-regresar" href="<?php echo esc_url($url_regresar); ?>">
            <?php echo esc_html('Regresar'); ?>
        </a>
        <a class="publicasa-ext3-boton publicasa-ext3-boton-participar" href="<?php echo esc_url($url_participar); ?>">
            <?php echo esc_html('Participar'); ?>
        </a>
    </nav>

    <?php

    return ob_get_clean();
}

add_shortcode('detalle_solicitud', 'publicasa_shortcode_detalle_solicitud');
