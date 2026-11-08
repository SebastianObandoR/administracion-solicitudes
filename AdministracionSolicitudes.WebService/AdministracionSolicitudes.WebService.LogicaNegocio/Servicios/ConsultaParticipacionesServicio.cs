using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AdministracionSolicitudes.WebService.AccesoDatos;
using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public class ConsultaParticipacionesServicio : IConsultaParticipacionesServicio
    {
        private readonly IParticipacionesRepositorio _participaciones;
        private readonly ISeguimientoParticipacionesRepositorio _seguimiento;

        public ConsultaParticipacionesServicio(string cadenaConexion)
            : this(
                new ParticipacionesRepositorio(cadenaConexion),
                new SeguimientoParticipacionesRepositorio(cadenaConexion)
            )
        {
        }

        internal ConsultaParticipacionesServicio(
            IParticipacionesRepositorio participaciones,
            ISeguimientoParticipacionesRepositorio seguimiento
        )
        {
            if (participaciones == null)
            {
                throw new ArgumentNullException("participaciones");
            }

            if (seguimiento == null)
            {
                throw new ArgumentNullException("seguimiento");
            }

            _participaciones = participaciones;
            _seguimiento = seguimiento;
        }

        public ResultadoConsultaParticipaciones Consultar(string cedulaJuridica)
        {
            var resultado = new ResultadoConsultaParticipaciones();

            if (string.IsNullOrWhiteSpace(cedulaJuridica))
            {
                resultado.Mensaje = "La cédula jurídica es requerida.";
                return resultado;
            }

            try
            {
                string cedula = Regex.Replace(cedulaJuridica.Trim(), @"[\s-]", string.Empty);
                if (cedula.Length == 0)
                {
                    resultado.Mensaje = "La cédula jurídica es requerida.";
                    return resultado;
                }

                bool existe = _participaciones.ExisteProveedor(cedula);
                if (existe)
                {
                    resultado.Solicitudes = _participaciones.ObtenerSolicitudes(cedula);
                    if (resultado.Solicitudes == null)
                    {
                        throw new InvalidOperationException("El repositorio devolvió una lista nula.");
                    }
                }

                _seguimiento.RegistrarConsulta();
                resultado.EsCorrecto = true;

                if (!existe)
                {
                    resultado.Mensaje = "Proveedor no existe.";
                }
                else if (resultado.Solicitudes.Count == 0)
                {
                    resultado.Mensaje = "Datos no encontrados";
                }

                return resultado;
            }
            catch (Exception excepcion)
            {
                RegistrarSeguro(excepcion.ToString());
                resultado.Solicitudes = new List<SolicitudParticipacion>();
                resultado.EsCorrecto = false;
                resultado.Mensaje = "No se pudo realizar la consulta.";

                return resultado;
            }
        }

        public bool RegistrarErrorConsulta(string referencia, string mensaje)
        {
            return RegistrarSeguro(referencia + ": " + mensaje);
        }

        private bool RegistrarSeguro(string mensaje)
        {
            try
            {
                _seguimiento.RegistrarError(mensaje);
                return true;
            }
            catch (Exception excepcion)
            {
                Trace.TraceError("Falló errorlog de EXT6: {0}", excepcion.Message);
                return false;
            }
        }
    }
}
