<?php
declare(strict_types=1);

namespace PublicaSA\ConsultaParticipaciones\Repositories;

use PublicaSA\ConsultaParticipaciones\Core\ClienteSoapSeguro;

final class ParticipacionesCliente
{
    public function consultar(string $cedulaJuridica): array
    {
        $respuesta = (new ClienteSoapSeguro())->ejecutar(
            'ConsultarParticipaciones',
            [['cedulaJuridica' => $cedulaJuridica]]
        );
        $resultado = $respuesta->ConsultarParticipacionesResult ?? null;

        if (!is_object($resultado) || !isset($resultado->EsCorrecto)) {
            throw new \RuntimeException('Respuesta WCF no válida para la consulta de participaciones.');
        }

        $solicitudes = $resultado->Solicitudes->SolicitudParticipacion ?? [];

        return [
            'correcto' => (bool) $resultado->EsCorrecto,
            'mensaje' => (string) ($resultado->Mensaje ?? ''),
            'solicitudes' => is_array($solicitudes) ? $solicitudes : [$solicitudes]
        ];
    }

    public function registrarError(string $referencia, string $mensaje): bool
    {
        $respuesta = (new ClienteSoapSeguro())->ejecutar(
            'RegistrarErrorConsultaParticipaciones',
            [['referencia' => $referencia, 'mensaje' => $mensaje]]
        );

        return ($respuesta->RegistrarErrorConsultaParticipacionesResult ?? false) === true;
    }

}
