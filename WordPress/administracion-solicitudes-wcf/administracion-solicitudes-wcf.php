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



if (!defined('PUBLICASA_WCF_ENDPOINT')) {
    define(
        'PUBLICASA_WCF_ENDPOINT',
        'https://tiusr21pl.cuc-carrera-ti.ac.cr/servicios/ServicioSolicitudes.svc'
    );
}

if (!defined('PUBLICASA_WCF_NAMESPACE')) {
    define(
        'PUBLICASA_WCF_NAMESPACE',
        'http://tempuri.org/'
    );
}

if (!defined('PUBLICASA_WCF_CONTRACT')) {
    define(
        'PUBLICASA_WCF_CONTRACT',
        'http://tempuri.org/IServicioSolicitudes/'
    );
}




require_once plugin_dir_path(__FILE__) . 'detalle-solicitud.php';
require_once plugin_dir_path(__FILE__) . 'participar-solicitud.php';



function publicasa_wcf_post(
    $metodo,
    $contenido_parametros = ''
) {
    $soap_action =
        PUBLICASA_WCF_CONTRACT
        . $metodo;


    $xml =
        '<?xml version="1.0" encoding="utf-8"?>'
        . '<s:Envelope '
        . 'xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">'
        . '<s:Body>'
        . '<' . $metodo
        . ' xmlns="' . PUBLICASA_WCF_NAMESPACE . '">'
        . $contenido_parametros
        . '</' . $metodo . '>'
        . '</s:Body>'
        . '</s:Envelope>';


    $respuesta =
        wp_remote_post(
            PUBLICASA_WCF_ENDPOINT,
            [
                'timeout' => 30,

                'headers' => [
                    'Content-Type' =>
                        'text/xml; charset=utf-8',

                    'SOAPAction' =>
                        '"' . $soap_action . '"'
                ],

                'body' => $xml
            ]
        );


    if (is_wp_error($respuesta)) {

        error_log(
            'PublicaSA WCF: '
            . $respuesta->get_error_message()
        );

        return new WP_Error(
            'error_wcf',
            'No fue posible comunicarse con el servicio.'
        );
    }


    $codigo =
        wp_remote_retrieve_response_code(
            $respuesta
        );


    $cuerpo =
        wp_remote_retrieve_body(
            $respuesta
        );


    if (
        $codigo < 200
        ||
        $codigo >= 300
    ) {

        error_log(
            'PublicaSA WCF HTTP '
            . $codigo
            . ': '
            . $cuerpo
        );

        return new WP_Error(
            'error_wcf',
            'El servicio respondió con un error.'
        );
    }


    /*
     * Detectar SOAP Fault.
     */
    if (
        stripos(
            $cuerpo,
            '<s:Fault>'
        ) !== false
        ||
        stripos(
            $cuerpo,
            ':Fault>'
        ) !== false
    ) {

        error_log(
            'PublicaSA WCF SOAP Fault: '
            . $cuerpo
        );

        return new WP_Error(
            'error_wcf',
            'El servicio no pudo procesar la solicitud.'
        );
    }


    return $cuerpo;
}


function publicasa_xml_obtener_valor(
    $contenido,
    $nombre
) {
    $patron =
        '/<(?:[a-zA-Z0-9_]+:)?'
        . preg_quote(
            $nombre,
            '/'
        )
        . '\b[^>]*>(.*?)'
        . '<\/(?:[a-zA-Z0-9_]+:)?'
        . preg_quote(
            $nombre,
            '/'
        )
        . '>/si';


    if (
        preg_match(
            $patron,
            $contenido,
            $coincidencia
        )
    ) {

        return html_entity_decode(
            strip_tags(
                $coincidencia[1]
            ),
            ENT_QUOTES | ENT_XML1,
            'UTF-8'
        );
    }


    return '';
}




