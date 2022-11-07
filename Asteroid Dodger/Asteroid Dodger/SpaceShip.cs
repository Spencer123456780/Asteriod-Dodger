using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace Asteroid_Dodger
{
    public class SpaceShip : Asset
    {
        public SpaceShip(Point center)
        {
            Center = center;
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen ship = new Pen(Color.Red);

            Point[] points = new Point[3];
            points[0] = new Point(Center.X + 50, Center.Y + 75);//Right
            points[1] = new Point(Center.X + 0, Center.Y + 75);//Left
            points[2] = new Point(Center.X + 25, Center.Y + 50);//Tip
            //Points (500, 500) (450, 500) (475, 450)
            e.Graphics.DrawPolygon(ship, points);
            rect = new Rectangle(Center.X + 25, Center.Y + 25, 50, 50);


            Point[] points2 = new Point[3];
            points2[0] = new Point(Center.X + 40, Center.Y + +70);
            points2[1] = new Point(Center.X + 10, Center.Y + +70);
            points2[2] = new Point(Center.X + 25, Center.Y + 55);
            e.Graphics.DrawPolygon(ship, points2);

        }

        public override void Move(int X1, int X2, int Y1, int Y2)
        {
            int newX = Center.X + MoveX;
            if(newX < X1)
            {
                newX = X1;
            }
            else if(newX > X2)
            {
                newX = X2;
            }
            
            int newY = Center.Y + MoveY;
            if(newY < Y1)
            {
                newY = Y1;
            }
            else if(newY > Y2)
            {
                newY = Y2;
            }

            Center = new Point(newX, newY);
        }

    }
}
