using Escolas.Dominio.Alunos;
using Escolas.Dominio.Clientes;
using Escolas.Dominio.Inscricoes;
using Escolas.Dominio.Turmas;
using System;

namespace Escolas.Dominio.ServicosDeDominio
{
    public sealed class CalculadoraDividaMensalService
    {
        #region Construtor
        private readonly IInscricoesRepositorio _inscricoesRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly ICalculoDescontoServiceFactory _factoryServicoDesconto;
        private readonly IClientesRepositorio _clienteRepositorio;

        public CalculadoraDividaMensalService(
            IInscricoesRepositorio inscricoesRepositorio,
            ITurmasRepositorio turmasRepositorio,
            IAlunosRepositorio alunosRepositorio,
            ICalculoDescontoServiceFactory factoryServicoDesconto,
            IClientesRepositorio clienteRepositorio)
        {
            _inscricoesRepositorio = inscricoesRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _alunosRepositorio = alunosRepositorio;
            _factoryServicoDesconto = factoryServicoDesconto;
            _clienteRepositorio = clienteRepositorio;
        }
        #endregion

        public decimal Calcular(Guid id)
        {
            var inscricao = _inscricoesRepositorio.Recuperar(id);
            var turma = _turmasRepositorio.Recuperar(inscricao.Turma);
            var aluno = _alunosRepositorio.Recuperar(inscricao.Aluno);
            var cliente = _clienteRepositorio.RecuperarAtivo();

            var desconto = _factoryServicoDesconto.Criar(cliente.Numero).Calcular(inscricao, turma, aluno);

            return turma.ValorMensal - desconto;
        }
    }
}
