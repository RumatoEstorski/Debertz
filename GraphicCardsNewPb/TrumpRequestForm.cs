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
        public CardSuite Suite { get; set; }
        bool Pass { get; set; }
        public TrumpRequestForm(bool pass)
        {
            InitializeComponent();
            pass = Pass;
            //button.Visible = pass;
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
            if (Pass == true) Close();
        }
    }
}
