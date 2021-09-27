using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hra2048_4ITB
{
    public partial class Hra : Form
    {
        Cislo[,] cisla;
        Random random = new Random();

        public Hra() {
            InitializeComponent();
        }

        private void Hra_Load(object sender, EventArgs e) {
            StartNewGame();
            hraPanel.Focus();
        }

        private void StartNewGame() {
            cisla = new Cislo[Nastaveni.sirka, Nastaveni.vyska];

            for (int i = 0; i < Nastaveni.sirka; i++) {
                for (int j = 0; j < Nastaveni.vyska; j++) {
                    Cislo c = new Cislo();
                    cisla[i, j] = c;
                    hraPanel.Controls.Add(c);
                    c.Location = new Point(i * c.Width, j * c.Height);
                }
            }
            PlaceNewCislo();
            PlaceNewCislo();
        }

        private void PlaceNewCislo() {
            if (JeVseObsazeno())
                return;
            int cislo = GetStartingValue();

            int x, y;
            while (true) {
                x = random.Next(0, cisla.GetLength(0));
                y = random.Next(0, cisla.GetLength(1));

                if (cisla[x, y].Hodnota == 0) {
                    break;
                }
            }

            cisla[x, y].Hodnota = cislo;
        }

        private bool JeVseObsazeno() {
            for (int i = 0; i < cisla.GetLength(0); i++) {
                for (int j = 0; j < cisla.GetLength(1); j++) {
                    if (cisla[i, j].Hodnota == 0) {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckGameEnd() {
            
            // jsou vedle sebe čísla, která lze spojit?
            for (int i = 0; i < cisla.GetLength(0) - 1; i++) {
                for (int j = 0; j < cisla.GetLength(1) - 1; j++) {
                    if (cisla[i, j].Hodnota == cisla[i + 1, j].Hodnota
                     || cisla[i, j].Hodnota == cisla[i, j + 1].Hodnota) {
                        //Console.WriteLine("Lze spojit " + i + ":" + j + " s hodnotou " + cisla[i, j].Hodnota);
                        return false;
                    }
                }
            }

            return JeVseObsazeno();
        }

        private int GetStartingValue() {
            return random.Next(0, 100) < 70 ? 2 : 4;
        }

        private void SplitContainer1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) {
                ProcessMove(-1, 0);
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) {
                ProcessMove(1, 0);
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) {
                ProcessMove(0, 1);
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) {
                ProcessMove(0, -1);
            }
        }

        private void ProcessMove(int x, int y) {
            if (x == -1) { // doleva
                for (int i = 0; i < cisla.GetLength(1); i++) {

                    List<int> cislaVRadku = GetCislaVRadku(i);
                    Spoj(cislaVRadku, false);

                    for (int j = 0; j < cisla.GetLength(0); j++) {
                        if (j < cislaVRadku.Count) {
                            cisla[j, i].Hodnota = cislaVRadku[j];
                        } else {
                            cisla[j, i].Hodnota = 0;
                        }
                    }
                }
            }
            if (x == 1) { // doprava
                for (int i = 0; i < cisla.GetLength(1); i++) {
                    List<int> cislaVRadku = GetCislaVRadku(i);
                    Spoj(cislaVRadku, true);
                    for (int j = cisla.GetLength(0) - 1; j >= 0; j--) {
                        if (cislaVRadku.Count > 0) {
                            cisla[j, i].Hodnota = cislaVRadku[cislaVRadku.Count - 1];
                            cislaVRadku.RemoveAt(cislaVRadku.Count - 1);
                        } else {
                            cisla[j, i].Hodnota = 0;
                        }
                    }
                }
            }
            if (y == 1) { // nahoru
                for (int i = 0; i < cisla.GetLength(0); i++) {
                    List<int> cislaVeSloupci = GetCislaVeSloupci(i);
                    Spoj(cislaVeSloupci, false);

                    for (int j = 0; j < cisla.GetLength(1); j++) {
                        if (j < cislaVeSloupci.Count) {
                            cisla[i, j].Hodnota = cislaVeSloupci[j];
                        } else {
                            cisla[i, j].Hodnota = 0;
                        }
                    }
                }
            }
            if (y == -1) { // dolů
                for (int i = 0; i < cisla.GetLength(0); i++) {
                    List<int> cislaVeSloupci = GetCislaVeSloupci(i);
                    Spoj(cislaVeSloupci, true);

                    for (int j = cisla.GetLength(1) - 1; j >= 0; j--) {
                        if (cislaVeSloupci.Count > 0) {
                            cisla[i, j].Hodnota = cislaVeSloupci[cislaVeSloupci.Count - 1];
                            cislaVeSloupci.RemoveAt(cislaVeSloupci.Count - 1);
                        } else {
                            cisla[i, j].Hodnota = 0;
                        }
                    }
                }
            }
            PlaceNewCislo();
            if (CheckGameEnd()) {
                MessageBox.Show("Konec hry");
            }
        }

        private void VypisList(List<int> list) {
            string a = "";
            list.ForEach(c => a += c + ",");
            Console.WriteLine(a);
        }

        private void Spoj(List<int> cisla, bool odKonce) {

            if (odKonce) {
                for (int j = cisla.Count - 1; j > 0; j--) {
                    if (cisla[j] == cisla[j - 1]) {
                        cisla[j - 1] *= 2;
                        cisla.RemoveAt(j);
                        j--;
                    }
                }
            } else {
                for (int j = 0; j < cisla.Count - 1; j++) {
                    if (cisla[j] == cisla[j + 1]) {
                        cisla[j + 1] *= 2;
                        cisla.RemoveAt(j);
                        j++;
                    }
                }
            }
        }

        private List<int> GetCislaVeSloupci(int cislo) {
            List<int> cislaVeSloupci = new List<int>();
            for (int j = 0; j < cisla.GetLength(1); j++) {
                if (cisla[cislo, j].Hodnota > 0) {
                    cislaVeSloupci.Add(cisla[cislo, j].Hodnota);
                }
            }
            return cislaVeSloupci;
        }

        private List<int> GetCislaVRadku(int cislo) {
            List<int> cislaVRadku = new List<int>();

            for (int j = 0; j < cisla.GetLength(0); j++) {
                if (cisla[j, cislo].Hodnota > 0) {
                    cislaVRadku.Add(cisla[j, cislo].Hodnota);
                }
            }

            return cislaVRadku;
        }
    }
}
