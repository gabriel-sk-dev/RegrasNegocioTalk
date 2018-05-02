namespace Escolas.Dominio.Shared
{
    public struct Endereco
    {
        public Endereco(string rua, string numero, string complemento, string bairro, string cidade, string estado, int distanciaAteClube)
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            DistanciaAteClube = distanciaAteClube;
        }

        public string Rua { get; }
        public string Numero { get; }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Estado { get; }
        public int DistanciaAteClube { get;}
    }
}
