using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceCowboy
{
    public partial class Form2 : Form
    {
        public static List<Stars> stars;
        public static List<Enemy> enemies;
        public static List<Point> projectiles;
        public static System.Timers.Timer starsTimer;
        public static System.Timers.Timer travelTimer;
        public static System.Timers.Timer enemyTimer;
        public static Random rng;

        public static Enemy testEnemy;

        private static UInt16 SIDE_BUFFER = 20;
        private static UInt16 BOTTOM_BUFFER = 35;
        private static UInt16 PROJ_SIZE = 5;
        private static UInt16 PROJ_SPEED = 4;
        private static Pen starColor;
        private static Pen projColor;

        private static Image player;
        private static Point playerPosition;
        private static byte playerSpeed;

        private static byte enemySpeed;
        private static Color[] enemyColors;

        public class Stars
        {
            public Point coords;
            public byte lumes;
            public byte size;

            public Stars()
            {
                coords = new Point(0, 0);
                lumes = size = 0;
            }

            public Stars(Point alpha, byte beta, byte cappa)
            {
                coords = alpha;
                lumes = beta;
                size = cappa;
            }

            public void move()
            {
                coords = new Point(coords.X, coords.Y + 1);
            }
        }

        public class Enemy
        {
            public Point coords;
            public Color color;
            public byte size;

            public Enemy()
            {
                coords = new Point(0, 0);
                color = Color.White;
                size = 0;
            }

            public Enemy(Point alpha, Color beta, byte cappa)
            {
                coords = alpha;
                color = beta;
                size = cappa;
            }

            public void move()
            {
                coords = new Point(coords.X, coords.Y + enemySpeed);
            }
        }
        public Form2()
        {
            stars = new List<Stars>();
            enemies = new List<Enemy>();
            projectiles = new List<Point>();
            projColor = new Pen(Color.Orange);

            starsTimer = new System.Timers.Timer();
            starsTimer.Interval = 500; // 0.5 seconds
            starsTimer.AutoReset = true;
            starsTimer.Enabled = true;
            starsTimer.Elapsed += new System.Timers.ElapsedEventHandler(makeStar);

            enemyTimer = new System.Timers.Timer();
            enemyTimer.Interval = 1000; // 1 seconds
            enemyTimer.AutoReset = true;
            enemyTimer.Enabled = true;
            enemyTimer.Elapsed += new System.Timers.ElapsedEventHandler(makeEnemies);

            travelTimer = new System.Timers.Timer();
            travelTimer.Interval = 17; // Just under 60 frames per second (or FPS)
            travelTimer.AutoReset = true;
            travelTimer.Enabled = true;
            travelTimer.Elapsed += new System.Timers.ElapsedEventHandler(paintTheStars);

            rng = new Random();

            player = Image.FromFile("..\\..\\ship.jpg");

            this.FormClosed +=
               new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);

            InitializeComponent();
            playerPosition = new Point((playingField.Width / 2) - 32, (playingField.Height - 64 - BOTTOM_BUFFER));
            playerSpeed = 5;

            testEnemy = new Enemy(new Point((playingField.Width / 2), 0), Color.Red, 5);
            enemies.Add(testEnemy);

            enemySpeed = 2;
            enemyColors = new Color[3];
            enemyColors[0] = Color.Green;
            enemyColors[1] = Color.Purple;
            enemyColors[2] = Color.Yellow;
        }

        private void makeStar(object sender, System.Timers.ElapsedEventArgs args)
        {
            Stars newStar = new Stars(new Point(rng.Next(playingField.Width - SIDE_BUFFER) + SIDE_BUFFER, 0),
                                      (byte)(rng.Next(55) + 151), (byte)(rng.Next(3) + 1));
            stars.Add(newStar);
        }

        private void makeEnemies(object sender, System.Timers.ElapsedEventArgs args)
        {
            Enemy newEnemy = new Enemy(new Point(rng.Next(playingField.Width - SIDE_BUFFER) + SIDE_BUFFER, 0),
                                       enemyColors[rng.Next(3)], (byte)(rng.Next(5) + 5));
            enemies.Add(newEnemy);
        }

        private void movePlayer(int displacement)
        {
            playerPosition = new Point(playerPosition.X + displacement, playerPosition.Y);
        }

        private void launchProjectile()
        {
            projectiles.Add(new Point(playerPosition.X + 32, playerPosition.Y));
        }

        private void paintTheStars(object sender, System.Timers.ElapsedEventArgs args)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].move();
                if (stars[i].coords.Y >= playingField.Height)
                {
                    stars.RemoveAt(i--);
                    continue;
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].move();
                if (enemies[i].coords.Y >= (playingField.Height - enemies[i].size))
                {
                    enemies.RemoveAt(i--);
                    continue;
                }
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i] = new Point(projectiles[i].X, projectiles[i].Y - PROJ_SPEED);

                if (projectiles[i].Y <= PROJ_SPEED)
                {
                    projectiles.RemoveAt(i--);
                    continue;
                }

                if (projectiles[i].X == testEnemy.coords.X)
                {
                    if ((projectiles[i].Y - PROJ_SIZE) <= (enemies[0].coords.Y + enemies[0].size))
                    {
                        projectiles.RemoveAt(i--);
                        enemies.RemoveAt(0);
                    }
                }
            }

            playingField.Refresh();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void playingField_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //g.DrawImage(player, new Point((playingField.Width / 2) - 32, (playingField.Height - 64 - BOTTOM_BUFFER)));
            if (playerPosition != null)
                g.DrawImage(player, playerPosition);

            for (int i = 0; i < stars.Count; i++)
            {
                starColor = new Pen(Color.FromArgb(stars[i].lumes, Color.White));

                // Drawing the tiniest of stars
                if (stars[i].size == 1)
                {
                    g.DrawRectangle(starColor, stars[i].coords.X, stars[i].coords.Y, 1, 1);
                }

                // Drawing medium sized stars
                if (stars[i].size == 2)
                {
                    g.DrawRectangle(starColor, stars[i].coords.X - 1, stars[i].coords.Y, 3, 1);

                    if (stars[i].coords.Y + 1 < playingField.Height)
                    {
                        g.DrawRectangle(starColor, stars[i].coords.X, stars[i].coords.Y + 1, 1, 1);
                    }
                    if (stars[i].coords.Y - 1 >= 0)
                    {
                        g.DrawRectangle(starColor, stars[i].coords.X, stars[i].coords.Y - 1, 1, 1);
                    }
                }

                // Drawing large sized stars
                if (stars[i].size == 3)
                {
                    g.DrawRectangle(starColor, stars[i].coords.X - 2, stars[i].coords.Y, 5, 1);

                    if (stars[i].coords.Y + 1 < playingField.Height)
                    {
                        g.DrawRectangle(starColor, stars[i].coords.X - 1, stars[i].coords.Y + 1, 3, 1);
                    }
                    if (stars[i].coords.Y + 2 < playingField.Height)
                    {
                        g.DrawRectangle(starColor, stars[i].coords.X, stars[i].coords.Y + 2, 1, 1);
                    }

                    if (stars[i].coords.Y - 1 >= 0)
                    {
                        g.DrawRectangle(starColor, stars[i].coords.X - 1, stars[i].coords.Y - 1, 3, 1);
                    }

                    if (stars[i].coords.Y - 1 >= 0)
                    {
                        g.DrawRectangle(starColor, stars[i].coords.X, stars[i].coords.Y - 2, 1, 1);
                    }
                }
            }

            // Drawing the projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                g.FillCircle(projColor.Brush, projectiles[i].X, projectiles[i].Y, PROJ_SIZE);
            }

            Pen enemiesColor;
            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesColor = new Pen(enemies[i].color);
                g.FillRectangle(enemiesColor.Brush, enemies[i].coords.X + (enemies[i].size / 2),
                                enemies[i].coords.Y, enemies[i].size, enemies[i].size);
            }
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                launchProjectile();
            }

            if (e.KeyChar == 'a')
            {
                movePlayer(playerSpeed * -1);
            }
            if (e.KeyChar == 'd')
            {
                movePlayer(playerSpeed);
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                movePlayer(playerSpeed * -1);
            }
            if (e.KeyCode == Keys.Right)
            {
                movePlayer(playerSpeed);
            }
        }
    }

    public static class GraphicsExtensions
    {
        public static void DrawCircle(this Graphics g, Pen pen,
                                      float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }
    }
}
