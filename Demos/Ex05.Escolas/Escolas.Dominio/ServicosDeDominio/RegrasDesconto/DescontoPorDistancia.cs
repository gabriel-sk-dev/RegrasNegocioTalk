using Escolas.Dominio.Shared;
using Escolas.Dominio.Turmas;
using System.Collections.Generic;
using System.Linq;

namespace Escolas.Dominio.ServicosDeDominio.RegrasDesconto
{
    public sealed class DescontoPorDistancia : ObjetoValor<DescontoPorDistancia>, IRegraDesconto
    {
        public DescontoPorDistancia(IDictionary<int, decimal> tabelaDesconto)
        {
            TabelaDesconto = tabelaDesconto;
        }

        public IDictionary<int,decimal> TabelaDesconto { get; }

        public Resultado<decimal, Falha> Calcular(ContextoCalculoDesconto contexto)
            => TabelaDesconto.FirstOrDefault(d => d.Key <= contexto.Aluno.EnderecoPrincipal.DistanciaAteClube).Value;

        protected override bool EqualsCore(DescontoPorDistancia other)
            => TabelaDesconto
                .Select(t => t.Value)
                .OrderBy(t => t)
                .SequenceEqual(other.TabelaDesconto.Select(t => t.Value).OrderBy(t => t));

        protected override int GetHashCodeCore()
            => TabelaDesconto.Select(t => t.Value).OrderBy(t => t).GetHashCode();
    }
}
