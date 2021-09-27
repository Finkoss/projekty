using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong4ITB
{
    public partial class Menu : Form
    {
        int ballSpeed = 8;
        string player1Name = "Hráč 1";
        string player2Name = "Hráč 2";

        public Menu() {
            InitializeComponent();
            this.Width = 504;
            
            trackBar1.Value = ballSpeed;
            label4.Text = ballSpeed.ToString();
            textBox1.Text = player1Name;
            textBox2.Text = player2Name;
        }

        private void button1_Click(object sender, EventArgs e) {
            Form1 form = new Form1();
            form.SetupGame(ballSpeed, player1Name, player2Name);
            form.FormClosing += (send, evt) => {
                this.Show();
            };
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            MessageBox.Show("Hrajte pong, nenechte spadnout míček\nHráč 1: W,S\nHráč 2: O,L");
        }

        private void button3_Click(object sender, EventArgs e) {
            this.Width = 786; //504
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            ballSpeed = trackBar1.Value;
            label4.Text = ballSpeed.ToString();
        }

        private void button5_Click(object sender, EventArgs e) {
            player1Name = textBox1.Text;
            player2Name = textBox2.Text;
            this.Width = 504;
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = true;
        }
    }
}
