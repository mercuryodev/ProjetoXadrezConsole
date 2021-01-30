using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString() => "T";

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.getPeca(pos);
            return p == null || p.cor != cor;
        }
        
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.isPosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                    break;
                pos.linha = pos.linha - 1;
            }

            //abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.isPosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                    break;
                pos.linha = pos.linha + 1;
            }

            //direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.isPosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                    break;
                pos.coluna = pos.coluna + 1;
            }

            //direita
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.isPosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                    break;
                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }

    }
}
