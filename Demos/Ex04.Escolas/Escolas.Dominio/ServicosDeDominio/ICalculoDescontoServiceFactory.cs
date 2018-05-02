namespace Escolas.Dominio.ServicosDeDominio
{
    public interface ICalculoDescontoServiceFactory
    {
        ICalculadoraDescontoService Criar(string numeroCliente = "");
    }
}
