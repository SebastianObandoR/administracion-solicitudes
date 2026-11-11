<?php

if (!defined('ABSPATH')) {
    exit;
}

if (!defined('WSDL_PARTICIPACION')) {
    define('WSDL_PARTICIPACION', 'http://localhost:56784/ServicioSolicitudes.svc?wsdl');
}

define('TASA_IVA_PARTICIPACION', 0.13);

function cliente_wcf_participacion()
{
    return new SoapClient(WSDL_PARTICIPACION, [
        'exceptions' => true,
        'trace' => false,
        'cache_wsdl' => WSDL_CACHE_NONE
    ]);
}

function url_solicitudes_participacion()
{
    $pagina = get_page_by_path('solicitudes-publicadas');

    return $pagina ? get_permalink($pagina) : home_url('/');
}

function url_detalle_participacion($id_solicitud)
{
    return home_url('/detalle-solicitud/?idSolicitud=' . absint($id_solicitud));
}

function meses_participacion()
{
    return [
        1 => 'Enero', 2 => 'Febrero', 3 => 'Marzo', 4 => 'Abril',
        5 => 'Mayo', 6 => 'Junio', 7 => 'Julio', 8 => 'Agosto',
        9 => 'Setiembre', 10 => 'Octubre', 11 => 'Noviembre', 12 => 'Diciembre'
    ];
}

function convertir_numero_participacion($valor)
{
    $valor = str_replace(',', '.', trim((string) $valor));

    return is_numeric($valor) ? (float) $valor : null;
}

function obtener_solicitud_participacion($id_solicitud)
{
    try {
        $respuesta = cliente_wcf_participacion()->ObtenerDetalleSolicitudPublicada([
            'idSolicitud' => $id_solicitud
        ]);
    } catch (Throwable $exception) {
        return null;
    }

    $solicitud = $respuesta->ObtenerDetalleSolicitudPublicadaResult ?? null;

    if (
        !is_object($solicitud)
        || !isset($solicitud->Publicada)
        || !filter_var($solicitud->Publicada, FILTER_VALIDATE_BOOLEAN)
    ) {
        return null;
    }

    return $solicitud;
}

function procesar_participacion()
{
    global $post;

    if (!$post || !has_shortcode($post->post_content, 'participar_solicitud')) {
        return;
    }

    $es_post = isset($_SERVER['REQUEST_METHOD']) && $_SERVER['REQUEST_METHOD'] === 'POST';

    $id_solicitud = 0;
    if ($es_post && isset($_POST['idSolicitud'])) {
        $id_solicitud = absint(wp_unslash($_POST['idSolicitud']));
    } elseif (isset($_GET['idSolicitud'])) {
        $id_solicitud = absint(wp_unslash($_GET['idSolicitud']));
    }

    $solicitud = $id_solicitud > 0 ? obtener_solicitud_participacion($id_solicitud) : null;

    if ($solicitud === null) {
        wp_safe_redirect(url_solicitudes_participacion());
        exit;
    }

    $estado = [
        'solicitud' => $solicitud,
        'errores' => [],
        'valores' => null,
        'pedir_contacto' => false,
        'enviado' => isset($_GET['enviado']) && $_GET['enviado'] === '1'
    ];

    $estado = apply_filters('publicasa_ext4_estado_precargado', $estado, $id_solicitud);

    if ($es_post && isset($_POST['publicasa_ext4_confirmar'])) {
        $estado = confirmar_participacion($estado, $id_solicitud);
    }

    $GLOBALS['publicasa_ext4_estado'] = $estado;
}

add_action('template_redirect', 'procesar_participacion');

function encolar_sweetalert_participacion()
{
    global $post;

    if (!$post || !has_shortcode($post->post_content, 'participar_solicitud')) {
        return;
    }

    wp_enqueue_script(
        'publicasa-sweetalert2',
        'https://cdn.jsdelivr.net/npm/sweetalert2@11',
        [],
        null,
        true
    );
}

