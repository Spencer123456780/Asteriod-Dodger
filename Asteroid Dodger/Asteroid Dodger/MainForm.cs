using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Dodger
{
    public partial class MainForm : Form
    {
        List<Asteroid> assests;
        SpaceShip ship;
        int collision = 0;
        int Score = 0;
        int counter = 0;


        public MainForm()
        {
            InitializeComponent();
        }
        private void InitializeObjects()
        {
            int[] movement = {-5, -4, -3, -3, -2, -2, -2, -1 ,-1, -1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 5};


            Random rand = new Random();
            assests = new List<Asteroid>();
            ship = new SpaceShip(new Point(550, 450));


            int totalAsteroids = 40;
            int asteroidCount = 0;
            while(asteroidCount < totalAsteroids)
            {
                int x = rand.Next(100, 1100);
                int y = rand.Next(100, 500);
                int radius = rand.Next(20, 70);
                Asteroid asteroid = new Asteroid(new Point(x, y), radius);
                asteroid.MoveX = movement[rand.Next(0, movement.Length - 1)];
                asteroid.MoveY = movement[rand.Next(0, movement.Length - 1)];

                assests.Add(asteroid);
                asteroidCount++;
            }

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            TimerAsteroid.Interval = 16;
            TimerAsteroid.Start();
            
            InitializeObjects();

            this.Size = new Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.DarkBlue;
            this.Paint += new PaintEventHandler(this.PaintObjects);

        }




        private void PaintObjects(Object sender, PaintEventArgs e)
        {
            Rectangle rec = new Rectangle(100, 100, 900, 600);
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, rec);

            Region clippingRegion = new Region(rec);
            e.Graphics.Clip = clippingRegion;

            ship.Draw(e);

            int assetIndex = assests.Count - 1;
            while(assetIndex > 0)
            {
                if (assests[assetIndex].Collision(ship))
                {
                    assests.RemoveAt(assetIndex);
                    collision++;
                }
                else
                {
                    assests[assetIndex].Draw(e);
                }
                assetIndex--;
            }
            
            e.Graphics.ResetClip();
            e.Graphics.DrawString("Score: " + Score.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.White, 100, 20);
            e.Graphics.DrawString("Collisions: " + collision.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.White, 700, 20);
            e.Graphics.DrawString("Made by: Spencer ", new Font("Arial", 30, FontStyle.Regular), Brushes.White, 300, 20);
        }

        private void TimerAsteroid_Tick(object sender, EventArgs e)
        {
            ship.Move(110, 940, 100, 600);//Max move length

            foreach(Asset a in assests)
            {
                a.Move(0, this.Size.Width, 0, this.Size.Height);
            }
            counter++;
            if(counter >= 60)
            {
                Score++;
                counter = 0;
            }
            this.Refresh();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            ship.MoveX = 0;
            ship.MoveY = 0;

        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                ship.MoveX = -5;
            }
            else if(e.KeyCode == Keys.Right)
            {
                ship.MoveX = 5;
            }
            else if (e.KeyCode == Keys.Up)
            {
                ship.MoveY = -5;
            }
            else if (e.KeyCode == Keys.Down)
            {
                ship.MoveY = 5;
            }
        }
    }
}
