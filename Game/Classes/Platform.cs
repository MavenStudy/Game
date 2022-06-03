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

        public Platform(PointF pos, int type)
        {
            switch (type)
            {
                case 1:
                    imagePlatform = Properties.Resources.platform;
                    mod = new Modify(pos, new Size(60, 20));
                    stepByPlayer = false;
                    break;
                case 2:
                    imagePlatform = Properties.Resources.platform2;
                    mod = new Modify(pos, new Size(60, 20));
                    stepByPlayer = false;
                    break;
                case 3:
                    imagePlatform = Properties.Resources.platform3;
                    mod = new Modify(pos, new Size(45, 20));
                    stepByPlayer = false;
                    break;

            }
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(imagePlatform, 
                mod.position.X, mod.position.Y,
                mod.size.Width, mod.size.Height);
        }

    }
}
