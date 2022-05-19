using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Enemy : Player
    {
        public Enemy(PointF pos, int type)
        {
            switch (type)
            {
                case 1:
                    imagePlayer = Properties.Resources.enemy1;
                    physics = new Physics(pos, new Size(40, 40));
                    break;
                case 2:
                    imagePlayer = Properties.Resources.enemy2;
                    physics = new Physics(pos, new Size(40, 40));
                    break;
                case 3:
                    imagePlayer = Properties.Resources.enemy3;
                    physics = new Physics(pos, new Size(40, 40));
                    break;

            }

        }
    }
}