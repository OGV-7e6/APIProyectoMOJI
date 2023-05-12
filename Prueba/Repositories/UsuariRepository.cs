using Dapper;
using Npgsql;
using TodoApi.Model;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    /// <summary>
    /// Clase Usuario que contiene todos los métodos SQL querys
    /// </summary>
    public class UsuariRepository
    {
        private PosgreSQLConfig connexionString;
        public UsuariRepository(PosgreSQLConfig connectionString)
        {
            this.connexionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(connexionString.ConnectionString);
        }

        //-------------------------------------------------------
        public async Task<IEnumerable<Usuari>> GetAllUsuaris()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM public.usuari
                            ORDER BY puntuacion DESC
                        ";

            return await db.QueryAsync<Usuari>(sql, new { });

        }
        //--------------------------------------------------------------

        public async Task<Usuari> GetUsuariId(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM public.usuari
                            WHERE id = @id
                        ";

            return await db.QueryFirstOrDefaultAsync<Usuari>(sql, new { id });
        }

        //-------------------------------------------------------
        public async Task<Usuari> GetUsuariDetall(string nick, string contrasenya)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM public.usuari
                            WHERE nick = @nick and contrasenya = @contrasenya
                        ";

            return await db.QueryFirstOrDefaultAsync<Usuari>(sql, new { nick, contrasenya });
        }
        //-------------------------------------------------------
        public async Task<bool> InsertUsuari(Usuari obj)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO public.usuari ( nom, cognom, nick, contrasenya, pais, admin)
                        VALUES (@nom, @cognom, @nick, @contrasenya, @pais, @admin)
                        ";

            var result = await db.ExecuteAsync(sql, new { obj.nom, obj.cognom, obj.nick, obj.contrasenya, obj.pais, obj.admin });
            return result > 0;
        }
        //-------------------------------------------------------
        public async Task<bool> UpdateUsuari(Usuari obj)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE  public.usuari
                        SET nick = @nick,
                            contrasenya = @contrasenya,
                            pais = @pais
                        WHERE id = @id;
                        ";

            var result = await db.ExecuteAsync(sql, new { obj.nick, obj.contrasenya, obj.pais, obj.id });
            return result > 0;
        }
        //-------------------------------------------------------
        public async Task<bool> UpdatePuntuacio(Usuari obj)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE  public.usuari
                        SET puntuacion = puntuacion + @puntuacion
                        WHERE id = @id;
                        ";

            var result = await db.ExecuteAsync(sql, new { obj.puntuacion, obj.id });
            return result > 0;
        }
        //-------------------------------------------------------
        public async Task<bool> DeleteUsuari(Usuari obj)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE FROM public.usuari
                        WHERE id = @Id
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = obj.id });
            return result > 0;
        }
    }
}
