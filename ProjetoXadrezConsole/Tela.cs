using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace ProjetoXadrezConsole
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.turno}");
            Console.Write("Aguardando jogada: ");
            partida.imprimirJogadorAtual(partida.jogadorAtual);
            if (partida.xeque)
            {
                ConsoleColor original = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("XEQUE!");
                Console.ForegroundColor = original;
            }

        }

        private static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.WriteLine("Pecas capturadas:");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Vermelhas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Vermelha));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Amarelas:  ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Amarela));
            Console.WriteLine();

            Console.ForegroundColor = original;
        }

        private static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    ImprimirPeca(tab.getPeca(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGreen;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i,j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;
                    ImprimirPeca(tab.getPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            if (string.IsNullOrEmpty(s))
                throw new TabuleiroException("Posição invalida!");

            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            if (coluna > 'h' || linha > 8)
                throw new TabuleiroException("Posição invalida!");

            return new PosicaoXadrez(coluna,linha);
        } 

        public static void ImprimirPeca (Peca peca)
        {
            if (peca == null)
                Console.Write("- ");
            else
            {
                if (peca.cor.Equals(Cor.Amarela))
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                else if (peca.cor.Equals(Cor.Vermelha))
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}