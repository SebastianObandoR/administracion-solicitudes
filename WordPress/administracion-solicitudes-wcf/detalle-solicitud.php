<?php

if (!defined('ABSPATH')) {
    exit;
}



if (!defined('PUBLICASA_EXT3_METODO_DETALLE_WCF')) {
    define(
        'PUBLICASA_EXT3_METODO_DETALLE_WCF',
        'ObtenerDetalleSolicitudPublicada'
    );
}


function publicasa_ext3_url_solicitudes_publicadas()
{
    $pagina = get_page_by_path(
        'solicitudes-publicadas'
    );

    if ($pagina) {
        return get_permalink($pagina);
    }

    return home_url('/');
}



function publicasa_ext3_redirigir_a_solicitudes_publicadas()
{
    wp_safe_redirect(
        publicasa_ext3_url_solicitudes_publicadas()
    );

    exit;
}



function publicasa_ext3_xml_valor(
    $contenido,
    $nombre
) {

    $patron_nil =
        '/<(?:[a-zA-Z0-9_]+:)?'
        . preg_quote($nombre, '/')
        . '\b[^>]*'
        . '(?:nil|i:nil)="true"'
        . '[^>]*\/?>/si';


    if (
        preg_match(
            $patron_nil,
            $contenido
        )
    ) {
        return null;
    }


    $patron =
        '/<(?:[a-zA-Z0-9_]+:)?'
        . preg_quote($nombre, '/')
        . '\b[^>]*>(.*?)'
        . '<\/(?:[a-zA-Z0-9_]+:)?'
        . preg_quote($nombre, '/')
        . '>/si';


    if (
        preg_match(
            $patron,
            $contenido,
            $coincidencia
        )
    ) {
        return html_entity_decode(
            strip_tags($coincidencia[1]),
            ENT_QUOTES | ENT_XML1,
            'UTF-8'
        );
    }


    return null;
}



function publicasa_ext3_procesar_detalle()
{
    global $post;


    if (
        !$post
        ||
        !has_shortcode(
            $post->post_content,
            'detalle_solicitud'
        )
    ) {
        return;
    }


    $id_solicitud =
        isset($_GET['idSolicitud'])
        ? absint(
            wp_unslash(
                $_GET['idSolicitud']
            )
        )
        : 0;


    if ($id_solicitud === 0) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }


    $parametros =
        '<idSolicitud>'
        . intval($id_solicitud)
        . '</idSolicitud>';


    $respuesta =
        publicasa_wcf_post(
            PUBLICASA_EXT3_METODO_DETALLE_WCF,
            $parametros
        );


    if (is_wp_error($respuesta)) {

        error_log(
            'PublicaSA EXT3: Error consultando detalle: '
            . $respuesta->get_error_message()
        );

        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }



    $nombre_resultado =
        PUBLICASA_EXT3_METODO_DETALLE_WCF
        . 'Result';


    $patron_resultado =
        '/<(?:[a-zA-Z0-9_]+:)?'
        . preg_quote(
            $nombre_resultado,
            '/'
        )
        . '\b([^>]*)>(.*?)'
        . '<\/(?:[a-zA-Z0-9_]+:)?'
        . preg_quote(
            $nombre_resultado,
            '/'
        )
        . '>/si';



    $patron_resultado_nulo =
        '/<(?:[a-zA-Z0-9_]+:)?'
        . preg_quote(
            $nombre_resultado,
            '/'
        )
        . '\b[^>]*'
        . '(?:nil|i:nil)="true"'
        . '[^>]*\/?>/si';


    if (
        preg_match(
            $patron_resultado_nulo,
            $respuesta
        )
    ) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }


    if (
        !preg_match(
            $patron_resultado,
            $respuesta,
            $coincidencia
        )
    ) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }


    $contenido =
        $coincidencia[2];


    $solicitud = new stdClass();


    $solicitud->IdSolicitud =
        intval(
            publicasa_ext3_xml_valor(
                $contenido,
                'IdSolicitud'
            )
        );


    $solicitud->ConsecutivoOficio =
        publicasa_ext3_xml_valor(
            $contenido,
            'ConsecutivoOficio'
        );


    $solicitud->DocumentoRespuesta =
        publicasa_ext3_xml_valor(
            $contenido,
            'DocumentoRespuesta'
        );


    $solicitud->DocumentoInicio =
        publicasa_ext3_xml_valor(
            $contenido,
            'DocumentoInicio'
        );


    $solicitud->Titulo =
        publicasa_ext3_xml_valor(
            $contenido,
            'Titulo'
        );


    $solicitud->Descripcion =
        publicasa_ext3_xml_valor(
            $contenido,
            'Descripcion'
        );


    $solicitud->NombreRepresentante =
        publicasa_ext3_xml_valor(
            $contenido,
            'NombreRepresentante'
        );


    $solicitud->Observaciones =
        publicasa_ext3_xml_valor(
            $contenido,
            'Observaciones'
        );


    $solicitud->NombreEstado =
        publicasa_ext3_xml_valor(
            $contenido,
            'NombreEstado'
        );


    $solicitud->FechaIngreso =
        publicasa_ext3_xml_valor(
            $contenido,
            'FechaIngreso'
        );


    $solicitud->FechaRespuesta =
        publicasa_ext3_xml_valor(
            $contenido,
            'FechaRespuesta'
        );


    $solicitud->FechaInicio =
        publicasa_ext3_xml_valor(
            $contenido,
            'FechaInicio'
        );


    $solicitud->FechaVencimientoPublicacion =
        publicasa_ext3_xml_valor(
            $contenido,
            'FechaVencimientoPublicacion'
        );


    $valor_publicada =
        publicasa_ext3_xml_valor(
            $contenido,
            'Publicada'
        );


    $solicitud->Publicada =
        filter_var(
            $valor_publicada,
            FILTER_VALIDATE_BOOLEAN
        );



    if (
        $solicitud->IdSolicitud <= 0
        ||
        !$solicitud->Publicada
        ||
        publicasa_ext3_valor_es_nulo(
            $solicitud
                ->FechaVencimientoPublicacion
        )
    ) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }


    $fecha_vencimiento =
        publicasa_ext3_fecha_timestamp(
            $solicitud
                ->FechaVencimientoPublicacion
        );


    $fecha_actual =
        strtotime(
            wp_date('Y-m-d')
        );


    if (
        $fecha_vencimiento === false
        ||
        $fecha_actual === false
        ||
        $fecha_vencimiento < $fecha_actual
    ) {
        publicasa_ext3_redirigir_a_solicitudes_publicadas();
    }


    $GLOBALS[
        'publicasa_ext3_solicitud'
    ] = $solicitud;
}


