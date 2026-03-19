using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace EsLaghetto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] matrice = new int[10, 40];
        bool drawing = false;
        int cellSize = 30;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    int x = j * cellSize;
                    int y = i * cellSize;

                    g.DrawRectangle(Pens.Black, x, y, cellSize, cellSize);

                    if (matrice[i, j] == 1)
                        g.FillRectangle(Brushes.Black, x, y, cellSize, cellSize);

                    if (matrice[i, j] == 2)
                        g.FillRectangle(Brushes.Blue, x, y, cellSize, cellSize);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawing) return;

            int col = e.X / cellSize;
            int row = e.Y / cellSize;

            if (row >= 0 && row < matrice.GetLength(0) &&
                col >= 0 && col < matrice.GetLength(1))
            {
                matrice[row, col] = 1;
            }

            Invalidate();
        }

        void FloodFill(int r, int c)
        {
            if (r < 0 || r >= matrice.GetLength(0) ||
        c < 0 || c >= matrice.GetLength(1))
                return;

            if (matrice[r, c] != 0)
                return;

            matrice[r, c] = 2;

            FloodFill(r + 1, c);
            FloodFill(r - 1, c);
            FloodFill(r, c + 1);
            FloodFill(r, c - 1);
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int col = e.X / cellSize;
            int row = e.Y / cellSize;

            if (row >= 0 && row < matrice.GetLength(0) &&
                col >= 0 && col < matrice.GetLength(1))
            {
                FloodFill(row, col);
            }

            Invalidate();
        }
    }
}