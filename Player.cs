using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPR_GameLogic_22SD_Humam
{
    internal class Player
    {
        public string Name { get; set; }
        public List<Champion> Champions { get; set; }
        public Color PlayerColor { get; set; }

        public Player()
        {
            Champions = new List<Champion>();
        }
    }
}
