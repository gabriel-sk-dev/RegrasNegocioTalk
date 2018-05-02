using Escolas.Dominio.Shared;
using System;
using System.Collections.Generic;

namespace Escolas.Dominio.Alunos
{
    public sealed class Aluno
    {
        public Aluno(string matricula, NomeCompleto nome, DateTime nascimento, Endereco enderecoPrincipal, string email, IEnumerable<string> familiares)
        {
            Matricula = matricula;
            Nome = nome;
            Nascimento = nascimento;
            EnderecoPrincipal = enderecoPrincipal;
            Email = email;
            Familiares = familiares;
        }

        public string Matricula { get; }
        public NomeCompleto Nome { get; }
        public DateTime Nascimento { get; }
        public Endereco EnderecoPrincipal { get; }
        public string Email { get; }
        public IEnumerable<string> Familiares { get; }
    }
}
