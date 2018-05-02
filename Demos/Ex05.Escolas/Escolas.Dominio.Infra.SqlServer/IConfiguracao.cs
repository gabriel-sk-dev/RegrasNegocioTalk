using Microsoft.Extensions.Configuration;

namespace Escolas.Dominio.Infra.SqlServer
{
    public interface IConfiguracao
    {
        string RecuperarStringConexao(string name = "defaultConnection");
        string RecuperarConfiguracao(string name);
    }

    public sealed class JsonConfiguration : IConfiguracao
    {
        private readonly IConfiguration _configurationRoot;

        public JsonConfiguration(Microsoft.Extensions.Configuration.IConfiguration configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public string RecuperarStringConexao(string name = "DefaultConnection")
            => _configurationRoot.GetConnectionString(name);

        public string RecuperarConfiguracao(string name) =>
            _configurationRoot[name];
    }
}
