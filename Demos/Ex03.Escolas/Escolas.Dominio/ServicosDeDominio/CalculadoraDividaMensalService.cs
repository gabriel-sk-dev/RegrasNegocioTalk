using Escolas.Dominio.Alunos;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.Turmas;
using System;
using System.Linq;

namespace Escolas.Dominio.ServicosDeDominio
{
    public sealed class CalculadoraDividaMensalService
    {
        #region Construtor
        private readonly IInscricoesRepositorio _inscricoesRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;
        private readonly IAlunosRepositorio _alunosRepositorio;

        public CalculadoraDividaMensalService(
            IInscricoesRepositorio inscricoesRepositorio,
            ITurmasRepositorio turmasRepositorio,
            IAlunosRepositorio alunosRepositorio)
        {
            _inscricoesRepositorio = inscricoesRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _alunosRepositorio = alunosRepositorio;
        }
        #endregion

        public decimal Calcular(Guid id)
        {
            var inscricao = _inscricoesRepositorio.Recuperar(id);
            var turma = _turmasRepositorio.Recuperar(inscricao.Turma);
            var aluno = _alunosRepositorio.Recuperar(inscricao.Aluno);
            
            var desconto = 0M;
            if (turma.DescontoTurnoManha.Usar && turma.Horarios.Any(h => h.Tipo == TipoHorario.Manha))
                desconto += turma.ValorMensal * (turma.DescontoTurnoManha.Desconto / 100);
            if (turma.DescontoTurnoTarde.Usar && turma.Horarios.Any(h => h.Tipo == TipoHorario.Tarde))
                desconto += turma.ValorMensal * (turma.DescontoTurnoTarde.Desconto / 100);
            if(turma.DescontoPorEndereco.Usar && aluno.EnderecoPrincipal.DistanciaAteClube > turma.DescontoPorEndereco.Distancia)
                desconto += turma.ValorMensal * (turma.DescontoPorEndereco.Desconto / 100);
            if (turma.DescontoPorDependenteInscrito.Usar)
            {
                var inscricoesTurma = _inscricoesRepositorio.RecuperarPorTurma(inscricao.Turma);
                var descontoFamilia = 0M;
                foreach (var inscricaoGeral in inscricoesTurma)
                {
                    if(aluno.Familiares.Any(f=> f== inscricaoGeral.Aluno))
                        descontoFamilia += turma.DescontoPorDependenteInscrito.Desconto;
                }
                descontoFamilia = descontoFamilia <= turma.DescontoPorDependenteInscrito.MaximoDesconto
                                    ? descontoFamilia
                                    : turma.DescontoPorDependenteInscrito.MaximoDesconto;                
                desconto += turma.ValorMensal * (descontoFamilia / 100);
            }   

            return turma.ValorMensal - desconto;
        }
    }
}
