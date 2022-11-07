using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid_Dodger
{
    public class Asteroid : Asset
    {
        public int Radius { get; set; }

        public Asteroid(Point center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.WhiteSmoke, 2);

            rect = new Rectangle(Center.X, Center.Y, Radius, Radius);
            e.Graphics.DrawEllipse(pen, rect);
        }

        public bool Collision(Asset ship)
        {
            if (rect.IntersectsWith(ship.rect))
            {
                return true;
            }
            return false;
        }

    }
}
