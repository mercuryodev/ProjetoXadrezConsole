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
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
                capturadas.Add(pecaCapturada);
            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino) 
        {
            Peca pecacapturada = executaMovimento(origem,destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecacapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
                xeque = true;
            else
                xeque = false;
               
            turno++;
            mudaJogador();
        }

        private void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
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
            if (!tab.getPeca(origem).podeMoverPara(destino) || destino == null)
                throw new TabuleiroException("Posição de destino invalida!");
        }

        private void mudaJogador()
        {
            if(jogadorAtual.Equals(Cor.Amarela))
                jogadorAtual = Cor.Vermelha;
            else
                jogadorAtual = Cor.Amarela;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor.Equals(cor))
                    aux.Add(x);
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor.Equals(cor))
                    aux.Add(x);
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor.Equals(Cor.Amarela))
                return Cor.Vermelha;
            else
                return Cor.Amarela;
        }

        private Peca rei(Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                    return x;
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);

            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                    return true;
            }
            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('c',1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('c',2, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('d',2, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('e',1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('e',2, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('d',1, new Rei(tab, Cor.Amarela));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Vermelha));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Vermelha));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Vermelha));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Vermelha));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Vermelha));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Vermelha));
        }

        public void imprimirJogadorAtual(Cor jogadorAtual)
        {
            ConsoleColor corOriginal = Console.ForegroundColor;
            if (jogadorAtual.Equals(Cor.Amarela))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(jogadorAtual);
                Console.ForegroundColor = corOriginal;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(jogadorAtual);
                Console.ForegroundColor = corOriginal;
            }
        }
    }
}