add_action('wp_enqueue_scripts', 'encolar_sweetalert_participacion');

function confirmar_participacion($estado, $id_solicitud)
{
    $nonce = isset($_POST['publicasa_ext4_nonce'])
        ? sanitize_text_field(wp_unslash($_POST['publicasa_ext4_nonce']))
        : '';

    if (!wp_verify_nonce($nonce, 'publicasa_ext4_confirmar')) {
        $estado['errores'][] = 'La sesión del formulario expiró. Intente nuevamente.';

        return $estado;
    }

    $campo = function ($nombre) {
        return isset($_POST[$nombre]) ? sanitize_text_field(wp_unslash($_POST[$nombre])) : '';
    };

    $valores = [
        'cedula_juridica' => $campo('cedula_juridica'),
        'nombre_proveedor' => $campo('nombre_proveedor'),
        'nombre_representante' => $campo('nombre_representante'),
        'telefono' => $campo('telefono'),
        'correo' => $campo('correo'),
        'filas' => []
    ];

    $filas_entrada = (isset($_POST['desgloses']) && is_array($_POST['desgloses']))
        ? wp_unslash($_POST['desgloses'])
        : [];

    $filas_soap = [];
    $errores = [];

    foreach (array_values($filas_entrada) as $indice => $fila) {
        if (!is_array($fila)) {
            continue;
        }

        $limpia = [];
        foreach (['mes', 'anio', 'horas', 'monto', 'observaciones', 'porcentaje_cobro'] as $clave) {
            $limpia[$clave] = isset($fila[$clave]) ? sanitize_text_field($fila[$clave]) : '';
        }
        $valores['filas'][] = $limpia;

        $mes = convertir_numero_participacion($limpia['mes']);
        $anio = convertir_numero_participacion($limpia['anio']);
        $horas = convertir_numero_participacion($limpia['horas']);
        $monto = convertir_numero_participacion($limpia['monto']);
        $porcentaje = convertir_numero_participacion($limpia['porcentaje_cobro']);

        if (
            $mes === null || $anio === null || $horas === null || $monto === null
            || $porcentaje === null || $limpia['observaciones'] === ''
        ) {
            $errores[] = 'Desglose ' . ($indice + 1) . ': todos los datos son requeridos.';
            continue;
        }

        $iva = round($monto * TASA_IVA_PARTICIPACION, 2);

        $filas_soap[] = [
            'Mes' => (int) $mes,
            'Anio' => (int) $anio,
            'Horas' => number_format($horas, 2, '.', ''),
            'Monto' => number_format($monto, 2, '.', ''),
            'Iva' => number_format($iva, 2, '.', ''),
            'Total' => number_format($monto + $iva, 2, '.', ''),
            'Observaciones' => $limpia['observaciones'],
            'PorcentajeCobro' => number_format($porcentaje, 2, '.', '')
        ];
    }

    if ($valores['cedula_juridica'] === '') {
        $errores[] = 'La cédula jurídica es requerida.';
    }

    if ($valores['nombre_proveedor'] === '') {
        $errores[] = 'El nombre del proveedor es requerido.';
    }

    if (empty($valores['filas'])) {
        $errores[] = 'Debe indicar al menos un desglose en la propuesta.';
    }

    $estado['valores'] = $valores;
    $estado['pedir_contacto'] = (
        $valores['nombre_representante'] !== ''
        || $valores['telefono'] !== ''
        || $valores['correo'] !== ''
    );

    if (!empty($errores)) {
        $estado['errores'] = $errores;

        return $estado;
    }

    try {
        $respuesta = cliente_wcf_participacion()->RegistrarParticipacion([
            'participacion' => [
                'IdSolicitud' => $id_solicitud,
                'CedulaJuridica' => $valores['cedula_juridica'],
                'NombreProveedor' => $valores['nombre_proveedor'],
                'NombreRepresentante' => $valores['nombre_representante'],
                'Telefono' => $valores['telefono'],
                'Correo' => $valores['correo'],
                'Desgloses' => ['ParticipacionDesglose' => $filas_soap]
            ]
        ]);

        $resultado = $respuesta->RegistrarParticipacionResult;
    } catch (Throwable $exception) {
        $estado['errores'][] = 'No fue posible enviar la participación. Intente de nuevo más tarde.';

        return $estado;
    }

    if (filter_var($resultado->Exitoso ?? false, FILTER_VALIDATE_BOOLEAN)) {
        wp_safe_redirect(add_query_arg(
            ['idSolicitud' => $id_solicitud, 'enviado' => 1],
            get_permalink()
        ));
        exit;
    }

    $estado['errores'][] = (string) ($resultado->Mensaje ?? 'No fue posible enviar la participación.');

    if (filter_var($resultado->RequiereDatosProveedor ?? false, FILTER_VALIDATE_BOOLEAN)) {
        $estado['pedir_contacto'] = true;
    }

    return $estado;
}

