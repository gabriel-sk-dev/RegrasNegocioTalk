using Escolas.Dominio.Shared;

namespace Escolas.Dominio.Turmas
{
    public interface ITurmasRepositorio
    {
        Resultado<Turma, Falha> Recuperar(string codigo);
        Resultado<Turma, Falha> AdicionarESalvar(Turma turma);
    }
}
