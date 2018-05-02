using System;
using System.Collections.Generic;

namespace Escolas.Dominio.Turmas
{
    public sealed class Turma
    {
        #region construtor
        public Turma(
            string codigo, 
            string descricao, 
            IEnumerable<Horario> horarios, 
            DateTime iniciaEm, 
            DateTime terminaEm, 
            decimal valorMensal,
            DescontoPorTurno descontoTurnoManha,
            DescontoPorTurno descontoTurnoTarde, 
            DescontoPorDistancia descontoPorEndereco,
            DescontoPorDependente descontoPorDependenteInscrito)
        {
            Codigo = codigo;
            Descricao = descricao;
            Horarios = horarios;
            IniciaEm = iniciaEm;
            TerminaEm = terminaEm;
            ValorMensal = valorMensal;
            DescontoTurnoManha = descontoTurnoManha;
            DescontoTurnoTarde = descontoTurnoTarde;
            DescontoPorEndereco = descontoPorEndereco;
            DescontoPorDependenteInscrito = descontoPorDependenteInscrito;
        }
        #endregion

        public string Codigo { get; }
        public string Descricao { get; }
        public IEnumerable<Horario> Horarios { get; }
        public DateTime IniciaEm { get; }
        public DateTime TerminaEm { get; }
        public decimal ValorMensal { get; }        
        public DescontoPorTurno DescontoTurnoManha { get; }
        public DescontoPorTurno DescontoTurnoTarde { get; }
        public DescontoPorDistancia DescontoPorEndereco { get; }
        public DescontoPorDependente DescontoPorDependenteInscrito { get; }
    }

    public struct DescontoPorTurno
    {
        public DescontoPorTurno(bool usar, decimal desconto)
        {
            Usar = usar;
            Desconto = desconto;
        }

        public bool Usar { get; }
        public decimal Desconto { get; }
    }

    public struct DescontoPorDistancia
    {
        public DescontoPorDistancia(bool usar, int distancia, decimal desconto) : this()
        {
            Usar = usar;
            Distancia = distancia;
            Desconto = desconto;
        }

        public bool Usar { get;  }
        public int Distancia { get;  }
        public decimal Desconto { get;  }
    }

    public struct DescontoPorDependente
    {
        public DescontoPorDependente(bool usar, decimal desconto, decimal maximoDesconto) : this()
        {
            Usar = usar;
            Desconto = desconto;
            MaximoDesconto = maximoDesconto;
        }
        public bool Usar { get; }
        public decimal Desconto { get; }
        public decimal MaximoDesconto { get; }
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
