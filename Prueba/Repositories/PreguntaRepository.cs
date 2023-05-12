using Dapper;
using Npgsql;
using TodoApi.Model;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    /// <summary>
    /// Clase Pregunta que contiene todos los métodos SQL querys
    /// </summary>
    public class PreguntaRepository
    {
        private PosgreSQLConfig connexionString;
        public PreguntaRepository(PosgreSQLConfig connectionString)
        {
            this.connexionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(connexionString.ConnectionString);
        }

        //-------------------------------------------------------
        public async Task<IEnumerable<Pregunta>> GetAllPregunta()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT idpregunta, stringpregunta, dificultat
                            FROM public.pregunta
                        ";

            return await db.QueryAsync<Pregunta>(sql, new { });

        }
        //-------------------------------------------------------
        public async Task<Pregunta> GetPreguntaDificultat(int dificultat)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT idpregunta, stringpregunta, dificultat
                            FROM public.pregunta
                            WHERE dificultat = @dificultat
                            ORDER BY RANDOM()
                            LIMIT 1
                        ";

            return await db.QueryFirstOrDefaultAsync<Pregunta>(sql, new { dificultat });
        }

        public async Task<Pregunta> GetPreguntaId()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT * 
                            FROM  public.pregunta
                            ORDER BY idpregunta DESC 
                            LIMIT 1;
                        ";

            return await db.QueryFirstOrDefaultAsync<Pregunta>(sql, new { });
        }
        //-------------------------------------------------------
        public async Task<bool> InsertPregunta(Pregunta obj)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO public.pregunta (stringpregunta, dificultat)
                        VALUES (@stringpregunta, @dificultat)
                        ";

            var result = await db.ExecuteAsync(sql, new { obj.stringpregunta, obj.dificultat });
            return result > 0;
        }
        //-------------------------------------------------------
        public async Task<bool> DeletePregunta(Pregunta obj)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE FROM public.pregunta
                        WHERE idpregunta = @Id
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = obj.idpregunta });
            return result > 0;
        }
    }
}
