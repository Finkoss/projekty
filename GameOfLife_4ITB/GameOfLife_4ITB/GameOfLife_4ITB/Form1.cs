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
    public partial class Form1 : Form
    {
        public Form1() {
            Cell.InitColors();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            canvas1.GenerateCells((int)numericUpDown1.Value);
        }

        private void button2_Click(object sender, EventArgs e) {
            canvas1.DoStep();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            timer1.Interval = trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e) {
            timer1.Enabled = !timer1.Enabled;
            button3.Text = timer1.Enabled ? "Stop" : "Start";
        }

        private void timer1_Tick(object sender, EventArgs e) {
            canvas1.DoStep();
        }
    }
}