add_action(
    'template_redirect',
    'publicasa_ext3_procesar_detalle'
);




function publicasa_ext3_fecha_timestamp(
    $valor
) {
    if (
        publicasa_ext3_valor_es_nulo(
            $valor
        )
    ) {
        return false;
    }


    return strtotime(
        (string) $valor
    );
}


function publicasa_ext3_valor_es_nulo(
    $valor
) {
    if ($valor === null) {
        return true;
    }


    $texto =
        trim(
            (string) $valor
        );


    return
        $texto === ''
        ||
        strpos(
            $texto,
            '0001-01-01'
        ) === 0;
}


function publicasa_ext3_valor_o_no_indicado(
    $valor,
    $fecha = false
) {
    if (
        publicasa_ext3_valor_es_nulo(
            $valor
        )
    ) {
        return '<em>&lt;No indicado&gt;</em>';
    }


    if ($fecha) {

        $marca_tiempo =
            publicasa_ext3_fecha_timestamp(
                $valor
            );


        if (
            $marca_tiempo === false
        ) {
            return '<em>&lt;No indicado&gt;</em>';
        }


        $valor =
            wp_date(
                'd/m/Y',
                $marca_tiempo
            );
    }


    return esc_html(
        (string) $valor
    );
}


function publicasa_shortcode_detalle_solicitud()
{
    if (
        empty(
            $GLOBALS[
                'publicasa_ext3_solicitud'
            ]
        )
    ) {
        return '';
    }


    $solicitud =
        $GLOBALS[
            'publicasa_ext3_solicitud'
        ];


    $pagina_regresar =
        get_page_by_path(
            'solicitudes-publicadas'
        );


    $url_regresar =
        $pagina_regresar
        ? get_permalink(
            $pagina_regresar
        )
        : home_url('/');


    $pagina_participar =
        get_page_by_path(
            'participaciones'
        );


    if (!$pagina_participar) {

        $pagina_participar =
            get_page_by_path(
                'participar'
            );
    }


    if ($pagina_participar) {

        $url_participar =
            add_query_arg(
                'idSolicitud',
                absint(
                    $solicitud
                        ->IdSolicitud
                ),
                get_permalink(
                    $pagina_participar
                )
            );

    } else {

        $url_participar =
            '#';
    }


    /*
     * Datos requeridos por EXT3.
     */
    $datos = [

        'Consecutivo de oficio' => [
            $solicitud
                ->ConsecutivoOficio
                ?? null,
            false
        ],

        'Documento de respuesta' => [
            $solicitud
                ->DocumentoRespuesta
                ?? null,
            false
        ],

        'Documento de inicio' => [
            $solicitud
                ->DocumentoInicio
                ?? null,
            false
        ],

        'T铆tulo de la solicitud' => [
            $solicitud
                ->Titulo
                ?? null,
            false
        ],

        'Descripci贸n' => [
            $solicitud
                ->Descripcion
                ?? null,
            false
        ],

        'Representante' => [
            $solicitud
                ->NombreRepresentante
                ?? null,
            false
        ],

        'Observaciones' => [
            $solicitud
                ->Observaciones
                ?? null,
            false
        ],

        'Estado de la solicitud' => [
            $solicitud
                ->NombreEstado
                ?? null,
            false
        ],

        'Fecha de ingreso' => [
            $solicitud
                ->FechaIngreso
                ?? null,
            true
        ],

        'Fecha de respuesta' => [
            $solicitud
                ->FechaRespuesta
                ?? null,
            true
        ],

        'Fecha de inicio' => [
            $solicitud
                ->FechaInicio
                ?? null,
            true
        ],

        'Fecha de vencimiento de la publicaci贸n' => [
            $solicitud
                ->FechaVencimientoPublicacion
                ?? null,
            true
        ],
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

            border:
                1px solid #e2e8f0;

            border-radius: 14px;

            box-shadow:
                0 10px 30px
                rgba(15, 23, 42, 0.06);
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

            border:
                1px solid #e2e8f0;

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

            border-bottom:
                1px solid #e2e8f0;
        }


        .publicasa-ext3-tabla tbody th {
            font-weight: 600;
        }


        .publicasa-ext3-tabla
        tbody
        tr:nth-child(even) {
            background: #f8fafc;
        }


        .publicasa-ext3-tabla
        tbody
        tr:last-child
        > * {
            border-bottom: none;
        }


        .publicasa-ext3-acciones {
            display: flex;
            justify-content: flex-end;
            gap: 12px;

            width: 100%;
            max-width: 1100px;

            margin:
                0 auto 40px;
        }


        .publicasa-ext3-boton {
            display: inline-block;

            padding:
                10px 18px;

            border:
                1px solid #147d73;

            border-radius:
                6px;

            font-size:
                15px;

            line-height:
                1.5;

            text-decoration:
                none;
        }


        .publicasa-ext3-boton-participar {
            background:
                #147d73;

            color:
                #ffffff;
        }


        .publicasa-ext3-boton-regresar {
            background:
                #ffffff;

            color:
                #147d73;
        }


        @media (
            max-width: 768px
        ) {

            .publicasa-ext3-tarjeta {
                margin-top:
                    10px;

                padding:
                    20px;
            }


            .publicasa-ext3-tabla th,
            .publicasa-ext3-tabla td {
                padding:
                    12px;

                font-size:
                    14px;
            }


            .publicasa-ext3-acciones {
                padding:
                    0 12px;

                box-sizing:
                    border-box;
            }

        }

    </style>


    <section
        class="publicasa-ext3-tarjeta"
    >

        <p
            class="
                publicasa-ext3-encabezado
            "
        >
            Detalle de la solicitud
        </p>


        <div
            class="
                publicasa-ext3-tabla-contenedor
            "
        >

            <table
                class="
                    publicasa-ext3-tabla
                "
            >

                <thead>

                    <tr>

                        <th scope="col">
                            Etiqueta
                        </th>

                        <th scope="col">
                            Valor
                        </th>

                    </tr>

                </thead>


                <tbody>

                    <?php foreach (
                        $datos
                        as $etiqueta => $dato
                    ): ?>

                        <tr>

                            <th scope="row">

                                <?php
                                    echo esc_html(
                                        $etiqueta
                                    );
                                ?>

                            </th>


                            <td>

                                <?php

                                    echo wp_kses(
                                        publicasa_ext3_valor_o_no_indicado(
                                            $dato[0],
                                            $dato[1]
                                        ),
                                        [
                                            'em' => []
                                        ]
                                    );

                                ?>

                            </td>

                        </tr>

                    <?php endforeach; ?>

                </tbody>

            </table>

        </div>

    </section>


    <nav
        class="
            publicasa-ext3-acciones
        "
        aria-label="Acciones de la solicitud"
    >

        <a
            class="
                publicasa-ext3-boton
                publicasa-ext3-boton-regresar
            "
            href="<?php
                echo esc_url(
                    $url_regresar
                );
            ?>"
        >
            Regresar
        </a>


        <?php if (
            $url_participar !== '#'
        ): ?>

            <a
                class="
                    publicasa-ext3-boton
                    publicasa-ext3-boton-participar
                "
                href="<?php
                    echo esc_url(
                        $url_participar
                    );
                ?>"
            >
                Participar
            </a>

        <?php endif; ?>

    </nav>

    <?php


    return ob_get_clean();
}


add_shortcode(
    'detalle_solicitud',
    'publicasa_shortcode_detalle_solicitud'
);
