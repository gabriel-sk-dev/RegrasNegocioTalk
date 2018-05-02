using Escolas.Dominio.Clientes;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.ServicosDeDominio;
using System;
using System.Linq;
using System.Reflection;

namespace Escolas.Aplicacao.CalculoDesconto
{
    public sealed class CalculoDescontoServiceFactory: ICalculoDescontoServiceFactory
    {
        #region construtor
        private readonly IClientesRepositorio _clientesRepositorio;
        private readonly IInscricoesRepositorio _inscricoesRepositorio;

        public CalculoDescontoServiceFactory(
            IClientesRepositorio clientesRepositorio,
            IInscricoesRepositorio inscricoesRepositorio)
        {
            _clientesRepositorio = clientesRepositorio;
            _inscricoesRepositorio = inscricoesRepositorio;
        }
        #endregion

        public ICalculadoraDescontoService Criar(string numeroCliente = "")
        {
            if (String.IsNullOrEmpty(numeroCliente))
                return new CalculadoraDescontoService();

            var cliente = _clientesRepositorio.RecuperarAtivo();
            var assemblyCliente = Assembly.LoadFrom(cliente.LocalDllCustomizada);
            Type tipo = assemblyCliente
                            .GetTypes()
                            .FirstOrDefault(type => type.FindInterfaces(FiltrarInterfacesIguais, typeof(ICalculadoraDescontoService)).FirstOrDefault() != null);
            return Activator.CreateInstance(tipo, _inscricoesRepositorio) as ICalculadoraDescontoService;
        }

        public static bool FiltrarInterfacesIguais(Type typeObj, Object criteriaObj)
        {
            if (typeObj.ToString() == criteriaObj.ToString())
                return true;
            else
                return false;
        }
    }
}
