using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Shooting
    {
        public Physics physics;
        public Image imageShot;

        public Shooting(PointF pos)
        {
            imageShot = Properties.Resources.shot;
            physics = new Physics(pos, new Size(15, 15));
        }

        public void MoveUp()
        {
            physics.mod.position.Y -= 15;
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(imageShot, physics.mod.position.X, physics.mod.position.Y, physics.mod.size.Width, physics.mod.size.Height);
        }
    }
}
