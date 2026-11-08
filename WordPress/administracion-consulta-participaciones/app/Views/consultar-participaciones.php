<?php
if (!defined('ABSPATH')) {
    exit;
}
$claseMensaje = $esError ? 'publicasa-mensaje-error' : 'publicasa-mensaje-vacio';
?>

<div class="publicasa-participaciones">
    <p class="publicasa-subtitulo">
        Indique la cédula jurídica del proveedor para consultar sus participaciones en solicitudes.
    </p>

    <form method="post" action="<?php echo esc_url(get_permalink()); ?>">
        <?php wp_nonce_field('consultar_participaciones'); ?>

        <div class="publicasa-campo">
            <label for="cedula-juridica">Cédula jurídica</label>
            <input
                id="cedula-juridica"
                type="text"
                name="cedula_juridica"
                value="<?php echo esc_attr($cedulaJuridica); ?>"
                required
            >
        </div>

        <div class="publicasa-acciones">
            <button class="publicasa-boton" type="submit">Consultar</button>
        </div>
    </form>

    <?php if ($mensaje !== ''): ?>
        <div
            class="publicasa-mensaje <?php echo esc_attr($claseMensaje); ?>"
            role="<?php echo $esError ? 'alert' : 'status'; ?>"
        >
            <?php echo esc_html($mensaje); ?>
        </div>
    <?php endif; ?>

    <?php if ($solicitudes): ?>
        <div class="publicasa-tabla-contenedor">
            <table class="publicasa-tabla">
                <thead>
                    <tr>
                        <th scope="col">Consecutivo de oficio</th>
                        <th scope="col">Título</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($solicitudes as $solicitud): ?>
                        <tr>
                            <td><?php echo esc_html((string) $solicitud->ConsecutivoOficio); ?></td>
                            <td><?php echo esc_html((string) $solicitud->Titulo); ?></td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        </div>
    <?php endif; ?>
</div>
