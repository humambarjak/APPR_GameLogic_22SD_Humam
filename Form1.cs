using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APPR_GameLogic_22SD_Humam
{
    public partial class Form1 : Form
    {
        PictureBox pcbForm = null;
        PictureBox pcbTo = null;
        // The color for champions that can be picked
        Color selectableColor = Color.LightSlateGray;
        // The color for champions that Cant be picked
        Color alreadyUsedColor = Color.Brown;
        //The color for each player
        Color playerLeftColor = Color.Red;
        Color playerRightColor = Color.Blue;
        Color playersTurnColor;  // Correctly declared at class level
        //The color for the currently playing player
        Color playersTunColor;
        //Which players turn is is
        GroupBox selectedGroupbox = null;
        Random randomGenerator = new Random();
        int championsPickedCount = 0;
        string playersTurn = "";

        public Form1()
        {
            InitializeComponent();
        }
       

        private void pcbChampionsAll_MouseDown(object sender, MouseEventArgs e)
        {
            pcbForm = (PictureBox)sender;
            if (pcbForm.BackColor == selectableColor)
            {
                if (playersTurn == "Left")
                {
                    selectedGroupbox = gbxPlayerOne;
                }
                else
                {
                    selectedGroupbox = gbxPlayerTwo;
                }

                foreach (PictureBox pictureBox in selectedGroupbox.Controls.OfType<PictureBox>())
                {
                    if (pictureBox.Image == null)
                    {
                        pictureBox.BackColor = Color.Green;
                    }
                    else
                    {
                        pictureBox.BackColor = Color.Transparent;
                    }
                }
                pcbForm.DoDragDrop(pcbForm.Image, DragDropEffects.Copy);
            }

        }
        public void ResetPlayersColor() 
        {
            //Makes all the pictureboxes that dont have an image there own players color
            foreach (PictureBox pictureBox in selectedGroupbox.Controls.OfType<PictureBox>())
            {
                if (pictureBox.Image == null)
                {
                    pictureBox.BackColor = playersTurnColor;
                }
                else
                {
                    pictureBox.BackColor = Color.Transparent;
                }
            }

        }
        /// <summary>
        /// who wins doesnt matter but this is a way of showing it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            gbxChampions.Enabled = false;
            btnFight.Enabled = false;

            foreach (PictureBox pictureBox in gbxChampions.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = selectableColor;
            }

            foreach (PictureBox pictureBox in gbxPlayerOne.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = playerLeftColor;
                pictureBox.AllowDrop = true;
            }

            foreach (PictureBox pictureBox in gbxPlayerTwo.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = playerRightColor;
                pictureBox.AllowDrop = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            gbxChampions.Enabled = true; // Schakel de selectie van champions in
            btnFight.Enabled = false; // Schakel de vechtknop uit tot alle champions zijn geselecteerd
            playersTurn = "Left"; // Begin met de linker speler
            championsPickedCount = 0; // Reset het aantal gekozen champions
            lblPlayersTurn.Text = "< Turn red"; // Update de beurt indicator
            ChangeTurns(); // Roep de methode aan om de beurt te wisselen en de UI bij te werken
        }
        ///<summary>
        ///Show the player who's turn it is
        /// </summary>
        public void ChangeTurns() 
        {
            if (playersTurn == "Left")
            {
                lblPlayersTurn.Text = "< Turn red";
                playersTurnColor = playerLeftColor;
                selectedGroupbox = gbxPlayerOne;
            }
            else
            {
                lblPlayersTurn.Text = "Turn blue >";
                playersTurnColor = playerRightColor;
                selectedGroupbox = gbxPlayerTwo;
            }

            ResetPlayersColor();
        }

        private void btnFight_Click(object sender, EventArgs e)
        {
            int playerLeftPowerlevel = randomGenerator.Next(1, 10001);
            int playerRightPowerlevel = randomGenerator.Next(1, 10001);

            if (playerLeftPowerlevel > playerRightPowerlevel)
            {
                gbxPlayerOne.BackColor = Color.Orange;
                lblPlayersTurn.Text = "Red team won with a power level of : " + playerLeftPowerlevel;
            }
            else if (playerRightPowerlevel > playerLeftPowerlevel)
            {
                gbxPlayerTwo.BackColor = Color.Orange;
                lblPlayersTurn.Text = "Blue team won with a power level of : " + playerRightPowerlevel;
            }
            else
            {
                lblPlayersTurn.Text = "It's a draw, both teams had :" + playerLeftPowerlevel + " as a power level";
            }
            btnStart.Enabled = true;
            btnFight.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Do you want to play again ?", "Play again?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                Application.Exit();
            }
        }
        ///<Summary>
        ///Resets all values so you can play a new game
        /// </Summary>
        public void RestartGame() 
        {
            gbxChampions.Enabled = true;
            btnStart.Enabled = false;
            playersTurn = "Left";
            championsPickedCount = 0;
            lblPlayersTurn.Text = "< Turn red";
            ChangeTurns();
            gbxPlayerOne.BackColor = Color.Transparent;
            gbxPlayerTwo.BackColor = Color.Transparent;

            foreach (PictureBox pictureBox in gbxPlayerOne.Controls.OfType<PictureBox>())
            {
                pictureBox.Image = null;
                pictureBox.BackColor = playerLeftColor;
            }

            foreach (PictureBox pictureBox in gbxPlayerTwo.Controls.OfType<PictureBox>())
            {
                pictureBox.Image = null;
                pictureBox.BackColor = playerRightColor;
            }

            foreach (PictureBox pictureBox in gbxChampions.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = selectableColor;
            }
        }

        private void pcbPlayers_DragDrop(object sender, DragEventArgs e)
        {
            pcbForm.BackColor = alreadyUsedColor;
            pcbTo = (PictureBox)sender;
            Image getPicture = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            pcbTo.Image = getPicture;

            ResetPlayersColor();

            // Corrected assignment from '==' to '='
            if (playersTurn == "Left")
            {
                playersTurn = "Right";
            }
            else
            {
                playersTurn = "Left";
            }
            ChangeTurns();
            championsPickedCount++;
            if (championsPickedCount == 6)
            {
                lblPlayersTurn.Text = "All champions are picked. Let them fight!";
                btnFight.Enabled = true;
            }

        }
        private void pcbPlayers_DragOver(object sender, DragEventArgs e)
        {
            //only lets the image be dropped if pcb == green
            pcbTo = (PictureBox)sender;
            if (pcbTo.BackColor == Color.Green)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }
    }
}
