using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama4ITB
{
    public partial class Sachovnice : UserControl
    {
        public event Action<int, int> OnKamenyChanged;

        Policko[,] policka = new Policko[8, 8];
        int velikostPolicka = 100;

        bool hrajePrvni = true;
        List<Policko> oznacena;

        Policko zvolenePolicko;
        Policko ZvolenePolicko {
            get { return zvolenePolicko; }
            set {
                if (zvolenePolicko == value) {
                    zvolenePolicko = null;
                } else {
                    zvolenePolicko = value;
                }
                OdoznacVse();
                if (zvolenePolicko != null)
                    OznacProPolicko(zvolenePolicko);
                // highlight for current

            }
        }

        public Sachovnice() {
            InitializeComponent();
            VytvorPolicka();
        }

        private void OznacProPolicko(Policko p) {
            oznacena = p.Kamen.GetPolickaProPohyb(policka, p);
            OznacPolicka(oznacena);
        }

        private void OznacPolicka(List<Policko> ps) {
            ps.ForEach(p => OznacPolicko(p));
        }

        private void OznacPolicko(Policko p) {
            p.Oznaceno = true;
        }

        private void OdoznacVse() {
            foreach(var p in policka) {
                p.Oznaceno = false;
            }
        }

        private void KlikNaPolicko(Policko p) {
            if(p.Kamen == null && oznacena != null && oznacena.Contains(p)) {
                Pohni(p);
            } else {
                if (p.Kamen != null && p.Kamen.JePrvniHrac == hrajePrvni) {
                    ZvolenePolicko = p;
                }
            }
            Refresh();
        }

        private void Pohni(Policko p) {
            ZkusOdebratKamen(p);
            p.Kamen = ZvolenePolicko.Kamen;
            ZvolenePolicko.Kamen = null;
            oznacena.ForEach(x => x.Oznaceno = false);
            oznacena.Clear();
            ZkusVytvoritDamy();
            PrepniHrace();
        }

        private void ZkusVytvoritDamy() {
            if(hrajePrvni) {
                for (int i = 0; i < 8; i++) { 
                    if(policka[i, 7].Kamen != null && policka[i, 7].Kamen.JePrvniHrac) {
                        if(!(policka[i, 7].Kamen is Dama)) {
                            policka[i, 7].Kamen = new Dama(policka[i, 7].Kamen.JePrvniHrac);
                        }
                    }
                }
            } else {
                for (int i = 0; i < 8; i++) {
                    if (policka[i, 0].Kamen != null && !policka[i, 0].Kamen.JePrvniHrac) {
                        if (!(policka[i, 0].Kamen is Dama)) {
                            policka[i, 0].Kamen = new Dama(policka[i, 0].Kamen.JePrvniHrac);
                        }
                    }
                }
            }
        }

        private void ZkusOdebratKamen(Policko p) {
            if(Math.Abs(zvolenePolicko.X - p.X) > 1) {

                int pocet = Math.Abs(zvolenePolicko.X - p.X);

                int hor = (p.X - zvolenePolicko.X) / Math.Abs(zvolenePolicko.X - p.X);
                int vert = (p.Y - zvolenePolicko.Y) / Math.Abs(zvolenePolicko.Y - p.Y);

                for(int i = 1; i < pocet; i++) {
                    policka[zvolenePolicko.X + i * hor, zvolenePolicko.Y + i * vert].Kamen = null;
                }

                //policka[x, y].Kamen = null;
                OnKamenyChanged?.Invoke(GetPocetZbyvajicichKamenu(true), GetPocetZbyvajicichKamenu(false));
            }
        }

        private int GetPocetZbyvajicichKamenu(bool hrac) {
            int counter = 0;
            foreach(var p in policka) {
                if (p.Kamen != null && p.Kamen.JePrvniHrac == hrac)
                    counter++;
            }
            return counter;
        }

        private void PrepniHrace() {
            hrajePrvni = !hrajePrvni;
        }

        private void VytvorPolicka() {
            bool jeBily = false;
            for (int i = 0; i < policka.GetLength(0); i++) {
                for(int j = 0; j < policka.GetLength(1); j++) {

                    jeBily = ((i % 2 == 0 ? 0 : 1) + policka.GetLength(0) * i + j) % 2 == 0;
                    policka[j, i] = new Policko(velikostPolicka, j, i, jeBily);

                    if (!jeBily) {
                        if (i < 1) { // vytvoření bílých kamenů
                            policka[j, i].Kamen = new Kamen(true);
                        }
                        if (i == 2) { // vytvoření černých kamenů
                            policka[j, i].Kamen = new Kamen(false);
                        }
                    }
                }
            }
        }

        private void Sachovnice_Paint(object sender, PaintEventArgs e) {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var policko in policka) {
                policko.Vykresli(e.Graphics);
            }
        }

        private void Sachovnice_Load(object sender, EventArgs e) {
            this.Width = 8 * velikostPolicka;
            this.Height = 8 * velikostPolicka;

            OnKamenyChanged?.Invoke(GetPocetZbyvajicichKamenu(true), GetPocetZbyvajicichKamenu(false));
        }

        private void Sachovnice_MouseClick(object sender, MouseEventArgs e) {
            int x = e.X / velikostPolicka;
            int y = e.Y / velikostPolicka;
            x = Math.Min(x, policka.GetLength(0) - 1);
            y = Math.Min(y, policka.GetLength(0) - 1);
            KlikNaPolicko(policka[x, y]);
        }
    }
}
