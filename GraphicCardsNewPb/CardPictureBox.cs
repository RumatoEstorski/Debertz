using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicCardsNewPb
{
    public class CardPictureBox:PictureBox
    {
        public CardPictureBox(Card card):base()
        {
            Card = card;
        }

        public Card Card { get; set; }
    }
}
