using Escolas.Dominio.Alunos;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.Turmas;
using STI.Infra.Crosscutting.Ioc;

namespace Escolas.Dominio.ServicosDeDominio.RegrasDesconto
{
    public sealed class ContextoCalculoDesconto
    {
        public ContextoCalculoDesconto(IDependencyContainer container, Inscricao inscricao, Turma turma, Aluno aluno)
        {
            Container = container;
            Inscricao = inscricao;
            Turma = turma;
            Aluno = aluno;
        }

        public IDependencyContainer Container { get; }
        public Inscricao Inscricao { get; }
        public Turma Turma { get; }
        public Aluno Aluno { get; }
    }
}
