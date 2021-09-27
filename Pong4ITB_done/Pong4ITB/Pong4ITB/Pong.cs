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
    public partial class Pong : UserControl
    {
        Ball ball;
        Player player1, player2;

        private static Pong instance;
        public static Pong Instance { 
            get {
                /*if (instance == null)
                    instance = new Pong();*/
                return instance; 
            } 
        }

        public Pong() {

            instance = this;

            InitializeComponent();
            ball = new Ball(Width/2, Height/2);
            ball.Reset();
            
        }

        public void SetupPlayers(Player player1, Player player2) {
            this.player1 = player1;
            this.player2 = player2;

            if (player1 != null)
                player1.Paddle = new Paddle(Color.Blue, 20, Height / 2);
            if (player2 != null)
                player2.Paddle = new Paddle(Color.Green, Width - 20, Height / 2);
        }

        public void SetupBall(int ballSpeed) {
            ball.Speed = ballSpeed;
        }

        private void gameTimer_Tick(object sender, EventArgs e) {
            if (player1 == null)
                return;

            ball.Update();
            player1.Update();
            player2.Update();
            CheckCollisions();
            Refresh();
        }

        private void CheckCollisions() {
            ball.CheckCollisionsWithWalls(this.Width, this.Height);
            ball.CheckCollisionWithPaddle(player1.Paddle);
            ball.CheckCollisionWithPaddle(player2.Paddle);
            
            if(ball.CheckWin(player1.Paddle)) {
                Goal(player1);
            }
            
            if(ball.CheckWin(player2.Paddle)) {
                Goal(player2);
            }
        }

        private void Goal(Player looser) {
            Player winner = looser == player1 ? player2 : player1;
            
            if (winner.score + 1 >= 3) {
                ball.Reset();
                ball.Speed = 0;
            } else {
                ball.Reset();
            }

            winner.AddScore();
            

            // zpracování události ScoreChanged, Menu
        }

        private void Pong_KeyDown(object sender, KeyEventArgs e) {
            var key = e.KeyCode;

            if (key == Keys.Up)
                Console.WriteLine("Nahoru!");
            if(player1.IsMyKey(key)) {
                player1.SaveMove(key);
            }

            if (player2.IsMyKey(key)) {
                player2.SaveMove(key);
            }
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e) {
            var key = e.KeyCode;
            if (player1.IsMyKey(key)) {
                player1.SaveMove(null);
            }

            if (player2.IsMyKey(key)) {
                player2.SaveMove(null);
            }
        }

        private void Pong_Paint(object sender, PaintEventArgs e) {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            ball.Draw(e.Graphics);


            if(player1 != null)
                player1.Paddle.Draw(e.Graphics);
            if(player2 != null)
                player2.Paddle.Draw(e.Graphics);
        }

    }
}
