using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama4ITB
{
    public class Policko
    {
        int velikost;
        public int Velikost => velikost;
        int x, y;
        public int X => x;
        public int Y => y;

        bool jeBily;

        public bool Oznaceno { get; set; }

        Kamen kamen;
        public Kamen Kamen {
            get { return kamen; }
            set { kamen = value; }
        }

        public Policko(int velikost, int x, int y, bool jeBily) {
            this.velikost = velikost;
            this.x = x; // pořadí x
            this.y = y; // pořadí y
            this.jeBily = jeBily;
        }

        public void Vykresli(Graphics g) {
            if(Oznaceno) {
                g.FillRectangle(Brushes.Aqua, x * velikost, y * velikost, velikost, velikost);
            } else {
                g.FillRectangle(jeBily ? Brushes.White : Brushes.Black, x * velikost, y * velikost, velikost, velikost);
            }

            if (kamen != null) {
                kamen.Vykresli(g, this);
            }
        }
    }
}
