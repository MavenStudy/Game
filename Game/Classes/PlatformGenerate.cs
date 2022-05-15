using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
     public static class PlatformGenerate
    {
        public static List<Platform> platforms;
        public static int startPos = 400;
        
        public static void AddPlatform(PointF position)
        {
            Platform platform = new Platform(position);
            platforms.Add(platform);
        }

        public static void GenerateStart()
        {
            Random r = new Random();
            for(int i = 0; i < 1000; i++)
            {
                int x = r.Next(0, 270);
                int y = r.Next(50, 60);
                startPos -= y;
                PointF position = new PointF(x, startPos);
                Platform platform = new Platform(position);
                platforms.Add(platform);
            }
        }

    }
}

