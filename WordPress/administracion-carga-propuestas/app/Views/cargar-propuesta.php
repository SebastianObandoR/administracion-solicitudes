<?php
if (!defined('ABSPATH')) {
    exit;
}

$columnas = [
    'Mes' => 'Mes',
    'Anio' => 'Año',
    'Horas' => 'Horas',
    'Monto' => 'Monto',
    'Iva' => 'IVA',
    'Total' => 'Total',
    'Observaciones' => 'Observaciones',
    'PorcentajeCobro' => 'Porcentaje de cobro'
];
?>

<div class="publicasa-propuesta-excel">
    <p class="publicasa-subtitulo">
        Seleccione el Excel con el desglose de su propuesta. Todos los datos son requeridos.
    </p>

    <?php if ($errores): ?>
        <div class="publicasa-mensaje publicasa-mensaje-error" role="alert">
            <ul>
                <?php foreach ($errores as $error): ?>
                    <li><?php echo esc_html((string) $error); ?></li>
                <?php endforeach; ?>
            </ul>
        </div>
    <?php endif; ?>

    <?php if ($mensaje): ?>
        <div class="publicasa-mensaje publicasa-mensaje-exito" role="status">
            <?php echo esc_html($mensaje); ?>
        </div>
    <?php endif; ?>

    <form
        method="post"
        enctype="multipart/form-data"
        action="<?php echo esc_url(get_permalink()); ?>"
    >
        <?php wp_nonce_field('cargar_propuesta_excel'); ?>
        <?php $id = $_POST['idSolicitud'] ?? $_GET['idSolicitud'] ?? ''; ?>

        <?php if (is_string($id) && ctype_digit($id)): ?>
            <input
                type="hidden"
                name="idSolicitud"
                value="<?php echo esc_attr($id); ?>"
            >
        <?php endif; ?>

        <div class="publicasa-campo">
            <label for="archivo-excel">Archivo Excel</label>
            <input
                id="archivo-excel"
                type="file"
                name="archivo_excel"
                accept=".xlsx,.xlsm"
                required
            >
        </div>

        <div class="publicasa-acciones">
            <button class="publicasa-boton" type="submit">Cargar archivo</button>
        </div>
    </form>

    <div
        class="publicasa-tabla-contenedor"
        tabindex="0"
        role="region"
        aria-label="Desglose de la propuesta"
    >
        <table class="publicasa-tabla">
            <thead>
                <tr>
                    <?php foreach ($columnas as $titulo): ?>
                        <th scope="col"><?php echo esc_html($titulo); ?></th>
                    <?php endforeach; ?>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($desgloses as $desglose): ?>
                    <tr>
                        <?php foreach ($columnas as $campo => $titulo): ?>
                            <td><?php echo esc_html((string) ($desglose->$campo ?? '')); ?></td>
                        <?php endforeach; ?>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    </div>
</div>
