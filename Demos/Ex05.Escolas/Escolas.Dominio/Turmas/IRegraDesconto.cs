using Escolas.Dominio.ServicosDeDominio.RegrasDesconto;
using Escolas.Dominio.Shared;

namespace Escolas.Dominio.Turmas
{
    public interface IRegraDesconto
    {
        Resultado<decimal, Falha> Calcular(ContextoCalculoDesconto contexto);
    }
}
