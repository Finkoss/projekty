using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife_4ITB
{
    public class Cell {
        static List<Brush> brushes = new List<Brush>();
        
        public static void InitColors() { 
            for (int i = 2; i < 12; i++) {
                brushes.Add(new SolidBrush(Color.FromArgb(255-(int) (255f / i), 255-(int) (255f / i),255- (int) (255f / i))));
            }
        }

        private bool isAlive = false;

        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private int stepsDead = 0;

        public void DoStep() {
            if (IsAlive)
                stepsDead = 0;
            else {
                stepsDead++;
                if (stepsDead >= 10)
                    stepsDead = 9;
            }

        }

        private Brush GetBrush() {
           return brushes[stepsDead];
        } 


        public void Draw(Graphics g, int x, int y, int size) {
            g.FillRectangle(isAlive ? Brushes.Black : GetBrush(), x * size + 1, y * size + 1, size-1, size-1);
        }
    }
}
