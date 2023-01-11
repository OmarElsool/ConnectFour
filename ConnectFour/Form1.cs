using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            playerTurn.Text = "Player 1 Turn, Blue";
            playerTurn.ForeColor = Color.Blue;
        }

        Game game = new Game();

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // paint the sections and the empty Ellipse
            game.drawBoard(e);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int colNumber = game.ColumnNumber(e.Location);

            if (colNumber != -1)
            {
                int rowIndex = game.climbToNextRow(colNumber);
                if (rowIndex != -1)
                {
                    if (game.player1)
                    {
                        game.board[rowIndex / 100, colNumber] = state.Blue;
                        Graphics g = panel1.CreateGraphics();
                        g.FillEllipse(Brushes.Blue, colNumber * 100, rowIndex, 100, 100);
                    }
                    else
                    {
                        game.board[rowIndex / 100, colNumber] = state.Red;
                        Graphics g = panel1.CreateGraphics();
                        g.FillEllipse(Brushes.Red, colNumber * 100, rowIndex, 100, 100);
                    }
                    if (game.ifWon(game.pieceColor))
                    {
                        MessageBox.Show($"{game.pieceColor} Player Wins");
                        game.resetOrStartGame(); // reset the state to state.Empty
                        panel1.Invalidate(); // reset the paint draw
                        playerTurn.Text = "Player 1 Turn, Blue";
                        playerTurn.ForeColor = Color.Blue;
                        return;
                    }
                    if (game.IsFull())
                    {
                        MessageBox.Show("The Game Ended Draw!!");
                    }
                    game.playerTurn();
                    if (game.player1)
                    {
                        playerTurn.Text = game.pieceColor + ", Player 1 Turn";
                        playerTurn.ForeColor = Color.Blue;
                    }
                    else
                    {
                        playerTurn.Text = game.pieceColor + ", Player 2 Turn";
                        playerTurn.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Are you want to reset game ??",
                                     "Confirm Reset!!",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                game.resetOrStartGame(); // reset the state to state.Empty
                panel1.Invalidate(); // reset the paint draw
                playerTurn.Text = "Player 1 Turn, Blue";
                playerTurn.ForeColor = Color.Blue;
                return;
            }
        }
    }
}
