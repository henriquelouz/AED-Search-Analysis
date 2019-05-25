
using System.Collections.Generic;
namespace AED_Search_Analysis
{
    class Pesquisa
    {
        List<Quarto> Quartos;

        public Pesquisa(List<Quarto> _quartos)
        {
            Quartos = _quartos;
        }

        public Quarto Sequencial(int idQuarto)
        {
            foreach (Quarto q in Quartos)
                if (q.idQuarto == idQuarto)
                    return q;

            return default(Quarto);
        }

        public Quarto Binária(int idQuarto)
        {
            Quarto[] quartos = Quartos.ToArray();
            ordenarQuartos(quartos, 0, quartos.Length - 1);

            int início = 0, fim = quartos.Length - 1;
            while (início <= fim)
            {
                int meio = (início + fim) / 2;
                if (idQuarto == quartos[meio].idQuarto)
                    return quartos[meio];
                else if (idQuarto < quartos[meio].idQuarto)
                    fim = meio - 1;
                else
                    início = meio + 1;
            }

            return default(Quarto);
        }

        public Quarto Árvore(int idQuarto)
        {
            ÁrvoreBinária árvore = new ÁrvoreBinária();

            foreach (Quarto q in Quartos)
                árvore.Inserir(q);

            return árvore.Pesquisar(idQuarto);
        }

        private void ordenarQuartos(Quarto[] vetor, int esquerda, int direita)
        {
            int i = esquerda,
                j = direita;
            Quarto pivot = vetor[(esquerda + direita) / 2];

            while (i <= j)
            {
                while (vetor[i].idQuarto < pivot.idQuarto && i < direita) i++;
                while (vetor[j].idQuarto > pivot.idQuarto && j > esquerda) j--;

                if (i <= j)
                {
                    Quarto aux = vetor[i];
                    vetor[i] = vetor[j];
                    vetor[j] = aux;

                    i++;
                    j--;
                }
            }

            if (j > esquerda)
                ordenarQuartos(vetor, esquerda, j);

            if (i < direita)
                ordenarQuartos(vetor, i, direita);
        }


        public List<Cidade> QuartosPorCidade()
        {
            List<Cidade> cidades = new List<Cidade>();
            foreach (Quarto q in Quartos)
            {
                int i = índiceCidade(q.cidade, cidades);

                if (i >= 0)
                {
                    cidades[i].Quartos++;
                    continue;
                }
                cidades.Add(new Cidade() { Nome = q.cidade, Quartos = 1 });
            }
            return cidades;
        }

        private int índiceCidade(string cidade, List<Cidade> lista)
        {
            for (int i = 0; i < lista.Count; i++)
                if (lista[i].Nome == cidade)
                    return i;
            return -1;
        }


        public Quarto[] Preços(string cidade)
        {
            Quarto maior = default(Quarto),
                   menor = default(Quarto);
            double iMaior = int.MinValue,
                   iMenor = int.MaxValue;

            foreach (Quarto q in Quartos)
            {
                if (q.cidade == cidade)
                {
                    if (q.preço < iMenor)
                    {
                        menor = q;
                        iMenor = q.preço;
                    }
                    else if (q.preço > iMaior)
                    {
                        maior = q;
                        iMaior = q.preço;
                    }
                }
            }

            if (iMaior == int.MinValue && iMenor == int.MaxValue)
                return null;

            return new Quarto[]{
                maior,
                menor,
            };
        }
    }

    class ÁrvoreBinária
    {
        class No
        {
            public Quarto Item;
            public No Esq, Dir;
            public No(Quarto item)
            {
                Item = item;
                Esq = Dir = null;
            }
        }

        private No raiz;

        public ÁrvoreBinária()
        {
            raiz = null;
        }

        public Quarto Pesquisar(int código)
        {
            No no = pesquisar(raiz, código);
            if (no != null)
                return no.Item;
            return null;
        }
        private No pesquisar(No no, int idQuarto)
        {
            if (no == null)
                return null;
            else if (idQuarto < no.Item.idQuarto)
                return pesquisar(no.Esq, idQuarto);
            else if (idQuarto > no.Item.idQuarto)
                return pesquisar(no.Dir, idQuarto);

            return no;
        }

        public void Inserir(Quarto item)
        {
            raiz = inserir(raiz, item);
        }
        private No inserir(No no, Quarto item)
        {
            if (no == null)
                no = new No(item);
            else if (item.idQuarto < no.Item.idQuarto)
                no.Esq = inserir(no.Esq, item);
            else if (item.idQuarto > no.Item.idQuarto)
                no.Dir = inserir(no.Dir, item);
            return no;
        }
    }
}