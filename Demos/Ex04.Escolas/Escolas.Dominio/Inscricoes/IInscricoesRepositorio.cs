using System;
using System.Collections.Generic;

namespace Escolas.Dominio.Inscricoes
{
    public interface IInscricoesRepositorio
    {
        Inscricao Recuperar(Guid id);
        IEnumerable<Inscricao> RecuperarPorTurma(string turma);
    }
}
