using AdministracionSolicitudes.WebService.AccesoDatos;
using AdministracionSolicitudes.WebService.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public class ParticipacionLogica
    {
        private const decimal TasaIva = 0.13m;
        private const decimal MaximoDecimal10_2 = 99999999.99m;

        private static readonly Regex CedulaRegex = new Regex(@"^3\d{9}$");
        private static readonly Regex CorreoRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private static readonly Regex TelefonoRegex = new Regex(@"^[0-9+\-\s()]{8,20}$");

        private readonly ParticipacionAccesoDatos _accesoDatos;

        public ParticipacionLogica()
        {
            _accesoDatos = new ParticipacionAccesoDatos();
        }

        public ResultadoParticipacion RegistrarParticipacion(ParticipacionProveedor p)
        {
            try
            {
                if (p == null)
                {
                    return Error("No se recibieron datos de la participacion");
                }

                if (!_accesoDatos.SolicitudPublicadaVigente(p.IdSolicitud))
                {
                    return Error("La solicitud no esta publicada o ya vencio");
                }

                var digitos = Regex.Replace(p.CedulaJuridica ?? string.Empty, @"[\s-]", string.Empty);

                if (!CedulaRegex.IsMatch(digitos))
                {
                    return Error("La cedula juridica no tiene un formato valido");
                }

                p.CedulaJuridica = digitos.Substring(0, 1) + "-" + digitos.Substring(1, 3) + "-" + digitos.Substring(4);
                p.NombreProveedor = (p.NombreProveedor ?? string.Empty).Trim();

                if (p.NombreProveedor.Length == 0 || p.NombreProveedor.Length > 150)
                {
                    return Error("El nombre del proveedor es requerido");
                }

                var errorDesgloses = ValidarYCalcularDesgloses(p.Desgloses);
                if (errorDesgloses != null)
                {
                    return Error(errorDesgloses);
                }

                var idProveedor = _accesoDatos.ObtenerIdProveedor(p.CedulaJuridica);

                if (idProveedor.HasValue)
                {
                    if (_accesoDatos.ExisteParticipacion(p.IdSolicitud, idProveedor.Value))
                    {
                        return Error("Este proveedor ya envio una participacion para esta solicitud");
                    }
                }
                else
                {
                    p.NombreRepresentante = (p.NombreRepresentante ?? string.Empty).Trim();
                    p.Telefono = (p.Telefono ?? string.Empty).Trim();
                    p.Correo = (p.Correo ?? string.Empty).Trim();

                    if (p.NombreRepresentante.Length == 0 && p.Telefono.Length == 0 && p.Correo.Length == 0)
                    {
                        return new ResultadoParticipacion
                        {
                            Exitoso = false,
                            RequiereDatosProveedor = true,
                            Mensaje = "El proveedor no existe coloque el nombre del representante, un telefono y un correo de contacto para crearlo"
                        };
                    }

                    if (p.NombreRepresentante.Length == 0 || p.NombreRepresentante.Length > 150)
                    {
                        return ErrorProveedorNuevo("El nombre del representante es requerido");
                    }

                    if (!TelefonoRegex.IsMatch(p.Telefono))
                    {
                        return ErrorProveedorNuevo("El telefono no es valido.");
                    }

                    if (p.Correo.Length > 150 || !CorreoRegex.IsMatch(p.Correo))
                    {
                        return ErrorProveedorNuevo("El correo electronico no es valido");
                    }
                }

                var idParticipacion = _accesoDatos.RegistrarParticipacion(p);

                return new ResultadoParticipacion
                {
                    Exitoso = true,
                    IdParticipacion = idParticipacion,
                    Mensaje = "Su participación fue enviada correctamente"
                };
            }
            catch (Exception ex)
            {
                try { _accesoDatos.RegistrarError(ex.ToString()); } catch { }

                return Error("Ocurrio un error al guardar la participacion");
            }
        }

        private static string ValidarYCalcularDesgloses(List<ParticipacionDesglose> desgloses)
        {
            if (desgloses == null || desgloses.Count == 0)
            {
                return "Debe indicar al menos un desglose en la propuesta";
            }

            if (desgloses.GroupBy(d => new { d.Mes, d.Anio }).Any(g => g.Count() > 1))
            {
                return "No puede repetir el mismo mes y año en el desglose";
            }

            for (var i = 0; i < desgloses.Count; i++)
            {
                var d = desgloses[i];
                var fila = "Desglose " + (i + 1) + ": ";

                if (d.Mes < 1 || d.Mes > 12) return fila + "el mes debe estar entre 1 y 12";
                if (d.Anio < 2000 || d.Anio > 2100) return fila + "el año no es válido";
                if (d.Horas <= 0 || d.Horas > 999.99m) return fila + "las horas deben ser mayores a 0";
                if (d.Monto < 0) return fila + "el monto no puede ser negativo";
                if (d.PorcentajeCobro < 0 || d.PorcentajeCobro > 100) return fila + "el porcentaje de cobro debe estar entre 0 y 100";

                d.Observaciones = (d.Observaciones ?? string.Empty).Trim();
                if (d.Observaciones.Length == 0 || d.Observaciones.Length > 500)
                {
                    return fila + "las observaciones son requeridas";
                }

                d.Monto = Math.Round(d.Monto, 2, MidpointRounding.AwayFromZero);
                d.Iva = Math.Round(d.Monto * TasaIva, 2, MidpointRounding.AwayFromZero);
                d.Total = d.Monto + d.Iva;
                d.Horas = Math.Round(d.Horas, 2, MidpointRounding.AwayFromZero);
                d.PorcentajeCobro = Math.Round(d.PorcentajeCobro, 2, MidpointRounding.AwayFromZero);

                if (d.Total > MaximoDecimal10_2) return fila + "el monto es demasiado grande";
            }

            return null;
        }

        private static ResultadoParticipacion Error(string mensaje)
        {
            return new ResultadoParticipacion { Exitoso = false, Mensaje = mensaje };
        }

        private static ResultadoParticipacion ErrorProveedorNuevo(string mensaje)
        {
            return new ResultadoParticipacion
            {
                Exitoso = false,
                RequiereDatosProveedor = true,
                Mensaje = mensaje
            };
        }
    }
}