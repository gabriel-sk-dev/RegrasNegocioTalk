namespace Escolas.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Aluno Recuperar(string codigo);
    }
}
