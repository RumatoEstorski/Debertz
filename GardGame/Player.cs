
using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Player
    {
        public virtual string Name { get; set; }
        public CardSet HandCards { get; set; }
        public CardSet DiscardPile { get; set; } = new CardSet();
        public Card PleyerCard { get; set; }

    }

}
