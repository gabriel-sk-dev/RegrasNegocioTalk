using Escolas.Dominio.Infra.SqlServer.Testes.Ambiente;
using Escolas.Dominio.Infra.SqlServer.Turmas;
using Escolas.Dominio.ServicosDeDominio.RegrasDesconto;
using Escolas.Dominio.Turmas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escolas.Dominio.Infra.SqlServer.Testes
{
    [TestClass]
    public class TurmasRepositorioTestes
    {
        private SqlHelper _sqlHelper;
        private ITurmasRepositorio _turmasRepositorio;

        [TestInitialize]
        public void Setup()
        {
            var configuracao = new ConfigurationFixo();
            _sqlHelper = new SqlHelper(configuracao);
            _turmasRepositorio = new TurmasRepositorio(configuracao);
        }

        [TestCleanup]
        public void FinalizarTestes()
        {
            _sqlHelper.Limpar("Horarios");
            _sqlHelper.Limpar("Turmas");
        }

        [TestMethod]
        public void Consigo_Incluir_Nova_Turma_Com_Regras_Diferentes()
        {
            var regraTurnoTarde = new DescontoParaTurno(TipoHorario.Tarde, 12.5M);
            var regraTurnoManha = new DescontoParaTurno(TipoHorario.Manha, 5.32M);
            var regraFamiliar = new DescontoPorFamilia(20.3M, 4.3M);
            var distancias = new Dictionary<int, decimal>();
            distancias.Add(23, 10.3M);
            distancias.Add(50, 13.4M);
            var regraDistancia = new DescontoPorDistancia(distancias);
            var turma = new Turma(
                                Guid.NewGuid().ToString(),
                                "Teste de regra por turno",
                                new List<Turma.Horario> { new Turma.Horario(TipoHorario.Manha, "10:00", "11:00") },
                                DateTime.Now.AddDays(-10),
                                DateTime.Now.AddDays(30),
                                new Turma.Financeiro(120.56M, new List<IRegraDesconto> { regraTurnoManha, regraTurnoTarde, regraFamiliar, regraDistancia }, 30.50M));

            var resultado = _turmasRepositorio.AdicionarESalvar(turma);

            resultado.EhSucesso.ShouldBeTrue();
            var horarios = _sqlHelper
                                .Query<dynamic>("SELECT Tipo, Inicio, Fim FROM Horarios WHERE CodigoTurma = @Codigo",new { turma.Codigo })
                                .Select(h => new Turma.Horario((TipoHorario)((int)h.Tipo), (string)h.Inicio, (string)h.Fim));
            var turmaInserida = _sqlHelper
                                    .Query<dynamic>("SELECT Codigo, Descricao, IniciaEm, TerminaEm, ValorMensal, RegrasDesconto, DescontoMaximo FROM Turmas WHERE Codigo = @Codigo", new { turma.Codigo })
                                    .Select(t =>
                                    {
                                        var regrasDesconto = JsonConvert.DeserializeObject<IEnumerable<IRegraDesconto>>(((string)t.RegrasDesconto), new JsonSerializerSettings
                                        {
                                            TypeNameHandling = TypeNameHandling.Objects
                                        });
                                        return new Turma((string)t.Codigo,
                                                            (string)t.Descricao,
                                                            horarios,
                                                            (DateTime)t.IniciaEm,
                                                            (DateTime)t.TerminaEm,
                                                            new Turma.Financeiro((decimal)t.ValorMensal, regrasDesconto, (decimal)t.DescontoMaximo));
                                    })
                                    .FirstOrDefault();
            turmaInserida.Descricao.ShouldBe(turma.Descricao);
            
        }
    }
}
