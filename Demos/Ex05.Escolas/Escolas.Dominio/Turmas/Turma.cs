using System;
using System.Collections.Generic;

namespace Escolas.Dominio.Turmas
{
    public sealed partial class Turma
    {
        #region construtor
        public Turma(
            string codigo, 
            string descricao, 
            IEnumerable<Horario> horarios, 
            DateTime iniciaEm, 
            DateTime terminaEm,
            Financeiro configuracaoFinanceira)
        {
            Codigo = codigo;
            Descricao = descricao;
            Horarios = horarios;
            IniciaEm = iniciaEm;
            TerminaEm = terminaEm;
            ConfiguracaoFinanceira = configuracaoFinanceira;
        }
        #endregion

        public string Codigo { get; }
        public string Descricao { get; }
        public IEnumerable<Horario> Horarios { get; }
        public DateTime IniciaEm { get; }
        public DateTime TerminaEm { get; }
        public Financeiro ConfiguracaoFinanceira { get;  }

    }
    public enum TipoHorario
    {
        Manha,
        MeioDia,
        Tarde,
        Noite
    }
}
