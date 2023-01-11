using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public enum state
    {
        Blue,
        Red,
        Empty
    };
    internal class Game
    {
        public int BoardWidth, BoardHeight;
        public static int Rows = 6;
        public int Columns = 7;
        int CellWidth;
        int CellHeight;
        public bool player1;
        public bool player2;
        public state pieceColor;
        public state[,] board = new state[6, 7];

        public Game()
        {
            BoardWidth = 700;
            BoardHeight = 600;
            player1 = true;
            player2 = false;
            pieceColor = state.Blue;
            CellWidth = BoardWidth / Columns;
            CellHeight = BoardHeight / Rows;
            resetOrStartGame();
        }
        public void resetOrStartGame()
        {
            player1 = true;
            player2 = false;
            pieceColor = state.Blue;
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    board[row, column] = state.Empty;
                }
            }
        }

        public void drawBoard(PaintEventArgs e)
        {
            Pen line = new Pen(Color.Black);
            int lineXi = 0, lineXf = BoardWidth; // horizontal 700
            int lineYi = 0, lineYf = BoardHeight; // vertical 600
            SolidBrush myBrush = new SolidBrush(Color.White);

            // draw line for each col
            for (int startY = 0; startY <= BoardWidth; startY += 100)
            {
                e.Graphics.DrawLine(line, startY, lineYi, startY, lineYf);
            }
            // draw line for each row
            for (int startX = 0; startX <= BoardHeight; startX += 100)
            {
                e.Graphics.DrawLine(line, lineXi, startX, lineXf, startX);
            }

            for (int y = 0; y < BoardHeight; y += 100)
            {
                for (int x = 0; x < BoardWidth; x += 100)
                {
                    e.Graphics.FillEllipse(myBrush, new Rectangle(x, y, 100, 100));
                }
            }

        }


        public int ColumnNumber(Point mouse)
        {

            for (int x = 0; x < BoardWidth; x += 100)
            {
                if (mouse.X >= x)
                {
                    if ((mouse.X < x + CellWidth))
                    {
                        //return col index
                        return x / 100;
                    }
                }
            }
            return -1;

        }

        public int climbToNextRow(int col)
        {
            if (!checkLastRow(col)) { return -1; }
            for (int i = Rows - 1; i >= 0; i--)
            {
                if (board[i, col] == state.Empty)
                {
                    return i * 100;
                }
            }
            return -1;
        }


        public void playerTurn()
        {
            player1 = !player1;
            player2 = !player2;
            if (player1)
            {
                pieceColor = state.Blue;
            }
            else
            {
                pieceColor = state.Red;
            }
        }

        // alogrithm for check if there is a winner
        public bool ifWon(state color)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (CheckVertically(row, column, color)) { return true; }
                    if (CheckHorizontally(row, column, color)) { return true; }
                    if (CheckDiagonallyDown(row, column, color)) { return true; }
                    if (CheckDiagonallyUp(row, column, color)) { return true; }
                }
            }

            return false;
        }


        private bool CheckHorizontally(int row, int column, state color)
        {

            if (column + 3 >= Columns) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (board[row, column + distance] != color) { return false; }
            }

            return true;
        }

        private bool CheckVertically(int row, int column, state color)
        {

            if (row + 3 >= Rows) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (board[row + distance, column] != color) { return false; }
            }

            return true;
        }

        private bool CheckDiagonallyDown(int row, int column, state color)
        {

            if (row + 3 >= Rows) { return false; }
            if (column + 3 >= Columns) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (board[row + distance, column + distance] != color) { return false; }
            }

            return true;
        }

        private bool CheckDiagonallyUp(int row, int column, state color)
        {

            if (row - 3 < 0) { return false; }
            if (column + 3 >= Columns) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (board[row - distance, column + distance] != color) { return false; }
            }

            return true;
        }

        public bool IsFull()
        {
            for (int column = 0; column < Columns; column++)
            {
                if (checkLastRow(column)) { return false; }
            }

            return true;
        }

        // check if there is space empty in last row in each column
        public bool checkLastRow(int column)
        {
            if (board[0, column] == state.Empty) { return true; }
            else { return false; }
        }

    }
}
