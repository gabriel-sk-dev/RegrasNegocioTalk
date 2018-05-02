namespace Escolas.Dominio.Shared
{
    public struct NomeCompleto
    {
        public NomeCompleto(string primeiro, string segundo)
        {
            Primeiro = primeiro;
            Segundo = segundo;
        }
        public string Primeiro { get; }
        public string Segundo { get; }

        public override string ToString()
            => $"{Primeiro} {Segundo}";
    }
}
