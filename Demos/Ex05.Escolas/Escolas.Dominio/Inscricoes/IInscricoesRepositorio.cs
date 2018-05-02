using Escolas.Dominio.Shared;
using System;
using System.Collections.Generic;

namespace Escolas.Dominio.Inscricoes
{
    public interface IInscricoesRepositorio
    {
        Resultado<Inscricao, Falha> Recuperar(Guid id);
        IEnumerable<Inscricao> RecuperarPorTurma(string turma);
    }
}
