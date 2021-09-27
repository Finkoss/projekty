using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong4ITB
{
    public class Ball
    {
        private int size = 15; // poloměr
        private float x;
        private float y;

        private float angle = 177;
        private float speed = 8;
        public float Speed {
            get { return speed; }
            set { speed = value; }
        }

        private float maxBounceAngle = 75;

        public Ball(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void Reset() {
            x = Pong.Instance.Width / 2;
            y = Pong.Instance.Height / 2;
            SetInitAngle();
        }

        public void SetInitAngle() {
            Random r = new Random();
            var angle = r.Next(-30, 30);
            this.angle = r.Next(0, 2) < 1 ? angle + 180 : angle;
        }

        public void Update(){
            float x = (float) Math.Cos(ToRad(angle)) * speed;
            float y = (float) Math.Sin(ToRad(angle)) * speed;

            this.x += x;
            this.y += y;
        }

        private float ToRad(float deg) {
            return (float)((Math.PI/180) * deg);
        }

        public void Draw(Graphics g) {
            g.FillEllipse(Brushes.Red, x - size, y - size, 2 * size, 2 * size);
        }

        public void CheckCollisionsWithWalls(int width, int height) {
            if(y + size >= height || y - size <= 0) {
                angle = 360 - angle;
            }
        }

        public void CheckCollisionWithPaddle(Paddle paddle) {
            Point p = new Point((int) (x + paddle.BallCheck * size), (int) y);
            var bouncePerc = paddle.CheckCollisionWithPoint(p);
            if(bouncePerc.HasValue) {
                float perc = bouncePerc.Value;
                if (paddle.BallCheck == -1) { // levá pálka
                    angle = perc * maxBounceAngle;
                } else { // pravá pálka
                    angle = 180 - perc * maxBounceAngle;
                }
            }
        }

        public bool CheckWin(Paddle paddle) {
            if(paddle.BallCheck == -1) {
                if(x - size <= 0) {
                    return true;
                }
            } else {
                if (x + size >= Pong.Instance.Width) {
                    return true;
                }
            }
            return false;
        }
    }
}
