using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong4ITB
{
    public class Paddle
    {
        private Color color;

        private int x;
        private int y;

        private int height = 100;
        private int width = 15;

        private Rectangle rect;

        public int BallCheck { get { return x < 200 ? -1 : 1; } }

        public Paddle(Color c, int x, int y) {
            color = c;
            this.x = x;
            this.y = y;
            rect = new Rectangle(x - width / 2, y - height / 2, width, height);
        }

        public void Move(int yMove) {

            if (y + yMove < 0 || y + yMove > Pong.Instance.Height - height)
                return;

            y += yMove;
            rect.Y = y;
        }

        public void Draw(Graphics g) {
            g.FillRectangle(new SolidBrush(color), rect);
        }

        public float? CheckCollisionWithPoint(Point p) {
            if(rect.Contains(p)) {
                return (p.Y - rect.Y - rect.Height / 2) / (float)(rect.Height / 2);
            }
            return null;
        }

    }
}
