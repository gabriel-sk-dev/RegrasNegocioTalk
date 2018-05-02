using System;

namespace Escolas.Dominio.Inscricoes
{
    public sealed class Inscricao
    {
        public Inscricao(Guid id, string aluno, string turma, DateTime inscritoEm, bool emAtividade)
        {
            Id = id;
            Aluno = aluno;
            Turma = turma;
            InscritoEm = inscritoEm;
            EmAtividade = emAtividade;
        }

        public Guid Id { get; }
        public string Aluno { get; }
        public string Turma { get; }
        public DateTime InscritoEm { get; }
        public bool EmAtividade { get; }
    }
}
