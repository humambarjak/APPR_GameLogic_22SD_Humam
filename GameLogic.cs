using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPR_GameLogic_22SD_Humam
{
    internal class GameLogic
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player CurrentTurn { get; set; }

        public GameLogic()
        {
            PlayerOne = new Player { Name = "Player One", PlayerColor = Color.Red };
            PlayerTwo = new Player { Name = "Player Two", PlayerColor = Color.Blue };
            CurrentTurn = PlayerOne;
        }

        public void ChangeTurns()
        {
            CurrentTurn = CurrentTurn == PlayerOne ? PlayerTwo : PlayerOne;
        }
    }
}
