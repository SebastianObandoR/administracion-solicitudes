<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Controllers;

use PublicaSA\CargaPropuestas\Core\{
    ArchivoExcelInvalido,
    ContextoCargaPropuesta,
    RegistroErrorCarga,
    LectorXlsx,
    MapeadorColumnasExcel
};
use PublicaSA\CargaPropuestas\Repositories\ImportacionPropuestaCliente;

final class CargaPropuestaControlador
{
    private array $errores = [];
    private array $desgloses = [];
    private string $mensaje = '';

    public function __construct(
        private string $directorio,
        private ContextoCargaPropuesta $contexto
    )
    {
    }

    public function procesar(): void
    {
        if (
            !is_singular()
            || !has_shortcode(
                (string) get_post_field('post_content', get_queried_object_id()),
                'cargar_propuesta_excel'
            )
        ) {
            return;
        }

        nocache_headers();
        if (!defined('DONOTCACHEPAGE')) {
            define('DONOTCACHEPAGE', true);
        }

        try {
            $idSolicitud = absint($_POST['idSolicitud'] ?? $_GET['idSolicitud'] ?? 0);
            if ($idSolicitud === 0) {
                throw new ArchivoExcelInvalido('Ingrese desde la opción Cargar Excel de la participación.');
            }

            $this->contexto->cargar();
            if (($_SERVER['REQUEST_METHOD'] ?? '') !== 'POST') {
                return;
            }

            if (isset($_POST['publicasa_ext5_abrir'])) {
                $this->conservarFormulario($idSolicitud);
                $this->redirigir(add_query_arg('idSolicitud', $idSolicitud, get_permalink()));
            }

            $nonce = $_POST['_wpnonce'] ?? '';
            if (!is_string($nonce) || !wp_verify_nonce(wp_unslash($nonce), 'cargar_propuesta_excel')) {
                throw new ArchivoExcelInvalido('El formulario venció. Recargue la página.');
            }
            $archivo = $_FILES['archivo_excel'] ?? [];
            if (!is_array($archivo)) {
                throw new ArchivoExcelInvalido('Seleccione un archivo Excel.');
            }

            $errorCarga = $archivo['error'] ?? UPLOAD_ERR_NO_FILE;
            if (in_array(
                $errorCarga,
                [
                    UPLOAD_ERR_PARTIAL,
                    UPLOAD_ERR_NO_TMP_DIR,
                    UPLOAD_ERR_CANT_WRITE,
                    UPLOAD_ERR_EXTENSION
                ],
                true
            )) {
                throw new \RuntimeException('Fallo técnico de carga PHP. Código: ' . $errorCarga);
            }

            if ($errorCarga !== UPLOAD_ERR_OK) {
                throw new ArchivoExcelInvalido(
                    'No fue posible recibir el archivo Excel. Selecciónelo nuevamente.'
                );
            }

            if (
                !is_string($archivo['name'] ?? null)
                || !in_array(
                    strtolower(pathinfo($archivo['name'], PATHINFO_EXTENSION)),
                    ['xlsx', 'xlsm'],
                    true
                )
            ) {
                throw new ArchivoExcelInvalido('Seleccione un archivo Excel en formato .xlsx o .xlsm.');
            }

            $ruta = $archivo['tmp_name'] ?? '';
            if (!is_string($ruta) || !is_uploaded_file($ruta)) {
                throw new ArchivoExcelInvalido('El archivo no se cargó correctamente.');
            }

            $filas = (new LectorXlsx())->leer($ruta);
            $datos = (new MapeadorColumnasExcel())->convertir($filas);
            foreach ($datos as &$dato) {
                $dato['Observaciones'] = sanitize_text_field($dato['Observaciones']);
            }
            unset($dato);

            $resultado = (new ImportacionPropuestaCliente())->cargar($datos);
            if (!$resultado['correcto']) {
                $this->errores = $resultado['errores'];
                return;
            }

            $this->volverAParticipacion($idSolicitud, $resultado['desgloses']);
        } catch (ArchivoExcelInvalido $excepcion) {
            $this->errores = [$excepcion->getMessage()];
        } catch (\Throwable $excepcion) {
            $referencia = RegistroErrorCarga::registrar($excepcion);
            $this->errores = ['No se pudo cargar el Excel. Referencia: ' . $referencia];
        }
    }


    private function conservarFormulario(int $idSolicitud): void
    {
        $nonce = $_POST['publicasa_ext4_nonce'] ?? '';
        if (!is_string($nonce) || !wp_verify_nonce(wp_unslash($nonce), 'publicasa_ext4_confirmar')) {
            throw new ArchivoExcelInvalido('El formulario venció. Vuelva a la participación.');
        }

        $valores = [];

        foreach ([
            'cedula_juridica',
            'nombre_proveedor',
            'nombre_representante',
            'telefono',
            'correo'
        ] as $campo) {
            $valor = $_POST[$campo] ?? '';
            $valores[$campo] = is_string($valor)
                ? sanitize_text_field(wp_unslash($valor))
                : '';
        }

        $valores['filas'] = [];
        $filas = $_POST['desgloses'] ?? [];

        foreach (is_array($filas) ? wp_unslash($filas) : [] as $fila) {
            if (!is_array($fila)) {
                continue;
            }

            $limpia = [];
            foreach (['mes', 'anio', 'horas', 'monto', 'observaciones', 'porcentaje_cobro'] as $campo) {
                $valor = $fila[$campo] ?? '';
                $limpia[$campo] = is_string($valor) ? sanitize_text_field($valor) : '';
            }

            $valores['filas'][] = $limpia;
        }

        $this->contexto->guardar($idSolicitud, [
            'valores' => $valores,
            'pedir_contacto' => (
                $valores['nombre_representante'] !== ''
                || $valores['telefono'] !== ''
                || $valores['correo'] !== ''
            )
        ]);
    }

    private function volverAParticipacion(int $idSolicitud, array $desgloses): never
    {
        $pagina = get_page_by_path('participar');
        if (!$pagina) {
            throw new \RuntimeException('No se encontró la página participar de EXT4.');
        }

        $datos = $this->contexto->obtener($idSolicitud);
        $valores = $datos['valores'] ?? [
            'cedula_juridica' => '',
            'nombre_proveedor' => '',
            'nombre_representante' => '',
            'telefono' => '',
            'correo' => ''
        ];
        $valores['filas'] = [];

        foreach ($desgloses as $desglose) {
            $valores['filas'][] = [
                'mes' => (string) $desglose->Mes,
                'anio' => (string) $desglose->Anio,
                'horas' => (string) $desglose->Horas,
                'monto' => (string) $desglose->Monto,
                'observaciones' => (string) $desglose->Observaciones,
                'porcentaje_cobro' => (string) $desglose->PorcentajeCobro
            ];
        }

        $this->contexto->guardar($idSolicitud, [
            'valores' => $valores,
            'pedir_contacto' => $datos['pedir_contacto'] ?? false
        ]);

        $this->redirigir(add_query_arg('idSolicitud', $idSolicitud, get_permalink($pagina)));
    }

    private function redirigir(string $url): never
    {
        if (!wp_safe_redirect($url)) {
            throw new \RuntimeException('No fue posible volver al formulario de participación.');
        }

        exit;
    }

    public function mostrar(): string
    {
        $errores = $this->errores;
        $desgloses = $this->desgloses;
        $mensaje = $this->mensaje;
        ob_start();
        require $this->directorio . '/app/Views/cargar-propuesta.php';
        return (string) ob_get_clean();
    }
}
