using Dapper;
using Npgsql;
using TodoApi.Model;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    /// <summary>
    /// Clase Respuesta que contiene todos los métodos SQL querys
    /// </summary>
    public class RespuestaRepository
    {
        private PosgreSQLConfig connexionString;
        public RespuestaRepository(PosgreSQLConfig connectionString)
        {
            this.connexionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(connexionString.ConnectionString);
        }

        //-------------------------------------------------------
        public async Task<IEnumerable<Respuesta>> GetAllRespuesta()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT idpregunta, idrespuesta, stringrespuesta, correcta
                            FROM public.respuesta
                        ";

            return await db.QueryAsync<Respuesta>(sql, new { });

        }
        //-------------------------------------------------------
        public async Task<IEnumerable<Respuesta>> GetRespuestaDetall(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT idpregunta, idrespuesta, stringrespuesta, correcta
                            FROM public.respuesta
                            WHERE idpregunta = @Id
                        ";

            return await db.QueryAsync<Respuesta>(sql, new { Id = id });
        }
        //-------------------------------------------------------
        public async Task<bool> InsertRespuesta(Respuesta obj)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO public.respuesta ( idpregunta, stringrespuesta, correcta)
                        VALUES (@idpregunta, @stringrespuesta, @correcta)
                        ";

            var result = await db.ExecuteAsync(sql, new { obj.idpregunta, obj.stringrespuesta, obj.correcta});
            return result > 0;
        }
    }
}
