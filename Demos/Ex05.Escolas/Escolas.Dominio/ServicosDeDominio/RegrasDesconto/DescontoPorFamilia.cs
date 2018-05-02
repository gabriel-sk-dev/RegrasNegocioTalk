using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.Shared;
using Escolas.Dominio.Turmas;
using System.Linq;

namespace Escolas.Dominio.ServicosDeDominio.RegrasDesconto
{
    public sealed class DescontoPorFamilia : ObjetoValor<DescontoPorFamilia>, IRegraDesconto
    {
        public DescontoPorFamilia(decimal maximo, decimal porFamiliar)
        {
            Maximo = maximo;
            PorFamiliar = porFamiliar;
        }

        public decimal Maximo { get; }
        public decimal PorFamiliar { get; }

        public Resultado<decimal, Falha> Calcular(ContextoCalculoDesconto contexto)
        {
            var inscricoesTurma = contexto
                                    .Container
                                    .Resolver
                                    .Resolve<IInscricoesRepositorio>()
                                    .RecuperarPorTurma(contexto.Inscricao.Turma);
            var descontoFamilia = 0M;
            foreach (var inscricaoGeral in inscricoesTurma)
                if (contexto.Aluno.Familiares.Any(f => f == inscricaoGeral.Aluno))
                    descontoFamilia += PorFamiliar;
            return descontoFamilia <= Maximo ? descontoFamilia : Maximo;
        }

        protected override bool EqualsCore(DescontoPorFamilia other)
            => Maximo.Equals(other.Maximo) &&
                PorFamiliar.Equals(other.PorFamiliar);

        protected override int GetHashCodeCore()
            => Maximo.GetHashCode() ^
                PorFamiliar.GetHashCode();
    }
}
