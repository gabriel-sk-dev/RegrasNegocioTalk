using Escolas.Dominio.Alunos;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.ServicosDeDominio.RegrasDesconto;
using Escolas.Dominio.Shared;
using Escolas.Dominio.Turmas;
using STI.Infra.Crosscutting.Ioc;
using System;

namespace Escolas.Dominio.ServicosDeDominio
{
    public sealed class CalculadoraDividaMensalService
    {
        #region Construtor
        private readonly IInscricoesRepositorio _inscricoesRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly IDependencyContainer _container;

        public CalculadoraDividaMensalService(
            IInscricoesRepositorio inscricoesRepositorio,
            ITurmasRepositorio turmasRepositorio,
            IAlunosRepositorio alunosRepositorio,
            IDependencyContainer container)
        {
            _inscricoesRepositorio = inscricoesRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _alunosRepositorio = alunosRepositorio;
            _container = container;
        }
        #endregion

        public Resultado<decimal,Falha> Calcular(Guid id)
        {
            var inscricao = _inscricoesRepositorio.Recuperar(id);
            if (inscricao.EhFalha)
                return inscricao.Falha;
            var turma = _turmasRepositorio.Recuperar(inscricao.Sucesso.Turma);
            if (turma.EhFalha)
                return turma.Falha;
            var aluno = _alunosRepositorio.Recuperar(inscricao.Sucesso.Aluno);
            if (aluno.EhFalha)
                return aluno.Falha;
            var desconto = 0M;
            var contextoCalculo = new ContextoCalculoDesconto(_container, inscricao.Sucesso, turma.Sucesso, aluno.Sucesso);
            foreach (var regraDesconto in turma.Sucesso.ConfiguracaoFinanceira.RegrasDesconto)
            {
                var calculado = regraDesconto.Calcular(contextoCalculo);
                if (calculado.EhFalha)
                    return Falha.Nova(1000, $"Falha ao calcular desconto (regra#{regraDesconto.GetType().Name}) para inscricao #{inscricao.Sucesso.Id}");
                desconto += calculado.Sucesso;
            }
            desconto = desconto > turma.Sucesso.ConfiguracaoFinanceira.DescontoMaximo
                            ? turma.Sucesso.ConfiguracaoFinanceira.DescontoMaximo
                            : desconto;
            return turma.Sucesso.ConfiguracaoFinanceira.ValorMensal - (turma.Sucesso.ConfiguracaoFinanceira.ValorMensal * (desconto/100));
        }
    }
}
