using Dapper;
using Escolas.Dominio.Shared;
using Escolas.Dominio.Turmas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Escolas.Dominio.Infra.SqlServer.Turmas
{
    public sealed class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly string _stringConexao;

        public TurmasRepositorio(IConfiguracao configuracao)
        {
            _stringConexao = configuracao.RecuperarStringConexao();
        }

        public Resultado<Turma, Falha> AdicionarESalvar(Turma turma)
        {
            using (var connection = new SqlConnection(_stringConexao))
            {
                #region sql
                const string sqlHorarios = @"INSERT INTO Horarios 
                                                (Id, CodigoTurma, Tipo, Inicio, Fim) 
                                            VALUES
                                                (@Id, @CodigoTurma, @Tipo, @Inicio, @Fim)";
                const string sqlTurmas = @"INSERT INTO Turmas 
                                                ( Codigo, Descricao, IniciaEm, TerminaEm, ValorMensal, RegrasDesconto, DescontoMaximo) 
                                            VALUES 
                                                ( @Codigo, @Descricao, @IniciaEm, @TerminaEm, @ValorMensal, @regras, @DescontoMaximo)";
                #endregion
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var regras = JsonConvert.SerializeObject(turma.ConfiguracaoFinanceira.RegrasDesconto, new JsonSerializerSettings {
                            TypeNameHandling = TypeNameHandling.All
                        });
                        var resultado = connection
                                            .Execute(sqlTurmas, new
                                            {
                                                turma.Codigo,
                                                turma.Descricao,
                                                turma.IniciaEm,
                                                turma.TerminaEm,
                                                turma.ConfiguracaoFinanceira.ValorMensal,
                                                turma.ConfiguracaoFinanceira.DescontoMaximo,
                                                regras
                                            }, transaction);
                        if (resultado <= 0)
                            return Falha.Nova(100, "Não foi possível incluir Turma");
                        foreach (var horario in turma.Horarios)
                        {
                            resultado = connection
                                            .Execute(sqlHorarios, new
                                            {
                                                CodigoTurma = turma.Codigo,
                                                Id = Guid.NewGuid(),
                                                horario.Tipo,
                                                horario.Inicio,
                                                horario.Fim
                                            }, transaction);
                            if (resultado <= 0)
                                return Falha.Nova(101, "Não foi possível incluir turma, falha ao incluir horario");
                        }
                        transaction.Commit();
                        return turma;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Falha.NovaComException(ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public Resultado<Turma, Falha> Recuperar(string codigo)
        {
            using (var connection = new SqlConnection(_stringConexao))
            {
                const string sql = @"SELECT Tipo, Inicio, Fim FROM Horarios WHERE CodigoTurma = @codigo;
                                     SELECT Codigo, Descricao, IniciaEm, TerminaEm, ValorMensal, RegrasDesconto, DescontoMaximo 
                                        FROM Turmas WHERE Codigo = @codigo;";
                try
                {
                    using (var query = connection.QueryMultiple(sql, new { codigo }))
                    {
                        var horarios = query
                                        .Read<dynamic>()
                                        .Select(h => new Turma.Horario((TipoHorario)((int)h.Tipo), (string)h.Inicio, (string)h.Fim));
                        return query
                                .Read<dynamic>()
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
                    }
                }
                catch (Exception ex)
                {
                    return Falha.NovaComException(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
