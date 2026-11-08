<?php
declare(strict_types=1);

namespace PublicaSA\CargaPropuestas\Core;

final class ClienteSoapSeguro
{

    public function ejecutar(string $wsdl, string $operacion, array $argumentos = []): object
    {
        if (!class_exists(\SoapClient::class)) {
            throw new \RuntimeException('La extensión SOAP de PHP no está habilitada.');
        }

        set_error_handler(static function (int $nivel, string $mensaje, string $archivo, int $linea): never
        {
            throw new \ErrorException($mensaje, 0, $nivel, $archivo, $linea);
        }, E_WARNING | E_NOTICE);
        try {
            $cliente = new \SoapClient($wsdl, [
                'exceptions' => true,
                'trace' => false,
                'cache_wsdl' => WSDL_CACHE_NONE,
            ]);
            $respuesta = $cliente->__soapCall($operacion, $argumentos);
            if (!is_object($respuesta)) {
                throw new \RuntimeException('El servicio devolvió una respuesta no válida.');
            }

            return $respuesta;
        } finally {
            restore_error_handler();
        }
    }
}
