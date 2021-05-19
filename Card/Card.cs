using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Card
    {
        public Card(CardSuite suite, CardFigure figure)
        {
            Suite = suite;
            Figure = figure;
            IsOpened = false;
        }

        public CardSuite Suite { get; set; }
        public CardFigure Figure { get; set; }

        public bool IsOpened { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj is Card ? Equals((Card)obj) : false;
        }

        public bool Equals(Card card)
        {
            if (card == null) return false;
            return card.Figure == Figure && card.Suite == Suite;
        }

        public override string ToString()
        {
            return $"{Figure} {Suite}!!!";
        }

        public virtual void Show()
        {
            IsOpened = true;
        }

        public virtual void Hide()
        {
            IsOpened = false;
        }

        public virtual void Rotate()
        {
            IsOpened = !IsOpened;
        }

        public override int GetHashCode()
        {
            var hashCode = Suite.GetHashCode();
            hashCode = hashCode * -1521134295 + Figure.GetHashCode();
            return hashCode;
        }
    }
}
