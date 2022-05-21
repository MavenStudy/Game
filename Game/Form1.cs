using Game.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
namespace Game
{
    public partial class Form1 : Form
    {
        Player player;
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            Init();
            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += new EventHandler(Update);
            timer.Start();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.BackgroundImage = Properties.Resources.back;
            this.Paint += new PaintEventHandler(OnRepaint);
        }

        public void Init()
        {
            PlatformGenerate.platforms = new List<Platform>();
            PlatformGenerate.AddPlatform(new PointF(100, 400));
            PlatformGenerate.startPos = 400;
            PlatformGenerate.GenerateStart();
            PlatformGenerate.shots.Clear();
            PlatformGenerate.enemies.Clear();
            player = new Player();
        }

        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            player.imagePlayer = Properties.Resources.player;
            player.physics.dx = 0;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    player.imagePlayer = Properties.Resources.rightJump;
                    player.physics.dx = 6;
                    break;
                case "Left":
                    player.imagePlayer = Properties.Resources.leftJump;
                    player.physics.dx = -6;
                    break;
                case "Space":
                    player.imagePlayer = Properties.Resources.shooting;
                    PlatformGenerate.CreateShot(new PointF(player.physics.mod.position.X + player.physics.mod.size.Width / 2, player.physics.mod.position.Y));
                    break;
            }
        }


        private void Update(object sender, EventArgs e)
        {
            if (player.physics.mod.position.Y >= PlatformGenerate.platforms[0].mod.position.Y + 200 || player.physics.CollideWithObjects(true))
                Init();

            player.physics.CollideWithObjects(false);

            if (PlatformGenerate.shots.Count > 0)
            {
                for (int i = 0; i < PlatformGenerate.shots.Count; i++)
                {
                    if (Math.Abs(PlatformGenerate.shots[i].physics.mod.position.Y - player.physics.mod.position.Y) > 500)
                    {
                        PlatformGenerate.RemoveShot(i);
                        continue;
                    }
                    PlatformGenerate.shots[i].MoveUp();
                }
            }
            if (PlatformGenerate.enemies.Count > 0)
            {
                for (int i = 0; i < PlatformGenerate.enemies.Count; i++)
                {
                    if (PlatformGenerate.enemies[i].physics.Collide())
                    {
                        PlatformGenerate.RemoveEnemy(i);
                        break;
                    }
                }
            }

            player.physics.CalculatePhysics();
            FollowMode();
            Invalidate();
        }

        private void OnRepaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (PlatformGenerate.platforms.Count > 0)
            {
                for (int i = 0; i < PlatformGenerate.platforms.Count; i++)
                    PlatformGenerate.platforms[i].DrawSprite(g);
            }
            if (PlatformGenerate.enemies.Count > 0)
            {
                for (int i = 0; i < PlatformGenerate.enemies.Count; i++)
                    PlatformGenerate.enemies[i].DrawSprite(g);
            }
            if (PlatformGenerate.shots.Count > 0)
            {
                for (int i = 0; i < PlatformGenerate.shots.Count; i++)
                    PlatformGenerate.shots[i].DrawSprite(g);
            }
            player.DrawSprite(g);
        }
        public void FollowMode()
        {
            int offset = 400 - (int)player.physics.mod.position.Y;
            player.physics.mod.position.Y += offset;
            for (int i = 0; i < PlatformGenerate.platforms.Count; i++)
            {
                var platform = PlatformGenerate.platforms[i];
                platform.mod.position.Y += offset;
            }
            for (int i = 0; i < PlatformGenerate.enemies.Count; i++)
            {
                var enemy = PlatformGenerate.enemies[i];
                enemy.physics.mod.position.Y += offset;
            }
            for (int i = 0; i < PlatformGenerate.shots.Count; i++)
            {
                var shot = PlatformGenerate.shots[i];
                shot.physics.mod.position.Y += offset;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
