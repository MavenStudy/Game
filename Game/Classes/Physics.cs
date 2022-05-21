using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Classes
{
    public class Physics
    {
        public Modify mod;
        float gravity;
        float a;

        public float dx;
        

        public Physics(PointF position, Size size)
        {
            mod = new Modify(position, size);
            gravity = 0;
            a = 0.4f;
            dx = 0;
        }

        public void CalculatePhysics()
        {
            if (dx != 0)
            {
                mod.position.X += dx;
            }
            if(mod.position.Y < 700)
            {
                mod.position.Y += gravity;
                gravity += a;
                Touch();
            }
        }

        public void Touch()
        {
            for(int i = 0; i < PlatformGenerate.platforms.Count; i++)
            {
                var platform = PlatformGenerate.platforms[i];
                if(mod.position.X + mod.size.Width/2 >= platform.mod.position.X && 
                    mod.position.X + mod.size.Width/2 <= platform.mod.position.X + platform.mod.size.Width)
                {
                    if(mod.position.Y + mod.size.Height >= platform.mod.position.Y &&
                        mod.position.Y + mod.size.Height <= platform.mod.position.Y + platform.mod.size.Height)
                    {
                        if (gravity > 0)
                        {
                            gravity = -10; //сила прыжка
                            if (!platform.stepByPlayer)
                            {
                                PlatformGenerate.GenerateRandomPlatform();
                                platform.stepByPlayer = true;
                            }
                        }
                    }
                }
            }
        }

        public bool CollideWithObjects(bool forMonsters)
        {
            if (forMonsters)
            {
                for (int i = 0; i < PlatformGenerate.enemies.Count; i++)
                {
                    var enemy = PlatformGenerate.enemies[i];
                    PointF delta = new PointF();
                    delta.X = (mod.position.X + mod.size.Width / 2) - (enemy.physics.mod.position.X + enemy.physics.mod.size.Width / 2);
                    delta.Y = (mod.position.Y + mod.size.Height / 2) - (enemy.physics.mod.position.Y + enemy.physics.mod.size.Height / 2);
                    if (Math.Abs(delta.X) <= mod.size.Width / 2 + enemy.physics.mod.size.Width / 2)
                    {
                        if (Math.Abs(delta.Y) <= mod.size.Height / 2 + enemy.physics.mod.size.Height / 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool Collide()
        {
            for (int i = 0; i < PlatformGenerate.shots.Count; i++)
            {
                var shot = PlatformGenerate.shots[i];
                PointF delta = new PointF();
                delta.X = (mod.position.X + mod.size.Width / 2) - (shot.physics.mod.position.X + shot.physics.mod.size.Width / 2);
                delta.Y = (mod.position.Y + mod.size.Height / 2) - (shot.physics.mod.position.Y + shot.physics.mod.size.Height / 2);
                if (Math.Abs(delta.X) <= mod.size.Width / 2 + shot.physics.mod.size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= mod.size.Height / 2 + shot.physics.mod.size.Height / 2)
                    {
                        PlatformGenerate.RemoveShot(i);
                        return true;
                    }

                }
            }
            return false;
        }

    }
}

