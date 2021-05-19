using CardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicCardsNewPb
{
    class GraphicPlayer:Player
    {
        public Label lblName { get; set; } 

        public override string Name
        { 
            get
            {
                return base.Name;
            }
            set 
            { 
                base.Name = value; lblName.Text = Name; 
            } 
        }
    }
}