function publicasa_obtener_solicitudes_publicadas()
{
    $respuesta =
        publicasa_wcf_post(
            'ObtenerSolicitudesPublicadas'
        );


    if (is_wp_error($respuesta)) {
        return $respuesta;
    }


    $patron =
        '/<(?:[a-zA-Z0-9_]+:)?SolicitudPublicada'
        . '\b[^>]*>(.*?)'
        . '<\/(?:[a-zA-Z0-9_]+:)?SolicitudPublicada>/si';


    preg_match_all(
        $patron,
        $respuesta,
        $coincidencias
    );


    if (
        empty($coincidencias)
        ||
        empty($coincidencias[1])
    ) {
        return [];
    }


    $solicitudes = [];


    foreach (
        $coincidencias[1]
        as $contenido
    ) {

        $solicitud =
            new stdClass();


        $solicitud->IdSolicitud =
            intval(
                publicasa_xml_obtener_valor(
                    $contenido,
                    'IdSolicitud'
                )
            );


        $solicitud->ConsecutivoOficio =
            publicasa_xml_obtener_valor(
                $contenido,
                'ConsecutivoOficio'
            );


        $solicitud->Titulo =
            publicasa_xml_obtener_valor(
                $contenido,
                'Titulo'
            );


        $solicitud->NombreRepresentante =
            publicasa_xml_obtener_valor(
                $contenido,
                'NombreRepresentante'
            );


        $solicitudes[] =
            $solicitud;
    }


    return $solicitudes;
}


function publicasa_url_detalle_solicitud($id_solicitud)
{
    /*
     * Buscar primero por shortcode.
     * Así no dependemos del slug que tenga
     * la página en WordPress.
     */
    $paginas = get_pages([
        'post_status' => 'publish'
    ]);

    foreach ($paginas as $pagina) {

        if (
            has_shortcode(
                $pagina->post_content,
                'detalle_solicitud'
            )
        ) {

            return add_query_arg(
                [
                    'page_id' => intval($pagina->ID),
                    'idSolicitud' => intval($id_solicitud)
                ],
                home_url('/')
            );
        }
    }


    /*
     * Segundo intento por slug,
     * por si acaso.
     */
    $pagina_detalle =
        get_page_by_path(
            'detalle-solicitud'
        );


    if ($pagina_detalle) {

        return add_query_arg(
            [
                'page_id' => intval($pagina_detalle->ID),
                'idSolicitud' => intval($id_solicitud)
            ],
            home_url('/')
        );
    }


    /*
     * Si no existe la página,
     * registrar el error.
     */
    error_log(
        'PublicaSA: No se encontró ninguna página con el shortcode [detalle_solicitud].'
    );


    return home_url('/');
}


