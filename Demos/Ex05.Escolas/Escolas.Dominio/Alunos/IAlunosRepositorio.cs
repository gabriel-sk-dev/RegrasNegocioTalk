using Escolas.Dominio.Shared;

namespace Escolas.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Resultado<Aluno, Falha> Recuperar(string codigo);
    }
}
