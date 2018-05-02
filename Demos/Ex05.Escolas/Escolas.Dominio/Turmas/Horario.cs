namespace Escolas.Dominio.Turmas
{
    public sealed partial class Turma
    {
        public struct Horario
        {
            public Horario(TipoHorario tipo, string inicio, string fim) : this()
            {
                Tipo = tipo;
                Inicio = inicio;
                Fim = fim;
            }

            public TipoHorario Tipo { get; }
            public string Inicio { get; }
            public string Fim { get; }

            public static Horario NovoParaManha(string inicio, string fim)
                => new Horario(TipoHorario.Manha, inicio, fim);
            public static Horario NovoParaTarde(string inicio, string fim)
                => new Horario(TipoHorario.Tarde, inicio, fim);
        }
    }
}
