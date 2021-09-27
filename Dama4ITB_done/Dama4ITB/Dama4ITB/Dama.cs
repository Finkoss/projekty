using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama4ITB
{
    public class Dama : Kamen
    {
        Pen damaPenOutline;

        public Dama(bool jePrvniHrac) : base(jePrvniHrac) {
            damaPenOutline = new Pen(Color.Red, 4f);
        }

        public Dama(bool jePrvniHrac, Policko p) : this(jePrvniHrac) {
            p.Kamen = this;
        }

        public override void Vykresli(Graphics g, Policko p) {
            g.FillEllipse(
                jePrvniHrac ? Brushes.Yellow : Brushes.Brown,
                p.X * p.Velikost + p.Velikost / 5,
                p.Y * p.Velikost + p.Velikost / 5,
                p.Velikost / 5 * 3,
                p.Velikost / 5 * 3
                );
            g.DrawEllipse(
                damaPenOutline,
                p.X * p.Velikost + p.Velikost / 5,
                p.Y * p.Velikost + p.Velikost / 5,
                p.Velikost / 5 * 3,
                p.Velikost / 5 * 3
                );
        }

        public override List<Policko> GetPolickaProPohyb(Policko[,] policka, Policko p) {
            List<Policko> vyseldne = new List<Policko>();

            //0,1,2,3
            for (int j = 0; j < 4; j++) {

                int hor = j % 2 == 0 ? -1 : 1;
                int vert = j / 2 == 0 ? -1 : 1;

                /*
                 0 -1 -1
                 1  1 -1
                 2 -1  1
                 3  1  1
                 */

                bool naselNepritele = false;
                for (int i = 1; i < 7; i++) {
                    if (JeNaSachovnici(p.X + (i * hor)) && JeNaSachovnici(p.Y + (i * vert))) { // je stále na šachovnici?
                        if (policka[p.X + (i * hor), p.Y + (i * vert)].Kamen == null) { // pokud není kámen
                            vyseldne.Add(policka[p.X + (i * hor), p.Y + (i * vert)]);
                        } else {
                            if (naselNepritele) {
                                naselNepritele = false;
                                break;
                            }
                            if (policka[p.X + (i * hor), p.Y + (i * vert)].Kamen.JePrvniHrac == JePrvniHrac) {
                                break;
                            } else {
                                naselNepritele = true;
                            }
                        }
                    }
                }
            }

            return vyseldne;
        }

        private bool JeNaSachovnici(int a) {
            return a >= 0 && a <= 7;
        }
    }
}
