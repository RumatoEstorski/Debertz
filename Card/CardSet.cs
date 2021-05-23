using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class CardSet
    {
        readonly Random random = new Random();

        public Card LastCard => Cards.Count > 0 ? Cards[Count - 1] : null;

        public CardSet()
        {
            Cards = new List<Card>();
        }

        public CardSet(List<Card> cards)
        {
            Cards = cards;
        }

        public CardSet(params Card[] cards)
        {
            Cards = new List<Card>(cards);
        }

        public List<Card> Cards { get; set; }
        public int Count
        {
            get
            {
                return Cards.Count;
            }
        }

        public Card this[int i]
        {
            get
            {
                return Cards[i];
            }
            set
            {
                Cards[i] = value;
            }
        }

        public void Full()
        {
            foreach (CardFigure figure in Enum.GetValues(typeof(CardFigure)))  
            {
                foreach (CardSuite suite in Enum.GetValues(typeof(CardSuite)))
                {
                    Cards.Add(GetCard(figure, suite));
                }
            }
        }

        public virtual Card GetCard(CardFigure figure, CardSuite suite)
        {
            return new Card(suite, figure);
        }

        public void Full(int amount)
        {
            Cards.Clear();
            Full();
            Sort();
            Cards.RemoveRange(0, Cards.Count - amount);
            
        }

        public virtual CardSet GetCardSet()
        {
            return new CardSet();
        }

        public void Mix(int count = 3)
        {
            Card card;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < Count - 1; j++)
                {
                    int numberOfCard = random.Next(0, Cards.Count - 1);
                    card = Cards[j];
                    Cards[j] = Cards[numberOfCard];
                    Cards[numberOfCard] = card;
                }
            }
        }

        public void Sort()
        {
            Cards.Sort((card1, card2) =>
                            card1.Figure.CompareTo(card2.Figure) == 0 ?
                            card1.Suite.CompareTo(card2.Suite):
                            card1.Figure.CompareTo(card2.Figure));
        }

        //Карты раздаются сверху колоды
        public CardSet Deal(int amount)
        {
            if (amount > Count) amount = Count;

            CardSet currentCardSet = new CardSet();
            for (int i = 0; i < amount; i++)
            {
                currentCardSet.Add(Cards[0]);
                Cards.RemoveAt(0);
            }
            return currentCardSet;
        }

        public Card Pull (int number = 0)
        {
            Card c = Cards[number];
            Cards.RemoveAt(number);
            return c;
        }

        public Card Pull(Card card)
        {
            Card foundCard = Cards.FirstOrDefault(c => c == card);
            if (foundCard != null) Cards.Remove(foundCard);
            return foundCard;
        }
        
        public void Add(params Card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                Cards.Add(cards[i]);
            }
        }

        public void Add(CardSet cards)
        {
            Add(cards.Cards);
        }

        public void Add(List<Card> cards)
        {
            Add(cards.ToArray());
        }

        public void Show()
        {
            foreach (var card in Cards)
            {
                card.Show();
            }
        }

        public void Hide()
        {
            foreach (var card in Cards)
            {
                card.Hide();
            }
        }

    }
}
