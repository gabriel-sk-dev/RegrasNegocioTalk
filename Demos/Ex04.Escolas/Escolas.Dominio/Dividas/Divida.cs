using System;

namespace Escolas.Dominio.Dividas
{
    public sealed class Divida
    {
        public Divida(Guid id, Guid inscricao, DateTime geradaEm, DateTime venceEm, decimal valor, Pagamento dadosPagamento)
        {
            Id = id;
            Inscricao = inscricao;
            GeradaEm = geradaEm;
            VenceEm = venceEm;
            Valor = valor;
            DadosPagamento = dadosPagamento;
        }

        public Guid Id { get; }
        public Guid Inscricao { get; }
        public DateTime GeradaEm { get; }
        public DateTime VenceEm { get;  }
        public decimal Valor { get; }
        public Pagamento DadosPagamento { get; }
    }

    public struct Pagamento
    {
        public Pagamento(DateTime quando, decimal valor, decimal juros, decimal multa) : this()
        {
            Quando = quando;
            Valor = valor;
            Juros = juros;
            Multa = multa;
        }

        public DateTime Quando { get; }
        public decimal Valor { get;  }
        public decimal Juros { get;  }
        public decimal Multa { get; }
    }
}
