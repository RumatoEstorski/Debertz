using Cards;
using GraphicCardsNewPb;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GraphicCards
{
    public class GraphicCard : Card
    {
        private readonly static Dictionary<Card, Image> images;
        private readonly static Image faceDownImage;
        public CardPictureBox Pb { get; }

        
        public GraphicCard(CardSuite suite, CardFigure figure) :base(suite, figure)
        {
            Pb = new CardPictureBox(this)
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = IsOpened ? images[this] : faceDownImage
            };
        }
        public GraphicCard(CardPictureBox pb, CardSuite suite, CardFigure figure) : base(suite, figure)
        {
            Pb = pb;
            Pb.SizeMode = PictureBoxSizeMode.Zoom;
            Pb.Image = IsOpened ? images[this] : faceDownImage;
        }

        static GraphicCard()
        {
            images = new Dictionary<Card, Image>();
            GraphicCardSet cardSet = new GraphicCardSet(new Panel());
            cardSet.Full();

            foreach (Card card in cardSet.Cards)
            {
                Image image = Image.FromFile($"{Application.StartupPath}\\images\\{card.Suite}s {card.Figure}.png");
                images.Add(card, image);
            }

            faceDownImage = Image.FromFile($"{Application.StartupPath}\\images\\12.jpg");
        }

        public override void Show()
        {
            base.Show();
            Pb.Image = images[this];
        }

        public override void Hide()
        {
            base.Hide();
            Pb.Image = faceDownImage;
        }

        public override void Rotate()
        {
            base.Rotate();
            Pb.Image = IsOpened ? images[this] : faceDownImage;
        }
    }
}
