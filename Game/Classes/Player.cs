using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Player

    {   public Image imagePlayer;
        public Physics physics;
        
        public Player()
        {
            imagePlayer = Properties.Resources.player;
            physics = new Physics(new PointF(100, 350), new Size(40, 40));
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(imagePlayer,
                physics.mod.position.X, physics.mod.position.Y,
                physics.mod.size.Width, physics.mod.size.Height);
        }
    }
}

