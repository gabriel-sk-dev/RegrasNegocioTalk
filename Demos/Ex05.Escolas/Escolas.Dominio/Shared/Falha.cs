using System;

namespace Escolas.Dominio.Shared
{
    public struct Falha
    {
        internal Falha(int codigo, string mensagem, string rastreamento)
        {
            Codigo = codigo;
            Mensagem = mensagem;
            Rastreamento = rastreamento;
        }

        public int Codigo { get; }
        public string Mensagem { get; }
        public string Rastreamento { get; }

        public static implicit operator Falha(Exception exception)
            => new Falha(9999, exception.Message, exception.StackTrace);

        public static Falha Nova(int codigo, string mensagem)
            => new Falha(codigo, mensagem, "");

        public static Falha NovaComException(Exception exception) => exception;
    }
}
