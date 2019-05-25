using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace AED_Search_Analysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Pesquisa pesquisa = new Pesquisa(extrair("dados_airbnb.txt"));

            imprimir(pesquisa.Sequencial(5752076));
            imprimir(pesquisa.Binária(5752076));
            imprimir(pesquisa.Árvore(5752076));
            pesquisa.QuartosPorCidade().ForEach(delegate (Cidade c) { Console.WriteLine("{0} - {1}", c.Nome, c.Quartos); });
            imprimirPreços(pesquisa.Preços("Paris"));
        }

        static List<Quarto> extrair(string dir)
        {
            List<Quarto> lista = new List<Quarto>();
            string[] linhas = File.ReadAllLines(dir).Skip(1).ToArray();

            foreach (string l in linhas)
            {
                string[] dados = l.Split('\t');
                lista.Add(
                    new Quarto(int.Parse(dados[0]))
                    {
                        idAnfitrião = int.Parse(dados[1]),
                        tipoQuarto = dados[2],
                        país = dados[3],
                        cidade = dados[4],
                        bairro = dados[5],
                        avaliações = int.Parse(dados[6]),
                        satisfaçãoGeral = double.Parse(dados[7], CultureInfo.InvariantCulture),
                        hóspedes = int.Parse(dados[8]),
                        quartos = double.Parse(dados[9], CultureInfo.InvariantCulture),
                        preço = double.Parse(dados[10], CultureInfo.InvariantCulture),
                        tipoPropriedade = dados[11],
                    }
                );
            }

            return lista;
        }

        static void imprimir(Quarto q)
        {
            if (q != null)
            {
                Console.WriteLine(
                    "{0} - {1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7:0.0}\t{8}\t{9:0.0}\t{10:0.0}\t{11}",
                    q.idQuarto,
                    q.idAnfitrião,
                    q.tipoQuarto,
                    q.país,
                    q.cidade,
                    q.bairro,
                    q.avaliações,
                    q.satisfaçãoGeral,
                    q.hóspedes,
                    q.quartos,
                    q.preço,
                    q.tipoPropriedade
                );
                return;
            }

            Console.WriteLine("Não encontrado.");
        }

        static void imprimirPreços(Quarto[] quartos)
        {
            if (quartos != null)
            {
                Console.WriteLine(quartos[0].cidade);

                Console.Write("Mais caro: ");
                imprimir(quartos[0]);

                Console.Write("Mais barato: ");
                imprimir(quartos[1]);

                return;
            }
            Console.WriteLine("Cidade não encontrada.");
        }
    }
}
