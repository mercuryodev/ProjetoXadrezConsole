using System;
using tabuleiro;
using xadrez;

namespace ProjetoXadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Vermelha), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Vermelha), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Vermelha), new Posicao(2, 4));

                tab.colocarPeca(new Rei(tab, Cor.Amarela), new Posicao(7,5));
                tab.colocarPeca(new Torre(tab, Cor.Amarela), new Posicao(5,6));
                tab.colocarPeca(new Torre(tab, Cor.Amarela), new Posicao(6,2));

                Tela.ImprimirTabuleiro(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}