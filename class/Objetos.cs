namespace AED_Search_Analysis
{
    class Quarto
    {
        public int idQuarto { get; private set; }
        public int idAnfitrião { get; set; }
        public string tipoQuarto { get; set; }
        public string país { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public int avaliações { get; set; }
        public double satisfaçãoGeral { get; set; }
        public int hóspedes { get; set; }
        public double quartos { get; set; }
        public double preço { get; set; }
        public string tipoPropriedade { get; set; }

        public Quarto(int _idQuarto)
        {
            idQuarto = _idQuarto;
        }
    }

    class Cidade
    {
        public string Nome { get; set; }
        public int Quartos { get; set; }
    }
}