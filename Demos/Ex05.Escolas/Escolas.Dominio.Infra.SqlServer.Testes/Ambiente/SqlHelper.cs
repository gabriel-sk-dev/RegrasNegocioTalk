using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Escolas.Dominio.Infra.SqlServer.Testes.Ambiente
{
    public sealed class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(IConfiguracao configuration)
        {
            _connectionString = configuration.RecuperarStringConexao();
        }

        public IEnumerable<T> Query<T>(string sql)
            => Query<T>(sql, new { });

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var resultado = connection.Query<T>(sql, param).ToList();
                connection.Close();
                return resultado;
            }
        }

        public void Limpar(string tabela)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute($"DELETE FROM {tabela}");
                connection.Close();
            }
        }

        public void Executar(string sql, object parametro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(sql, parametro);
                connection.Close();
            }
        }
    }
}
