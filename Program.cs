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

            Console.WriteLine("******************************");
            Console.WriteLine("* Pesquisa de quartos Airbnb *");
            Console.WriteLine("******************************");
            int comando;

            do
            {
                Console.WriteLine("\nOpções:");
                Console.Write("[0] Sair\n[1] Sequencial\n[2] Binária\n[3] Árvore\n[4] Quartos por cidade\n[5] Preços\n:");
                comando = int.Parse(Console.ReadLine());

                switch (comando)
                {
                    case 0:
                        break;
                    case 1:
                        Console.Write("ID do quarto: ");
                        imprimir(pesquisa.Sequencial(int.Parse(Console.ReadLine())));
                        break;
                    case 2:
                        Console.Write("ID do quarto: ");
                        imprimir(pesquisa.Binária(int.Parse(Console.ReadLine())));
                        break;
                    case 3:
                        Console.Write("ID do quarto: ");
                        imprimir(pesquisa.Árvore(int.Parse(Console.ReadLine())));
                        break;
                    case 4:
                        pesquisa.QuartosPorCidade().ForEach(delegate (Cidade c) { Console.WriteLine("{0} - {1}", c.Nome, c.Quartos); });
                        break;
                    case 5:
                        Console.Write("Cidade: ");
                        imprimirPreços(pesquisa.Preços(Console.ReadLine()));
                        break;
                    default:
                        Console.WriteLine("Comando inválido.");
                        break;
                }
            } while (comando != 0);
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
