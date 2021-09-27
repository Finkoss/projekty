using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife_4ITB
{
    public partial class Canvas : UserControl
    {
        int cellSize = 5;
        int cellCountWidth = 150;
        int cellCountHeight = 100;

        Cell[,] cells;
        bool isMouseDown = false;

        public Canvas() {
            InitializeComponent();
            cells = new Cell[cellCountWidth, cellCountHeight];
            CreateCells();

            cells[5, 5].IsAlive = true;
        }

        public void DoStep() {
            bool[,] newStates = new bool[cellCountWidth, cellCountHeight];
            for (int i = 0; i < cellCountWidth; i++) {
                for (int j = 0; j < cellCountHeight; j++) {

                    int count = GetNeighboursCount(i, j);
                    if(cells[i,j].IsAlive) {
                        if (count < 2)
                            newStates[i, j] = false;
                        if (count > 3)
                            newStates[i, j] = false;
                        if (count == 2 || count == 3)
                            newStates[i, j] = true;
                    } else {
                        if (count == 3)
                            newStates[i, j] = true;
                    }
                }
            }

            for (int i = 0; i < cellCountWidth; i++) {
                for (int j = 0; j < cellCountHeight; j++) {
                    cells[i, j].IsAlive = newStates[i, j];
                    cells[i, j].DoStep();
                }
            }
            Refresh();
        }

        private int GetNeighboursCount(int x, int y) {
            int count = 0;

            for(int i = x - 1; i <= x + 1; i++) {
                if (i < 0 || i >= cellCountWidth)
                    continue;
                for (int j = y - 1; j <= y + 1; j++) {
                    if (j < 0 || j >= cellCountHeight)
                        continue;
                    if(cells[i,j].IsAlive) {
                        count++;
                    }
                }
            }

            if (cells[x, y].IsAlive)
                count--;
                
            return count;
        }

        private void CreateCells() {
            for (int i = 0; i < cellCountWidth; i++) {
                for (int j = 0; j < cellCountHeight; j++) {
                    cells[i, j] = new Cell();
                }
            }
        }

        public void GenerateCells(int perc) {
            //int alive = 0;
            Random r = new Random();
            for (int i = 0; i < cellCountWidth; i++) {
                for (int j = 0; j < cellCountHeight; j++) {
                    cells[i, j].IsAlive = r.Next(0, 100) < perc;
                    //if (cells[i, j].IsAlive)
                    //    alive++;
                    
                }
            }

            //Console.WriteLine(((float)alive / (cellCountWidth * cellCountHeight) * 100) + "%");


            Refresh();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            for(int i = 1; i < cellCountWidth; i++) {
                e.Graphics.DrawLine(Pens.Gray, i * cellSize, 0, i * cellSize, Height);
            }
            for (int i = 1; i < cellCountHeight; i++) {
                e.Graphics.DrawLine(Pens.Gray, 0, i * cellSize, Width, i * cellSize);
            }

            for (int i = 0; i < cellCountWidth; i++) {
                for (int j = 0; j < cellCountHeight; j++) {
                    cells[i, j].Draw(e.Graphics, i, j, cellSize);
                }
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                isMouseDown = true;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                isMouseDown = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e) {
            if(isMouseDown) {
                int i = e.X / cellSize;
                int j = e.Y / cellSize;
                cells[i, j].IsAlive = !cells[i, j].IsAlive;
                Refresh();
            }
        }
    }
}
