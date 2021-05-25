using Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicCardsNewPb
{

    public partial class TrumpRequestForm : Form
    {

        public CardSuite? Suite { get; set; } = null;
        public bool Pass { get; set; }
        public TrumpRequestForm(bool pass)
        {
            Random rnd = new Random();
            InitializeComponent();
            Pass = pass;
            buttonPass.Visible = pass;
            if (!pass)
                Suite = (CardSuite)rnd.Next(3);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Suite = CardSuite.Diamond;
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Suite = CardSuite.Spade;
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Suite = CardSuite.Club;
            Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Suite = CardSuite.Heart;
            Close();
        }

        private void buttonPass_Click(object sender, EventArgs e)
        {
            Suite = null;
            Close();
        }
    }
}
