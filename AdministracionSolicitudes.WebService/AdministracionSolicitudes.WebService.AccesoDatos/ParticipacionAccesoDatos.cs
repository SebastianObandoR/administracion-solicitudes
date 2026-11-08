using AdministracionSolicitudes.WebService.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Web.Script.Serialization;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public class ParticipacionAccesoDatos
    {
        private const string ModuloBitacora = "Participaciones";
        private readonly string _connectionString;

        public ParticipacionAccesoDatos()
        {
            _connectionString =
                ConfigurationManager.ConnectionStrings["PublicaSA"]?.ConnectionString
                ?? throw new ConfigurationErrorsException(
                    "No se encontro la cadena de conexion PublicaSA");
        }

        public bool SolicitudPublicadaVigente(int idSolicitud)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM solicitud
                WHERE id_solicitud = @IdSolicitud
                  AND publicada = 1
                  AND fecha_vencimiento_publicacion >= CURDATE();";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(sql, new { IdSolicitud = idSolicitud }) > 0;
            }
        }

        public int? ObtenerIdProveedor(string cedulaJuridica)
        {
            const string sql = @"
                SELECT id_proveedor
                FROM proveedor
                WHERE cedula_juridica = @Cedula
                LIMIT 1;";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<int?>(sql, new { Cedula = cedulaJuridica });
            }
        }

        public bool ExisteParticipacion(int idSolicitud, int idProveedor)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM participacion
                WHERE id_solicitud = @IdSolicitud
                  AND id_proveedor = @IdProveedor;";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    sql,
                    new { IdSolicitud = idSolicitud, IdProveedor = idProveedor }) > 0;
            }
        }

        public int RegistrarParticipacion(ParticipacionProveedor p)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    int? idProveedor = connection.QuerySingleOrDefault<int?>(
                        "SELECT id_proveedor FROM proveedor WHERE cedula_juridica = @CedulaJuridica LIMIT 1;",
                        new { p.CedulaJuridica },
                        tx);

                    bool proveedorNuevo = !idProveedor.HasValue;

                    if (proveedorNuevo)
                    {
                        idProveedor = connection.ExecuteScalar<int>(@"
                            INSERT INTO proveedor
                                (cedula_juridica, nombre, nombre_representante, telefono, correo)
                            VALUES
                                (@CedulaJuridica, @NombreProveedor, @NombreRepresentante, @Telefono, @Correo);
                            SELECT LAST_INSERT_ID();",
                            p,
                            tx);
                    }

                    int idParticipacion = connection.ExecuteScalar<int>(@"
                        INSERT INTO participacion (id_solicitud, id_proveedor)
                        VALUES (@IdSolicitud, @IdProveedor);
                        SELECT LAST_INSERT_ID();",
                        new { p.IdSolicitud, IdProveedor = idProveedor.Value },
                        tx);

                    foreach (var d in p.Desgloses)
                    {
                        connection.Execute(@"
                            INSERT INTO participaciondesglose
                                (id_participacion, mes, anio, horas, monto, iva, total,
                                 porcentaje_cobro, observaciones)
                            VALUES
                                (@IdParticipacion, @Mes, @Anio, @Horas, @Monto, @Iva, @Total,
                                 @PorcentajeCobro, @Observaciones);",
                            new
                            {
                                IdParticipacion = idParticipacion,
                                d.Mes,
                                d.Anio,
                                d.Horas,
                                d.Monto,
                                d.Iva,
                                d.Total,
                                d.PorcentajeCobro,
                                d.Observaciones
                            },
                            tx);
                    }

                    var json = new JavaScriptSerializer().Serialize(new
                    {
                        idParticipacion,
                        idSolicitud = p.IdSolicitud,
                        proveedorNuevo,
                        proveedor = new
                        {
                            idProveedor = idProveedor.Value,
                            cedulaJuridica = p.CedulaJuridica,
                            nombre = p.NombreProveedor
                        },
                        desgloses = p.Desgloses.Select(d => new
                        {
                            mes = d.Mes,
                            anio = d.Anio,
                            horas = d.Horas,
                            monto = d.Monto,
                            iva = d.Iva,
                            total = d.Total,
                            porcentajeCobro = d.PorcentajeCobro,
                            observaciones = d.Observaciones
                        }).ToList()
                    });

                    connection.Execute(@"
                        INSERT INTO bitacora (id_usuario, modulo, accion, descripcion, fecha)
                        VALUES (NULL, @Modulo, 'CREATE', @Descripcion, NOW());",
                        new
                        {
                            Modulo = ModuloBitacora,
                            Descripcion = "Participación registrada: " + json
                        },
                        tx);

                    tx.Commit();
                    return idParticipacion;
                }
            }
        }

        public void RegistrarError(string mensaje)
        {
            const string sql = @"
                INSERT INTO errorlog (id_usuario, modulo, mensaje_error, fecha)
                VALUES (NULL, @Modulo, @Mensaje, NOW());";

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(sql, new { Modulo = ModuloBitacora, Mensaje = mensaje });
            }
        }
    }
}