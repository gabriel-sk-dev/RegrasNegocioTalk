using Escolas.Dominio.Alunos;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.ServicosDeDominio;
using Escolas.Dominio.Turmas;
using System.Linq;

namespace Escolas.Dominio.Cliente
{
    public sealed class CalculadoraDescontoService : ICalculadoraDescontoService
    {
        private readonly IInscricoesRepositorio _inscricoesRepositorio;

        public CalculadoraDescontoService(
            IInscricoesRepositorio inscricoesRepositorio)
        {
            _inscricoesRepositorio = inscricoesRepositorio;
        }

        public decimal Calcular(Inscricao inscricao, Turma turma, Aluno aluno)
        {
            var desconto = 0M;
            if (turma.DescontoPorDependenteInscrito.Usar)
            {
                var inscricoesTurma = _inscricoesRepositorio.RecuperarPorTurma(inscricao.Turma);
                var descontoFamilia = 0M;
                foreach (var inscricaoGeral in inscricoesTurma)
                {
                    if (aluno.Familiares.Any(f => f == inscricaoGeral.Aluno))
                        descontoFamilia += turma.DescontoPorDependenteInscrito.Desconto;
                }
                descontoFamilia = descontoFamilia <= turma.DescontoPorDependenteInscrito.MaximoDesconto
                                    ? descontoFamilia
                                    : turma.DescontoPorDependenteInscrito.MaximoDesconto;
                desconto += turma.ValorMensal * (descontoFamilia / 100);
            }
            return desconto;
        }
    }
}
