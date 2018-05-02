using Escolas.Dominio.Shared;
using Escolas.Dominio.Turmas;
using System.Linq;

namespace Escolas.Dominio.ServicosDeDominio.RegrasDesconto
{
    public sealed class DescontoParaTurno : ObjetoValor<DescontoParaTurno>, IRegraDesconto
    {
        public DescontoParaTurno(TipoHorario turno, decimal desconto)
        {
            Turno = turno;
            Desconto = desconto;
        }

        public TipoHorario Turno { get; }
        public decimal Desconto { get; }

        public Resultado<decimal, Falha> Calcular(ContextoCalculoDesconto contexto)
        {
            if (contexto.Turma.Horarios.Any(h => h.Tipo != Turno))
                return 0;
            return Desconto;
        }

        protected override bool EqualsCore(DescontoParaTurno other)
            => Turno.Equals(other.Turno) &&
                Desconto.Equals(other.Desconto);

        protected override int GetHashCodeCore()
            => Turno.GetHashCode() ^
                Desconto.GetHashCode();
    }
}
