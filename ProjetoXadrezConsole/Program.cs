using System;
using tabuleiro;
using xadrez;

namespace ProjetoXadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez p = new PosicaoXadrez('c', 7);

            Console.WriteLine(p);

            Console.WriteLine(p.toPosicao());

            //try
            //{
            //    Tabuleiro tab = new Tabuleiro(8, 8);

            //    tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            //    tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            //    tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

            //    Tela.ImprimirTabuleiro(tab);
            //}
            //catch (TabuleiroException e) 
            //{
            //    Console.WriteLine(e.Message);
            //}
            Console.ReadLine();
        }
    }
}
