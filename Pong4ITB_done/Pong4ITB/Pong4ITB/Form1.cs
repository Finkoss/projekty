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
    public partial class Form1 : Form
    {
        Player player1;
        Player player2;


        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        public void SetupGame(int ballSpeed, string player1Name, string player2Name) {
            player1 = new Player(Keys.W, Keys.S, player1Name);
            player2 = new Player(Keys.O, Keys.L, player2Name);
            player1.ScoreChanged += OnScoreChanged;
            player2.ScoreChanged += OnScoreChanged;
            pong1.SetupPlayers(player1, player2);
            pong1.SetupBall(ballSpeed);

            SetPlayerLabel(player1, label1);
            SetPlayerLabel(player2, label2);
        }

        private void OnScoreChanged(Player player) {
            if(player == player1) {
                SetPlayerLabel(player, label1);
            } else {
                SetPlayerLabel(player, label2);
            }

            if(player.score >= 3) {
                DialogResult res = MessageBox.Show("Konec hry! Vítězem je " + player.name + "\nChcete se vrátit do menu?", "Konec hry", MessageBoxButtons.YesNo);
                if(res == DialogResult.Yes) {
                    this.Close();
                } else {
                    Application.Exit();
                }
            }
        }

        private void SetPlayerLabel(Player player, Label label) {
            label.Text = $"{player.name}: {player.score}";
        }
    }
}
