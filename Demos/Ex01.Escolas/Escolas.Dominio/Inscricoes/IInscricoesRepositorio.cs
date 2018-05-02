using System;
namespace Escolas.Dominio.Inscricoes
{
    public interface IInscricoesRepositorio
    {
        Inscricao Recuperar(Guid id);
    }
}
