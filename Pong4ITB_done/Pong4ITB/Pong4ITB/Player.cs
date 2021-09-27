using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong4ITB
{
    public class Player
    {
        public event Action<Player> ScoreChanged;

        public string name;
        private Paddle paddle;
        public Paddle Paddle {
            get { return paddle; }
            set { paddle = value; }
        }
        public int score;

        private Keys upControl;
        private Keys downControl;

        private int yMove = 0;
        private int speed = 7;

        public Player(Keys up, Keys down, string name) {
            this.upControl = up;
            this.downControl = down;
            this.name = name;
        }

        public void Update() {
            Paddle.Move(yMove);
        }

        public bool IsMyKey(Keys k) {
            return k == upControl || k == downControl;
        }

        public void SaveMove(Keys? k) {
            if(k.HasValue) {
                yMove = k.Value == upControl ? -speed : speed;
            } else {
                yMove = 0;
            }
        }

        public void AddScore() {
            score++;
            ScoreChanged?.Invoke(this);
        }
    }
}
