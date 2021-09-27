using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hra2048_4ITB
{
    public partial class Cislo : UserControl
    {
        private int hodnota;
        public int Hodnota {
            get { return hodnota; }
            set {
                hodnota = value;
                label1.Text = Format(hodnota);
            }
        }

        public Cislo() {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            Hodnota = 0;
        }

        private string Format(int cislo) {
            if (cislo == 0)
                return "";
            return cislo.ToString();
        }
    }
}
