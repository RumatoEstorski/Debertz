using Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicCards
{
    public class GraphicCardSet : CardSet
    {
        public GraphicCardSet(Panel panel, GraphicCard[] cards) : base(cards)
        {
            Pnl = panel;
        }
        public GraphicCardSet(Panel panel, List<GraphicCard> cards) : this(panel, cards.ToArray()) { }

        public GraphicCardSet(Panel panel):base()
        {
            Pnl = panel;
        }

        public GraphicCardSet() : base()
        {
            Pnl = new Panel();
        }

        public Panel Pnl { get; set; }



        public void Draw()
        {
            Pnl.Controls.Clear();
            for (int i = 0; i < Count; i++)
            {
                GraphicCard card = (GraphicCard)Cards[i];
                PictureBox pb = card.Pb;
                Pnl.Controls.Add(pb);
                pb.BringToFront();
                pb.Size = new Size(pb.Image.Width * Pnl.Height / pb.Image.Height, Pnl.Height);
                pb.Location = new Point(i * (Pnl.Width - pb.Width) / Count, 0);
            }
        }

        public override Card GetCard(CardFigure figure, CardSuite suite)
        {
            return new GraphicCard(suite, figure);
        }

        public override CardSet GetCardSet()
        {
            return new GraphicCardSet();
        }
    }
}
