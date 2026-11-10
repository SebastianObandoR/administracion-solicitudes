using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using AdministracionSolicitudes.WebService.AccesoDatos;
using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public class ImportacionPropuestaServicio : IImportacionPropuestaServicio
    {
        private readonly IErrorImportacionRepositorio _errores;

        public ImportacionPropuestaServicio(string cadenaConexion)
            : this(new ErrorImportacionRepositorio(cadenaConexion))
        {
        }

        internal ImportacionPropuestaServicio(IErrorImportacionRepositorio errores)
        {
            if (errores == null)
            {
                throw new ArgumentNullException("errores");
            }

            _errores = errores;
        }

        public ResultadoCargaPropuesta CargarDesglosesPropuesta(List<FilaDesgloseExcel> filas)
        {
            var resultado = new ResultadoCargaPropuesta();

            try
            {
                if (filas == null || filas.Count == 0)
                {
                    resultado.Errores.Add("Todos los datos son requeridos.");
                    return resultado;
                }

                var normalizadas = new List<FilaDesgloseExcel>();
                var periodos = new HashSet<string>();

                foreach (var fila in filas)
                {
                    if (fila == null)
                    {
                        resultado.Errores.Add("Todos los datos del desglose son requeridos.");
                        continue;
                    }

                    var normalizada = Validar(fila, resultado.Errores);
                    if (normalizada == null)
                    {
                        continue;
                    }

                    string periodo = normalizada.Mes + "/" + normalizada.Anio;
                    if (!periodos.Add(periodo))
                    {
                        resultado.Errores.Add(
                            "Fila " + fila.NumeroFila + ": no puede repetir el mismo mes y año."
                        );
                    }

                    normalizadas.Add(normalizada);
                }

                if (resultado.Errores.Count > 0)
                {
                    return resultado;
                }

                resultado.Desgloses = normalizadas;
                resultado.EsCorrecto = true;

                return resultado;
            }
            catch (Exception excepcion)
            {
                string referencia = Guid.NewGuid().ToString("N");
                RegistrarErrorSeguro(referencia + ": " + excepcion);
                resultado.Desgloses.Clear();
                resultado.Errores.Clear();
                resultado.Errores.Add("No se pudo procesar el Excel. Referencia: " + referencia);

                return resultado;
            }
        }

        public bool RegistrarErrorImportacion(string referencia, string mensaje)
        {
            return RegistrarErrorSeguro(referencia + ": " + mensaje);
        }

        private bool RegistrarErrorSeguro(string mensaje)
        {
            try
            {
                _errores.Registrar(mensaje, null);
                return true;
            }
            catch (Exception excepcion)
            {
                Trace.TraceError(
                    "No se pudo registrar el error de EXT5: {0}",
                    excepcion.Message
                );

                return false;
            }
        }


        private static FilaDesgloseExcel Validar(FilaDesgloseExcel fila, List<string> errores)
        {
            string prefijo = "Fila " + fila.NumeroFila + ": ";
            int erroresIniciales = errores.Count;
            var campos = new Dictionary<string, string>
            {
                { "Mes", fila.Mes },
                { "Año", fila.Anio },
                { "Horas", fila.Horas },
                { "Monto", fila.Monto },
                { "IVA", fila.Iva },
                { "Total", fila.Total },
                { "Observaciones", fila.Observaciones },
                { "Porcentaje de cobro", fila.PorcentajeCobro }
            };

            foreach (var campo in campos)
            {
                if (string.IsNullOrWhiteSpace(campo.Value))
                {
                    errores.Add(prefijo + campo.Key + " es requerido.");
                }
            }

            if (errores.Count != erroresIniciales)
            {
                return null;
            }

            int mes;
            int anio;
            decimal horas;
            decimal monto;
            decimal iva;
            decimal total;
            decimal porcentaje;

            if (!int.TryParse(fila.Mes, out mes) || mes < 1 || mes > 12)
            {
                errores.Add(prefijo + "Mes debe ser un entero entre 1 y 12.");
            }

            if (!int.TryParse(fila.Anio, out anio) || anio < 2000 || anio > 2100)
            {
                errores.Add(prefijo + "Año debe ser un entero entre 2000 y 2100, como requiere EXT4.");
            }

            bool horasValidas = Numero(fila.Horas, out horas);
            bool montoValido = Numero(fila.Monto, out monto);
            bool ivaValido = Numero(fila.Iva, out iva);
            bool totalValido = Numero(fila.Total, out total);
            bool porcentajeValido = Numero(fila.PorcentajeCobro, out porcentaje);

            if (
                !horasValidas
                || horas <= 0
                || horas > 999.99m
                || Redondear(horas) < 0.01m
            )
            {
                errores.Add(prefijo + "Horas debe ser un número entre 0.01 y 999.99.");
            }

            if (!montoValido || monto < 0)
            {
                errores.Add(prefijo + "Monto debe ser un número no negativo.");
            }

            if (!ivaValido || iva < 0)
            {
                errores.Add(prefijo + "IVA debe ser un número no negativo.");
            }

            if (!totalValido || total < 0)
            {
                errores.Add(prefijo + "Total debe ser un número no negativo.");
            }

            if (!porcentajeValido || porcentaje < 0 || porcentaje > 100)
            {
                errores.Add(prefijo + "Porcentaje de cobro debe ser un número entre 0 y 100.");
            }

            string observaciones = fila.Observaciones.Trim();
            if (observaciones.Length > 500)
            {
                errores.Add(prefijo + "Observaciones no puede superar 500 caracteres, como requiere EXT4.");
            }

            if (errores.Count != erroresIniciales)
            {
                return null;
            }

            monto = Redondear(monto);

            if (monto > 99999999.99m)
            {
                errores.Add(prefijo + "El total excede la capacidad de almacenamiento utilizada por EXT4.");
                return null;
            }

            decimal ivaEsperado = Redondear(monto * 0.13m);
            decimal totalEsperado = monto + ivaEsperado;

            if (Redondear(iva) != ivaEsperado)
            {
                errores.Add(prefijo + "IVA debe ser el 13 % del monto, redondeado a dos decimales.");
            }

            if (Redondear(total) != totalEsperado)
            {
                errores.Add(prefijo + "Total debe ser monto más IVA, con dos decimales.");
            }

            if (totalEsperado > 99999999.99m)
            {
                errores.Add(prefijo + "El total excede la capacidad de almacenamiento utilizada por EXT4.");
            }

            if (errores.Count != erroresIniciales)
            {
                return null;
            }

            return new FilaDesgloseExcel
            {
                NumeroFila = fila.NumeroFila,
                Mes = mes.ToString(CultureInfo.InvariantCulture),
                Anio = anio.ToString(CultureInfo.InvariantCulture),
                Horas = Formatear(horas),
                Monto = Formatear(monto),
                Iva = Formatear(ivaEsperado),
                Total = Formatear(totalEsperado),
                Observaciones = observaciones,
                PorcentajeCobro = Formatear(porcentaje)
            };
        }

        private static decimal Redondear(decimal valor)
        {
            return Math.Round(valor, 2, MidpointRounding.AwayFromZero);
        }

        private static string Formatear(decimal valor)
        {
            return Redondear(valor).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private static bool Numero(string texto, out decimal valor)
        {
            return decimal.TryParse(
                texto.Trim().Replace(',', '.'),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out valor
            );
        }
    }
}
