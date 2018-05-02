using Escolas.Dominio.Alunos;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.Turmas;
using System.Linq;

namespace Escolas.Dominio.ServicosDeDominio
{
    public interface ICalculadoraDescontoService
    {
        decimal Calcular(Inscricao inscricao, Turma turma, Aluno aluno);
    }

    public sealed class CalculadoraDescontoService : ICalculadoraDescontoService
    {
        public decimal Calcular(Inscricao inscricao, Turma turma, Aluno aluno)
        {
            var desconto = 0M;
            if (turma.DescontoTurnoManha.Usar && turma.Horarios.Any(h => h.Tipo == TipoHorario.Manha))
                desconto += turma.ValorMensal * (turma.DescontoTurnoManha.Desconto / 100);
            if (turma.DescontoTurnoTarde.Usar && turma.Horarios.Any(h => h.Tipo == TipoHorario.Tarde))
                desconto += turma.ValorMensal * (turma.DescontoTurnoTarde.Desconto / 100);
            if (turma.DescontoPorEndereco.Usar && aluno.EnderecoPrincipal.DistanciaAteClube > turma.DescontoPorEndereco.Distancia)
                desconto += turma.ValorMensal * (turma.DescontoPorEndereco.Desconto / 100);
            return desconto;
        }
    }
}
