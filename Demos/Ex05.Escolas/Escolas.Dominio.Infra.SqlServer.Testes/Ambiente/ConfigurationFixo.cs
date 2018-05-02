
namespace Escolas.Dominio.Infra.SqlServer.Testes.Ambiente
{
    public sealed class ConfigurationFixo : IConfiguracao
    {
        private const string server = @"Gabriel-NT";
        private const string senha = "Gabriel";
        public string RecuperarStringConexao(string name)
            => $@"Data Source = {server}; Initial Catalog = DemoRegrasNegocio; User ID = sa; Password = {senha};";

        public string RecuperarConfiguracao(string name)
            => "";
    }
}
