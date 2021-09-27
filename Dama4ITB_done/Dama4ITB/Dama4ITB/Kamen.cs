using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama4ITB
{
    public class Kamen
    {
        protected bool jePrvniHrac;
        public bool JePrvniHrac => jePrvniHrac;
        public Kamen(bool jePrvniHrac) {
            this.jePrvniHrac = jePrvniHrac;
        }

        public virtual void Vykresli(Graphics g, Policko p) {
            g.FillEllipse(
                jePrvniHrac ? Brushes.Yellow : Brushes.Brown,
                p.X * p.Velikost + p.Velikost / 5,
                p.Y * p.Velikost + p.Velikost / 5,
                p.Velikost / 5 * 3,
                p.Velikost / 5 * 3
                );
        }

        public virtual List<Policko> GetPolickaProPohyb(Policko[,] policka, Policko p) {
            List<Policko> vysledne = new List<Policko>();
            
            if (jePrvniHrac) {
                if (p.X - 1 >= 0 && p.Y + 1 < policka.GetLength(0)) { // pokud je políčko na šachovnici
                    if (policka[p.X - 1, p.Y + 1].Kamen != null) { // pokud je v cestě kámen
                        if (!policka[p.X - 1, p.Y + 1].Kamen.jePrvniHrac) { // je to kámen protihráče?
                            if (p.X - 2 >= 0 && p.Y + 2 < policka.GetLength(0)) { // dá se za něj skočit?
                                if (policka[p.X - 2, p.Y + 2].Kamen == null) { // není za ním další kámen
                                    vysledne.Add(policka[p.X - 2, p.Y + 2]); //můžeš skočit
                                }
                            }
                        }
                    } else {
                        vysledne.Add(policka[p.X - 1, p.Y + 1]);
                    }
                }

                if (p.X + 1 < policka.GetLength(0) && p.Y + 1 < policka.GetLength(0)) { // pokud je políčko na šachovnici
                    if (policka[p.X + 1, p.Y + 1].Kamen != null) { // pokud je v cestě kámen
                        if (!policka[p.X + 1, p.Y + 1].Kamen.jePrvniHrac) { // je to kámen protihráče?
                            if (p.X + 2 < policka.GetLength(0) && p.Y + 2 < policka.GetLength(0)) { // dá se za něj skočit?
                                if (policka[p.X + 2, p.Y + 2].Kamen == null) { // není za ním další kámen
                                    vysledne.Add(policka[p.X + 2, p.Y + 2]); //můžeš skočit
                                }
                            }
                        }
                    } else {
                        vysledne.Add(policka[p.X + 1, p.Y + 1]);
                    }
                }
            } else {
                if (p.X - 1 >= 0 && p.Y - 1 >= 0) { // pokud je políčko na šachovnici
                    if (policka[p.X - 1, p.Y - 1].Kamen != null) { // pokud je v cestě kámen
                        if (policka[p.X - 1, p.Y - 1].Kamen.jePrvniHrac) { // je to kámen protihráče?
                            if (p.X - 2 >= 0 && p.Y - 2 >= 0) { // dá se za něj skočit?
                                if (policka[p.X - 2, p.Y - 2].Kamen == null) { // není za ním další kámen
                                    vysledne.Add(policka[p.X - 2, p.Y - 2]); //můžeš skočit
                                }
                            }
                        }
                    } else {
                        vysledne.Add(policka[p.X - 1, p.Y - 1]);
                    }
                }

                if (p.X + 1 < policka.GetLength(0) && p.Y - 1 >= 0) { // pokud je políčko na šachovnici
                    if (policka[p.X + 1, p.Y - 1].Kamen != null) { // pokud je v cestě kámen
                        if (policka[p.X + 1, p.Y - 1].Kamen.jePrvniHrac) { // je to kámen protihráče?
                            if (p.X + 2 < policka.GetLength(0) && p.Y - 2 >= 0) { // dá se za něj skočit?
                                if (policka[p.X + 2, p.Y - 2].Kamen == null) { // není za ním další kámen
                                    vysledne.Add(policka[p.X + 2, p.Y - 2]); //můžeš skočit
                                }
                            }
                        }
                    } else {
                        vysledne.Add(policka[p.X + 1, p.Y - 1]);
                    }
                }
            }
            return vysledne;
        }
    }
}
