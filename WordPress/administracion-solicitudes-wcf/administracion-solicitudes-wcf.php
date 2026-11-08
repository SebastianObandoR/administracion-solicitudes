<?php

/**
 * Plugin Name: Administracion Solicitudes WCF
 * Description: Muestra las solicitudes publicadas obtenidas desde el Web Service WCF.
 * Version: 1.0.0
 * Author: Equipo Publica S.A.
 */

if (!defined('ABSPATH')) {
    exit;
}

require_once plugin_dir_path(__FILE__) . 'detalle-solicitud.php';
require_once plugin_dir_path(__FILE__) . 'participar-solicitud.php';

function publicasa_obtener_solicitudes_publicadas()
{
    $wsdl = 'http://localhost:56784/ServicioSolicitudes.svc?wsdl';

    try {
        $cliente = new SoapClient($wsdl, [
            'exceptions' => true,
            'trace' => false,
            'cache_wsdl' => WSDL_CACHE_NONE
        ]);

        $respuesta = $cliente->ObtenerSolicitudesPublicadas();

        if (
            !isset($respuesta->ObtenerSolicitudesPublicadasResult) ||
            !isset($respuesta->ObtenerSolicitudesPublicadasResult->SolicitudPublicada)
        ) {
            return [];
        }

        $solicitudes =
            $respuesta
                ->ObtenerSolicitudesPublicadasResult
                ->SolicitudPublicada;

        if (!is_array($solicitudes)) {
            $solicitudes = [$solicitudes];
        }

        return $solicitudes;
    } catch (Throwable $exception) {
        return new WP_Error(
            'error_wcf',
            'No fue posible consultar las solicitudes publicadas.'
        );
    }
}

function publicasa_shortcode_solicitudes_publicadas()
{
    $solicitudes = publicasa_obtener_solicitudes_publicadas();

    ob_start();
    ?>

    <style>
        .publicasa-solicitudes-wrapper {
            width: 100%;
            max-width: 1100px;
            margin: 20px auto 40px auto;
            padding: 30px;
            box-sizing: border-box;

            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 14px;

            box-shadow: 0 10px 30px rgba(15, 23, 42, 0.06);
        }

        .publicasa-solicitudes-header {
            margin-bottom: 24px;
        }

        .publicasa-solicitudes-subtitle {
            margin: 0;
            color: #64748b;
            font-size: 16px;
            line-height: 1.6;
        }

        .publicasa-tabla-contenedor {
            width: 100%;
            overflow-x: auto;

            border: 1px solid #e2e8f0;
            border-radius: 10px;
        }

        .publicasa-tabla {
            width: 100%;
            margin: 0;
            border-collapse: collapse;

            background: #ffffff;
        }

        .publicasa-tabla thead {
            background: #147d73;
        }

        .publicasa-tabla th {
            padding: 15px 18px;

            color: #ffffff;
            font-size: 15px;
            font-weight: 600;
            text-align: left;

            border: none;
        }

        .publicasa-tabla td {
            padding: 16px 18px;

            color: #334155;
            font-size: 15px;

            border: none;
            border-bottom: 1px solid #e2e8f0;
        }

        .publicasa-tabla tbody tr:last-child td {
            border-bottom: none;
        }

        .publicasa-tabla tbody tr:nth-child(even) {
            background: #f8fafc;
        }

        .publicasa-tabla tbody tr {
            transition: background-color 0.2s ease;
        }

        .publicasa-tabla tbody tr:hover {
            background: #f0fdfa;
        }

        .publicasa-enlace {
            color: #147d73;
            font-weight: 600;
            text-decoration: none;
        }

        .publicasa-enlace:hover {
            color: #0f5f58;
            text-decoration: underline;
        }

        .publicasa-mensaje {
            padding: 18px 20px;

            border-radius: 10px;

            font-size: 15px;
            line-height: 1.5;
        }

        .publicasa-mensaje-vacio {
            color: #475569;

            background: #f8fafc;
            border: 1px solid #e2e8f0;
        }

        .publicasa-mensaje-error {
            color: #991b1b;

            background: #fef2f2;
            border: 1px solid #fecaca;
        }

        @media (max-width: 768px) {
            .publicasa-solicitudes-wrapper {
                margin-top: 10px;
                padding: 20px;
            }

            .publicasa-tabla th,
            .publicasa-tabla td {
                padding: 12px;
                font-size: 14px;
            }
        }
    </style>

    <div class="publicasa-solicitudes-wrapper">

        <div class="publicasa-solicitudes-header">
            <p class="publicasa-solicitudes-subtitle">
                Consulte aquí las solicitudes publicadas disponibles por Pública S.A.
            </p>
        </div>

        <?php if (is_wp_error($solicitudes)): ?>

            <div class="publicasa-mensaje publicasa-mensaje-error">
                <?php echo esc_html($solicitudes->get_error_message()); ?>
            </div>

        <?php elseif (empty($solicitudes)): ?>

            <div class="publicasa-mensaje publicasa-mensaje-vacio">
                No hay solicitudes publicadas disponibles en este momento.
            </div>

        <?php else: ?>

            <div class="publicasa-tabla-contenedor">

                <table class="publicasa-tabla">

                    <thead>
                        <tr>
                            <th>Consecutivo de oficio</th>
                            <th>Título</th>
                            <th>Representante</th>
                        </tr>
                    </thead>

                    <tbody>

                        <?php foreach ($solicitudes as $solicitud): ?>

                            <tr>

                                <td>
                                    <a
                                        class="publicasa-enlace"
                                        href="<?php
                                            echo esc_url(
                                                home_url(
                                                    '/detalle-solicitud/?idSolicitud='
                                                    . intval($solicitud->IdSolicitud)
                                                )
                                            );
                                        ?>"
                                    >
                                        <?php
                                            echo esc_html(
                                                $solicitud->ConsecutivoOficio
                                            );
                                        ?>
                                    </a>
                                </td>

                                <td>
                                    <?php
                                        echo esc_html(
                                            $solicitud->Titulo
                                        );
                                    ?>
                                </td>

                                <td>
                                    <?php
                                        echo !empty($solicitud->NombreRepresentante)
                                            ? esc_html($solicitud->NombreRepresentante)
                                            : '-';
                                    ?>
                                </td>

                            </tr>

                        <?php endforeach; ?>

                    </tbody>

                </table>

            </div>

        <?php endif; ?>

    </div>

    <?php

    return ob_get_clean();
}

add_shortcode(
    'solicitudes_publicadas',
    'publicasa_shortcode_solicitudes_publicadas'
);