function dibujar_fila_participacion($indice, $fila)
{
    $meses = meses_participacion();
    $n = 'desgloses[' . $indice . ']';
    ?>
    <tr class="participacion-fila">
        <td>
            <select name="<?php echo esc_attr($n); ?>[mes]" required>
                <option value="">Mes</option>
                <?php foreach ($meses as $numero => $nombre): ?>
                    <option value="<?php echo esc_attr($numero); ?>" <?php selected((string) $fila['mes'], (string) $numero); ?>>
                        <?php echo esc_html($nombre); ?>
                    </option>
                <?php endforeach; ?>
            </select>
        </td>
        <td>
            <input type="number" name="<?php echo esc_attr($n); ?>[anio]" min="2000" max="2100" step="1" required
                   value="<?php echo esc_attr($fila['anio']); ?>" placeholder="Año">
        </td>
        <td>
            <input type="number" name="<?php echo esc_attr($n); ?>[horas]" min="0.01" max="999.99" step="0.01" required
                   value="<?php echo esc_attr($fila['horas']); ?>" placeholder="Horas">
        </td>
        <td>
            <input type="number" data-campo="monto" name="<?php echo esc_attr($n); ?>[monto]" min="0" step="0.01" required
                   value="<?php echo esc_attr($fila['monto']); ?>" placeholder="Monto">
        </td>
        <td><input type="text" data-campo="iva" readonly tabindex="-1" value="" placeholder="IVA 13%"></td>
        <td><input type="text" data-campo="total" readonly tabindex="-1" value="" placeholder="Total"></td>
        <td>
            <input type="text" name="<?php echo esc_attr($n); ?>[observaciones]" maxlength="500" required
                   value="<?php echo esc_attr($fila['observaciones']); ?>" placeholder="Observaciones">
        </td>
        <td>
            <input type="number" name="<?php echo esc_attr($n); ?>[porcentaje_cobro]" min="0" max="100" step="0.01" required
                   value="<?php echo esc_attr($fila['porcentaje_cobro']); ?>" placeholder="%">
        </td>
        <td>
            <button type="button" class="participacion-quitar" aria-label="Quitar desglose">&times;</button>
        </td>
    </tr>
    <?php
}

