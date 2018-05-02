using Escolas.Dominio.Shared;
using System;

namespace Escolas.Dominio.Alunos
{
    public sealed class Aluno
    {
        public Aluno(string matricula, NomeCompleto nome, DateTime nascimento, Endereco enderecoPrincipal, string email)
        {
            Matricula = matricula;
            Nome = nome;
            Nascimento = nascimento;
            EnderecoPrincipal = enderecoPrincipal;
            Email = email;
        }

        public string Matricula { get; }
        public NomeCompleto Nome { get; }
        public DateTime Nascimento { get; }
        public Endereco EnderecoPrincipal { get; }
        public string Email { get; }

    }
}
