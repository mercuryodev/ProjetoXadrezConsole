using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public bool isTerminada { get; private set; }
        public int turno { get; private set;}
        public Cor jogadorAtual { get; private set;}

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            colocarPecas();
        }

        public void exectuaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino) 
        {
            exectuaMovimento(origem,destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.getPeca(pos) == null)
                throw new TabuleiroException("Não existe posição de origem escolhida!");
            if (jogadorAtual != tab.getPeca(pos).cor)
                throw new TabuleiroException("A peça de tabuleiro escolhido não é sua!");
            if (!tab.getPeca(pos).existeMovimentoPossiveis())
                throw new TabuleiroException("Não há movimentos possiveis para peça de origem escolhida!");
        }


        public void validarPosicaoDeDestino (Posicao origem, Posicao destino)
        {
            if (!tab.getPeca(origem).podeMoverPara(destino))
                throw new TabuleiroException("Posição de destino invalida!");
        }

        private void mudaJogador()
        {
            if(jogadorAtual.Equals(Cor.Amarela))
                jogadorAtual = Cor.Vermelha;
            else
                jogadorAtual = Cor.Amarela;
        }

        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Amarela), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Vermelha), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Vermelha), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Vermelha), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Vermelha), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Vermelha), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Vermelha), new PosicaoXadrez('d', 8).toPosicao());
        }

        public void imprimirJogadorAtual(Cor jogadorAtual)
        {
            ConsoleColor corOriginal = Console.ForegroundColor;
            if (jogadorAtual.Equals(Cor.Amarela))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(jogadorAtual);
                Console.ForegroundColor = corOriginal;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(jogadorAtual);
                Console.ForegroundColor = corOriginal;
            }
        }
    }
}
