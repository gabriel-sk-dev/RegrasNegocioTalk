using System;
using System.Collections.Generic;

namespace Escolas.Dominio.Turmas
{
    public sealed class Turma
    {
        public Turma(string codigo, string descricao, IEnumerable<Horario> horarios, DateTime iniciaEm, DateTime terminaEm, decimal valorMensal)
        {
            Codigo = codigo;
            Descricao = descricao;
            Horarios = horarios;
            IniciaEm = iniciaEm;
            TerminaEm = terminaEm;
            ValorMensal = valorMensal;
        }

        public string Codigo { get; }
        public string Descricao { get; }
        public IEnumerable<Horario> Horarios { get; }
        public DateTime IniciaEm { get; }
        public DateTime TerminaEm { get; }
        public decimal ValorMensal { get; }
    }

    public struct Horario
    {
        public Horario(TipoHorario tipo, string inicio, string fim) : this()
        {
            Tipo = tipo;
            Inicio = inicio;
            Fim = fim;
        }

        public TipoHorario Tipo { get; }
        public string Inicio { get;  }
        public string Fim { get; }

        public static Horario NovoParaManha(string inicio, string fim)
            => new Horario(TipoHorario.Manha, inicio, fim);
        public static Horario NovoParaTarde(string inicio, string fim)
            => new Horario(TipoHorario.Tarde, inicio, fim);
    }

    public enum TipoHorario
    {
        Manha,
        MeioDia,
        Tarde,
        Noite
    }
}