function publicasa_shortcode_solicitudes_publicadas()
{
    $solicitudes =
        publicasa_obtener_solicitudes_publicadas();


    ob_start();
    ?>

    <style>

        .publicasa-solicitudes-wrapper {
            width: 100%;
            max-width: 1100px;

            margin:
                20px auto 40px auto;

            padding: 30px;

            box-sizing:
                border-box;

            background:
                #ffffff;

            border:
                1px solid #e2e8f0;

            border-radius:
                14px;

            box-shadow:
                0 10px 30px
                rgba(
                    15,
                    23,
                    42,
                    0.06
                );
        }


        .publicasa-solicitudes-header {
            margin-bottom:
                24px;
        }


        .publicasa-solicitudes-subtitle {
            margin: 0;

            color:
                #64748b;

            font-size:
                16px;

            line-height:
                1.6;
        }


        .publicasa-tabla-contenedor {
            width: 100%;

            overflow-x:
                auto;

            border:
                1px solid #e2e8f0;

            border-radius:
                10px;
        }


        .publicasa-tabla {
            width: 100%;

            margin: 0;

            border-collapse:
                collapse;

            background:
                #ffffff;
        }


        .publicasa-tabla thead {
            background:
                #147d73;
        }


        .publicasa-tabla th {
            padding:
                15px 18px;

            color:
                #ffffff;

            font-size:
                15px;

            font-weight:
                600;

            text-align:
                left;

            border:
                none;
        }


        .publicasa-tabla td {
            padding:
                16px 18px;

            color:
                #334155;

            font-size:
                15px;

            border:
                none;

            border-bottom:
                1px solid #e2e8f0;
        }


        .publicasa-tabla
        tbody
        tr:last-child
        td {
            border-bottom:
                none;
        }


        .publicasa-tabla
        tbody
        tr:nth-child(even) {
            background:
                #f8fafc;
        }


        .publicasa-tabla
        tbody
        tr {
            transition:
                background-color
                0.2s ease;
        }


        .publicasa-tabla
        tbody
        tr:hover {
            background:
                #f0fdfa;
        }


        .publicasa-enlace {
            color:
                #147d73;

            font-weight:
                600;

            text-decoration:
                none;

            cursor:
                pointer;
        }


        .publicasa-enlace:hover {
            color:
                #0f5f58;

            text-decoration:
                underline;
        }


        .publicasa-mensaje {
            padding:
                18px 20px;

            border-radius:
                10px;

            font-size:
                15px;

            line-height:
                1.5;
        }


        .publicasa-mensaje-vacio {
            color:
                #475569;

            background:
                #f8fafc;

            border:
                1px solid #e2e8f0;
        }


        .publicasa-mensaje-error {
            color:
                #991b1b;

            background:
                #fef2f2;

            border:
                1px solid #fecaca;
        }


        @media (
            max-width: 768px
        ) {

            .publicasa-solicitudes-wrapper {
                margin-top:
                    10px;

                padding:
                    20px;
            }


            .publicasa-tabla th,
            .publicasa-tabla td {
                padding:
                    12px;

                font-size:
                    14px;
            }

        }

    </style>


    <div
        class="
            publicasa-solicitudes-wrapper
        "
    >

        <div
            class="
                publicasa-solicitudes-header
            "
        >

            <p
                class="
                    publicasa-solicitudes-subtitle
                "
            >
                Consulte aquí las solicitudes
                publicadas disponibles por
                Pública S.A.
            </p>

        </div>


        <?php if (
            is_wp_error(
                $solicitudes
            )
        ): ?>

            <div
                class="
                    publicasa-mensaje
                    publicasa-mensaje-error
                "
            >
                No fue posible consultar
                las solicitudes publicadas.
            </div>


        <?php elseif (
            empty(
                $solicitudes
            )
        ): ?>

            <div
                class="
                    publicasa-mensaje
                    publicasa-mensaje-vacio
                "
            >
                No hay solicitudes publicadas
                disponibles en este momento.
            </div>


        <?php else: ?>

            <div
                class="
                    publicasa-tabla-contenedor
                "
            >

                <table
                    class="
                        publicasa-tabla
                    "
                >

                    <thead>

                        <tr>

                            <th>
                                Consecutivo de oficio
                            </th>

                            <th>
                                Título
                            </th>

                            <th>
                                Representante
                            </th>

                        </tr>

                    </thead>


                    <tbody>

                        <?php foreach (
                            $solicitudes
                            as $solicitud
                        ): ?>


                            <?php

                            $url_detalle =
                                publicasa_url_detalle_solicitud(
                                    $solicitud
                                        ->IdSolicitud
                                );

                            ?>


                            <tr>

                                <td>

                                    <a
                                        class="
                                            publicasa-enlace
                                        "
                                        href="<?php
                                            echo esc_url(
                                                $url_detalle
                                            );
                                        ?>"
                                    >
                                        <?php
                                            echo esc_html(
                                                $solicitud
                                                    ->ConsecutivoOficio
                                            );
                                        ?>
                                    </a>

                                </td>


                                <td>

                                    <?php
                                        echo esc_html(
                                            $solicitud
                                                ->Titulo
                                        );
                                    ?>

                                </td>


                                <td>

                                    <?php

                                    if (
                                        !empty(
                                            $solicitud
                                                ->NombreRepresentante
                                        )
                                    ) {

                                        echo esc_html(
                                            $solicitud
                                                ->NombreRepresentante
                                        );

                                    } else {

                                        echo '-';
                                    }

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
