using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardGame;
using Cards;
using GraphicCards;

namespace GraphicCardsNewPb
{
    public class GraphicGame : Game
    {
        public GraphicGame(Panel pnlDeck,
            Panel pnlTable,
            Action<string> showMessage, 
            Action<Player> markActivePlayer,
            Action<CardSet> showCards,
            params Player[] players) : base(showMessage, markActivePlayer, showCards, players)
        {
            ((GraphicCardSet)Table).Pnl = pnlTable;
            ((GraphicCardSet)Deck).Pnl = pnlDeck;
        }

        public override CardSet GetCardSet()
        {
            return new GraphicCardSet();
        }
    }
}
