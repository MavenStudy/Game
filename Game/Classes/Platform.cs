using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Platform
    {
        Image imagePlatform;
        public Modify mod;
       
        public bool stepByPlayer;

        public Platform(PointF pos)
        {
            imagePlatform = Properties.Resources.platform;
            mod = new Modify(pos, new Size(60, 35));
            stepByPlayer = false;
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(imagePlatform, 
                mod.position.X, mod.position.Y,
                mod.size.Width, mod.size.Height);
        }

    }
}
