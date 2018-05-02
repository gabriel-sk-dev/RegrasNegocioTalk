using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.Turmas;
using System;
using System.Linq;

namespace Escolas.Dominio.ServicosDeDominio
{
    public sealed class CalculadoraDividaMensalService
    {
        private readonly IInscricoesRepositorio _inscricoesRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;

        public CalculadoraDividaMensalService(
            IInscricoesRepositorio inscricoesRepositorio,
            ITurmasRepositorio turmasRepositorio)
        {
            _inscricoesRepositorio = inscricoesRepositorio;
            _turmasRepositorio = turmasRepositorio;
        }

        public decimal Calcular(Guid id)
        {
            var inscricao = _inscricoesRepositorio.Recuperar(id);
            var turma = _turmasRepositorio.Recuperar(inscricao.Turma);
            var desconto = 0M;
            if (turma.Horarios.Any(h => h.Tipo == TipoHorario.Manha))
                desconto = turma.ValorMensal * (turma.DescontoParaTurnoManha / 100);
            return turma.ValorMensal - desconto;
        }
    }
}
