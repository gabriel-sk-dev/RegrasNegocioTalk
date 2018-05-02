using System.Collections.Generic;

namespace Escolas.Dominio.Turmas
{
    public sealed partial class Turma
    {
        public struct Financeiro
        {
            public Financeiro(decimal valorMensal, IEnumerable<IRegraDesconto> regrasDesconto, decimal descontoMaximo)
            {
                ValorMensal = valorMensal;
                RegrasDesconto = regrasDesconto;
                DescontoMaximo = descontoMaximo;
            }

            public decimal ValorMensal { get; }
            public IEnumerable<IRegraDesconto> RegrasDesconto { get; }
            public decimal DescontoMaximo { get; }
        }
    }
}
