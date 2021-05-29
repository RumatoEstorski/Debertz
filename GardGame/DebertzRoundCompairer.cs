using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardGame
{
    public class DebertzRoundCompairer : IComparer<Card>
    {
        public DebertzRoundCompairer(CardSuite trump, CardSuite first)
        {
            Trump = trump;
            First = first;
        }

        CardSuite Trump { get; set; }
        CardSuite First { get; set; }
        public int Compare(Card x, Card y)
        {
            return ValueCard(x, Trump, First).CompareTo(ValueCard(y, Trump, First));
        }

        private int ValueCard(Card card, CardSuite trump, CardSuite first)
        {
            int value = (int)card.Figure;
            if (value == 14) value++;
            if (value == 10) value = 14;
            if (card.Suite == trump) value += 20;
            if (value == 29) value = 36;
            if (value == 31) value = 37;
            if (card.Suite != trump && card.Suite != first) value -= 20;
            return value;
        }
    }
}