function shortcode_participar_solicitud()
{
    if (empty($GLOBALS['publicasa_ext4_estado'])) {
        return '';
    }

    $estado = $GLOBALS['publicasa_ext4_estado'];
    $solicitud = $estado['solicitud'];
    $id_solicitud = absint($solicitud->IdSolicitud);

    $url_detalle = url_detalle_participacion($id_solicitud);

    $pagina_excel = get_page_by_path('cargar-excel');
    $url_excel = $pagina_excel
        ? add_query_arg('idSolicitud', $id_solicitud, get_permalink($pagina_excel))
        : '#';

    $fila_vacia = [
        'mes' => '', 'anio' => wp_date('Y'), 'horas' => '', 'monto' => '',
        'observaciones' => '', 'porcentaje_cobro' => ''
    ];

    $valores = $estado['valores'] ?? [
        'cedula_juridica' => '', 'nombre_proveedor' => '', 'nombre_representante' => '',
        'telefono' => '', 'correo' => '', 'filas' => []
    ];

    $filas = $valores['filas'];

    if (empty($filas)) {
        $filas = apply_filters('publicasa_ext4_filas_precargadas', [], $id_solicitud);
    }

    if (empty($filas)) {
        $filas = [$fila_vacia];
    }

    ob_start();
    ?>

    <style>
        .participacion-tarjeta { width: 100%; max-width: 1100px; margin: 20px auto 40px; padding: 30px; box-sizing: border-box; background: #fff; border: 1px solid #e2e8f0; border-radius: 14px; box-shadow: 0 10px 30px rgba(15, 23, 42, .06); }
        .participacion-tarjeta h3 { margin: 28px 0 12px; color: #0f5f58; font-size: 18px; }
        .participacion-tarjeta h3:first-of-type { margin-top: 0; }
        .participacion-resumen { margin: 0 0 24px; color: #64748b; font-size: 16px; line-height: 1.6; }
        .participacion-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(240px, 1fr)); gap: 16px; }
        .participacion-campo label { display: block; margin-bottom: 6px; color: #334155; font-size: 14px; font-weight: 600; }
        .participacion-campo input, .participacion-tabla input, .participacion-tabla select { width: 100%; box-sizing: border-box; padding: 9px 10px; border: 1px solid #cbd5e1; border-radius: 6px; font-size: 14px; background: #fff; }
        .participacion-tabla input[readonly] { background: #f1f5f9; color: #475569; }
        .participacion-contenedor { overflow-x: auto; border: 1px solid #e2e8f0; border-radius: 10px; }
        .participacion-tabla { width: 100%; min-width: 980px; margin: 0; border-collapse: collapse; }
        .participacion-tabla thead { background: #147d73; }
        .participacion-tabla th { padding: 12px 10px; color: #fff; font-size: 14px; font-weight: 600; text-align: left; border: none; }
        .participacion-tabla td { padding: 8px 6px; border: none; border-bottom: 1px solid #e2e8f0; }
        .participacion-quitar { width: 32px; height: 32px; border: 1px solid #fecaca; border-radius: 6px; background: #fef2f2; color: #991b1b; font-size: 18px; line-height: 1; cursor: pointer; }
        .participacion-quitar:disabled { opacity: .4; cursor: not-allowed; }
        .participacion-agregar { margin-top: 12px; padding: 8px 14px; border: 1px dashed #147d73; border-radius: 6px; background: #f0fdfa; color: #147d73; font-size: 14px; cursor: pointer; }
        .participacion-acciones { display: flex; justify-content: flex-end; flex-wrap: wrap; gap: 12px; margin-top: 28px; }
        .participacion-boton { display: inline-block; padding: 10px 18px; border: 1px solid #147d73; border-radius: 6px; font-size: 15px; line-height: 1.5; text-decoration: none; cursor: pointer; }
        .participacion-boton-confirmar { background: #147d73; color: #fff; }
        .participacion-boton-secundario { background: #fff; color: #147d73; }
        .participacion-mensaje { margin-bottom: 20px; padding: 14px 18px; border-radius: 10px; font-size: 15px; line-height: 1.5; }
        .participacion-mensaje ul { margin: 0; padding-left: 20px; }
        .participacion-error { color: #991b1b; background: #fef2f2; border: 1px solid #fecaca; }
        .participacion-exito { color: #166534; background: #f0fdf4; border: 1px solid #bbf7d0; }
        .participacion-aviso { color: #92400e; background: #fffbeb; border: 1px solid #fde68a; }
        @media (max-width: 768px) { .participacion-tarjeta { margin-top: 10px; padding: 20px; } }
    </style>

    <section class="participacion-tarjeta">

        <p class="participacion-resumen">
            <strong><?php echo esc_html($solicitud->ConsecutivoOficio ?? ''); ?></strong>
            &mdash; <?php echo esc_html($solicitud->Titulo ?? ''); ?>
        </p>

        <?php if ($estado['enviado']): ?>

            <div class="participacion-acciones">
                <a class="participacion-boton participacion-boton-secundario" href="<?php echo esc_url($url_detalle); ?>">Volver al detalle</a>
                <a class="participacion-boton participacion-boton-confirmar" href="<?php echo esc_url(url_solicitudes_participacion()); ?>">Ver solicitudes publicadas</a>
            </div>

        <?php else: ?>

            <form method="post" action="<?php echo esc_url(add_query_arg('idSolicitud', $id_solicitud, get_permalink())); ?>" id="participacion-form" novalidate>

                <?php wp_nonce_field('publicasa_ext4_confirmar', 'publicasa_ext4_nonce'); ?>
                <input type="hidden" name="idSolicitud" value="<?php echo esc_attr($id_solicitud); ?>">
                <input type="hidden" name="publicasa_ext4_confirmar" value="1">

                <h3>Datos del proveedor</h3>

                <div class="participacion-grid">
                    <div class="participacion-campo">
                        <label for="cedula_juridica">Cédula jurídica</label>
                        <input type="text" id="cedula_juridica" name="cedula_juridica" required maxlength="13"
                               pattern="3-?[0-9]{3}-?[0-9]{6}" placeholder="3-101-123456"
                               title="Formato: 3-101-123456"
                               value="<?php echo esc_attr($valores['cedula_juridica']); ?>">
                    </div>
                    <div class="participacion-campo">
                        <label for="nombre_proveedor">Nombre del proveedor</label>
                        <input type="text" id="nombre_proveedor" name="nombre_proveedor" required maxlength="150"
                               value="<?php echo esc_attr($valores['nombre_proveedor']); ?>">
                    </div>
                </div>

                <?php if ($estado['pedir_contacto']): ?>
                    <h3>Proveedor nuevo: datos de contacto</h3>


                    <div class="participacion-grid">
                        <div class="participacion-campo">
                            <label for="nombre_representante">Nombre del representante</label>
                            <input type="text" id="nombre_representante" name="nombre_representante" required maxlength="150"
                                   value="<?php echo esc_attr($valores['nombre_representante']); ?>">
                        </div>
                        <div class="participacion-campo">
                            <label for="telefono">Teléfono</label>
                            <input type="tel" id="telefono" name="telefono" required maxlength="20"
                                   value="<?php echo esc_attr($valores['telefono']); ?>">
                        </div>
                        <div class="participacion-campo">
                            <label for="correo">Correo electrónico</label>
                            <input type="email" id="correo" name="correo" required maxlength="150"
                                   value="<?php echo esc_attr($valores['correo']); ?>">
                        </div>
                    </div>
                <?php endif; ?>

                <h3>Detalle de la propuesta</h3>

                <div class="participacion-contenedor">
                    <table class="participacion-tabla">
                        <thead>
                            <tr>
                                <th>Mes</th><th>Año</th><th>Horas</th><th>Monto</th>
                                <th>IVA (13%)</th><th>Total</th><th>Observaciones</th><th>% cobro</th><th></th>
                            </tr>
                        </thead>
                        <tbody id="participacion-filas">
                            <?php foreach (array_values($filas) as $indice => $fila): ?>
                                <?php dibujar_fila_participacion($indice, array_merge($fila_vacia, $fila)); ?>
                            <?php endforeach; ?>
                        </tbody>
                    </table>
                </div>

                <template id="participacion-plantilla">
                    <?php dibujar_fila_participacion('__i__', $fila_vacia); ?>
                </template>

                <button type="button" class="participacion-agregar" id="participacion-agregar">+ Agregar desglose</button>

                <div class="participacion-acciones">
                    <button
                        type="submit"
                        name="publicasa_ext5_abrir"
                        value="1"
                        class="participacion-boton participacion-boton-secundario"
                        formaction="<?php echo esc_url($url_excel); ?>"
                        formnovalidate
                        <?php disabled($url_excel, '#'); ?>
                    >Cargar Excel</button>
                    <a class="participacion-boton participacion-boton-secundario" href="<?php echo esc_url($url_detalle); ?>">Cancelar</a>
                    <button type="submit" class="participacion-boton participacion-boton-confirmar">Confirmar</button>
                </div>
            </form>

            <script>
                (function () {
                    var cuerpo = document.getElementById('participacion-filas');
                    var plantilla = document.getElementById('participacion-plantilla');
                    var siguiente = cuerpo.children.length;
                    var TASA_IVA = <?php echo (float) TASA_IVA_PARTICIPACION; ?>;

                    function dos(n) { return (Math.round(n * 100) / 100).toFixed(2); }

                    function recalcular(fila) {
                        var monto = parseFloat(fila.querySelector('[data-campo="monto"]').value);
                        var iva = fila.querySelector('[data-campo="iva"]');
                        var total = fila.querySelector('[data-campo="total"]');

                        if (isNaN(monto)) { iva.value = ''; total.value = ''; return; }

                        var valorIva = Math.round(monto * TASA_IVA * 100) / 100;
                        iva.value = dos(valorIva);
                        total.value = dos(monto + valorIva);
                    }

                    function actualizarBotonesQuitar() {
                        var botones = cuerpo.querySelectorAll('.participacion-quitar');
                        for (var i = 0; i < botones.length; i++) {
                            botones[i].disabled = (botones.length === 1);
                        }
                    }

                    for (var i = 0; i < cuerpo.children.length; i++) { recalcular(cuerpo.children[i]); }
                    actualizarBotonesQuitar();

                    cuerpo.addEventListener('input', function (e) {
                        if (e.target.getAttribute('data-campo') === 'monto') {
                            recalcular(e.target.closest('tr'));
                        }
                    });

                    cuerpo.addEventListener('click', function (e) {
                        if (e.target.classList.contains('participacion-quitar')) {
                            if (cuerpo.children.length > 1) {
                                e.target.closest('tr').remove();
                                actualizarBotonesQuitar();
                            }
                        }
                    });

                    document.getElementById('participacion-agregar').addEventListener('click', function () {
                        var contenedor = document.createElement('tbody');
                        contenedor.innerHTML = plantilla.innerHTML.replace(/__i__/g, siguiente++);
                        cuerpo.appendChild(contenedor.firstElementChild);
                        actualizarBotonesQuitar();
                    });
                })();
            </script>

        <?php endif; ?>

    </section>

    <script>
        (function () {
            var ERRORES_SERVIDOR = <?php echo wp_json_encode(array_values($estado['errores']), JSON_HEX_TAG | JSON_HEX_AMP | JSON_HEX_APOS | JSON_HEX_QUOT); ?>;
            var ENVIADO = <?php echo $estado['enviado'] ? 'true' : 'false'; ?>;
            var URL_DETALLE = <?php echo wp_json_encode(esc_url_raw($url_detalle)); ?>;
            var URL_SOLICITUDES = <?php echo wp_json_encode(esc_url_raw(url_solicitudes_participacion())); ?>;
            var COLOR = '#147d73';

            function listaHtml(mensajes) {
                var lista = document.createElement('ul');
                lista.style.textAlign = 'left';
                lista.style.margin = '0';
                lista.style.paddingLeft = '20px';

                mensajes.forEach(function (mensaje) {
                    var li = document.createElement('li');
                    li.textContent = mensaje;
                    lista.appendChild(li);
                });

                return lista;
            }

            function mostrarErrores(mensajes, icono, titulo, alCerrar) {
                if (typeof Swal === 'undefined') {
                    alert(mensajes.join('\n'));
                    if (alCerrar) { alCerrar(); }
                    return;
                }

                Swal.fire({
                    title: titulo,
                    html: listaHtml(mensajes),
                    icon: icono,
                    confirmButtonText: 'Aceptar',
                    confirmButtonColor: COLOR
                }).then(function () {
                    if (alCerrar) { alCerrar(); }
                });
            }

            function validarFormulario(form) {
                var mensajes = [];
                var primerInvalido = null;

                function marcar(campo) {
                    if (!primerInvalido) { primerInvalido = campo; }
                }

                function revisar(id, etiqueta, mensajeFormato) {
                    var campo = form.querySelector('#' + id);
                    if (!campo) { return; }

                    if (campo.value.trim() === '') {
                        mensajes.push(etiqueta + ' es requerido.');
                        marcar(campo);
                    } else if (!campo.validity.valid) {
                        mensajes.push(mensajeFormato || (etiqueta + ' no es válido.'));
                        marcar(campo);
                    }
                }

                revisar('cedula_juridica', 'La cédula jurídica', 'La cédula jurídica no tiene un formato válido (ejemplo: 3-101-123456).');
                revisar('nombre_proveedor', 'El nombre del proveedor');
                revisar('nombre_representante', 'El nombre del representante');
                revisar('telefono', 'El teléfono');
                revisar('correo', 'El correo electrónico', 'El correo electrónico no tiene un formato válido.');

                var filas = form.querySelectorAll('#participacion-filas tr');
                var nombres = { mes: 'Mes', anio: 'Año', horas: 'Horas', monto: 'Monto', observaciones: 'Observaciones', porcentaje_cobro: '% cobro' };

                filas.forEach(function (fila, i) {
                    var faltan = [];
                    var invalidos = [];

                    Object.keys(nombres).forEach(function (clave) {
                        var campo = fila.querySelector('[name$="[' + clave + ']"]');
                        if (!campo) { return; }

                        if (campo.value.trim() === '') {
                            faltan.push(nombres[clave]);
                            marcar(campo);
                        } else if (!campo.validity.valid) {
                            invalidos.push(nombres[clave]);
                            marcar(campo);
                        }
                    });

                    if (faltan.length) {
                        mensajes.push('Desglose ' + (i + 1) + ': falta completar ' + faltan.join(', ') + '.');
                    }
                    if (invalidos.length) {
                        mensajes.push('Desglose ' + (i + 1) + ': valor fuera del rango permitido en ' + invalidos.join(', ') + '.');
                    }
                });

                return { mensajes: mensajes, primerInvalido: primerInvalido };
            }

            document.addEventListener('DOMContentLoaded', function () {
                if (ENVIADO && typeof Swal !== 'undefined') {
                    Swal.fire({
                        title: 'Participación enviada',
                        text: 'Su participación fue enviada correctamente.',
                        icon: 'success',
                        showCancelButton: true,
                        confirmButtonText: 'Ver solicitudes publicadas',
                        cancelButtonText: 'Volver al detalle',
                        confirmButtonColor: COLOR,
                        allowOutsideClick: false
                    }).then(function (resultado) {
                        window.location.href = resultado.isConfirmed ? URL_SOLICITUDES : URL_DETALLE;
                    });
                    return;
                }

                if (ERRORES_SERVIDOR.length) {
                    mostrarErrores(ERRORES_SERVIDOR, 'error', 'No se pudo enviar la participación');
                }

                var form = document.getElementById('participacion-form');
                if (!form) { return; }

                form.addEventListener('submit', function (e) {
                    
                    if (e.submitter && e.submitter.hasAttribute('formnovalidate')) { return; }

                    var resultado = validarFormulario(form);

                    if (resultado.mensajes.length) {
                        e.preventDefault();
                        mostrarErrores(resultado.mensajes, 'warning', 'Campos obligatorios', function () {
                            if (resultado.primerInvalido) { resultado.primerInvalido.focus(); }
                        });
                    }
                });
            });
        })();
    </script>

    <?php

    return ob_get_clean();
}

add_shortcode('participar_solicitud', 'shortcode_participar_solicitud');