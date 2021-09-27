using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama4ITB
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
            sachovnice1.OnKamenyChanged += OnKamenyChanged;
        }

        private void OnKamenyChanged(int prvni, int druhy) {

            label1.Text = prvni.ToString();
            label2.Text = druhy.ToString();

            if(prvni == 0) {
                MessageBox.Show("Vyhrál druhý hráč!");
            } else if(druhy == 0) {
                MessageBox.Show("Vyhrál první hráč");
            }
        }
    }
}
