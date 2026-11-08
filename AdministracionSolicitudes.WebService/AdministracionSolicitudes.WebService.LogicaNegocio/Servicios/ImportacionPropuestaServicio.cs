using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using AdministracionSolicitudes.WebService.AccesoDatos;
using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.LogicaNegocio
{
    public class ImportacionPropuestaServicio : IImportacionPropuestaServicio
    {
        private readonly IErrorImportacionRepositorio _errores;
        public ImportacionPropuestaServicio(string cadenaConexion) : this(new ErrorImportacionRepositorio(cadenaConexion)) {}
        internal ImportacionPropuestaServicio(IErrorImportacionRepositorio errores)
        {
            if (errores == null) 
                throw new ArgumentNullException("errores");
            _errores = errores;
        }
        public ResultadoCargaPropuesta CargarDesglosesPropuesta(List<FilaDesgloseExcel> filas)
        {
            var resultado = new ResultadoCargaPropuesta();
            try
            {
                if (filas == null || filas.Count == 0) 
                    resultado.Errores.Add("El Excel no contiene desgloses.");
                else foreach (var fila in filas)
                {
                    if (fila == null) { 
                        resultado.Errores.Add("Se encontró una fila inválida."); 
                        continue; 
                    }
                    Validar(fila, resultado.Errores);
                }
                if (resultado.Errores.Count > 0) 
                    return resultado;
                resultado.Desgloses = filas;
                resultado.EsCorrecto = true;
                return resultado;
            }
            catch (Exception excepcion)
            {
                string referencia = Guid.NewGuid().ToString("N");
                RegistrarErrorSeguro(referencia + ": " + excepcion.ToString());
                resultado.Desgloses.Clear();
                resultado.Errores.Clear();
                resultado.Errores.Add("No se pudo procesar el Excel. Referencia: " + referencia);
                return resultado;
            }
        }
        public bool RegistrarErrorImportacion(string referencia, string mensaje)
        {
            if (!Regex.IsMatch(referencia ?? "", "\\A[a-f0-9]{32}\\z") || string.IsNullOrWhiteSpace(mensaje)) 
                return false;
            return RegistrarErrorSeguro(referencia + ": " + mensaje);
        }
        private bool RegistrarErrorSeguro(string mensaje)
        {
            try { 
                _errores.Registrar(mensaje, null); 
                return true; 
            }
            catch (Exception excepcion) { 
                Trace.TraceError("No se pudo registrar el error de EXT5: {0}", excepcion.Message); 
                return false; 
            }
        }
        private static void Validar(FilaDesgloseExcel fila, List<string> errores)
        {
            string prefijo = "Fila " + fila.NumeroFila + ": ";
            int mes, anio;
            if (!int.TryParse(fila.Mes, out mes) || mes < 1 || mes > 12) 
                errores.Add(prefijo + "Mes debe estar entre 1 y 12.");
            if (!int.TryParse(fila.Anio, out anio) || anio < 1) 
                errores.Add(prefijo + "Año debe ser un entero positivo.");
            decimal horas, monto, iva, total, porcentaje;
            bool h = Numero(fila.Horas, out horas), m = Numero(fila.Monto, out monto);
            bool i = Numero(fila.Iva, out iva), t = Numero(fila.Total, out total), p = Numero(fila.PorcentajeCobro, out porcentaje);
            if (!h || horas < 0) 
                errores.Add(prefijo + "Horas es requerido y debe ser un número no negativo.");
            if (!m || monto < 0) 
                errores.Add(prefijo + "Monto es requerido y debe ser un número no negativo.");
            if (!i || iva < 0) 
                errores.Add(prefijo + "IVA es requerido y debe ser un número no negativo.");
            if (!t || total < 0) 
                errores.Add(prefijo + "Total es requerido y debe ser un número no negativo.");
            if (!p || porcentaje < 0 || porcentaje > 100) 
                errores.Add(prefijo + "Porcentaje de cobro debe estar entre 0 y 100.");
            if (string.IsNullOrWhiteSpace(fila.Observaciones)) 
                errores.Add(prefijo + "Observaciones es requerido.");
            if (m && i && monto >= 0 && iva >= 0)
            {
                try
                {
                    decimal esperado = Math.Round(monto * 0.13m, 2, MidpointRounding.AwayFromZero);
                    if (iva != esperado) errores.Add(prefijo + "IVA debe ser el 13 % del monto, redondeado a dos decimales.");
                    if (t && total != monto + esperado) errores.Add(prefijo + "Total debe ser monto más IVA.");
                }
                catch (OverflowException) { errores.Add(prefijo + "El monto excede la capacidad de cálculo decimal."); }
            }
        }
        private static bool Numero(string texto, out decimal valor)
        {
            valor = 0;
            if (string.IsNullOrWhiteSpace(texto)) 
                return false;
            texto = texto.Trim();
            if (!Regex.IsMatch(texto, "\\A[+-]?[0-9]+(?:[.,][0-9]+)?\\z")) 
                return false;
            return decimal.TryParse(texto.Replace(',', '.'), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out valor);
        }
    }
}
