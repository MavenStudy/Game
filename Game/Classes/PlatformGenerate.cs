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
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Shooting> shots = new List<Shooting>();
        public static int startPos = 400;
        
        public static void AddPlatform(PointF position)
        {
            Random r = new Random();
            var posType = r.Next(1, 4);
            switch (posType)
            {
                case 1:
                    var platform = new Platform(position, posType);
                    platforms.Add(platform);
                    break;
                case 2:
                    platform = new Platform(position, posType);
                    platforms.Add(platform);
                    break;
                case 3:
                    platform = new Platform(position, posType);
                    platforms.Add(platform);
                    break;
            }
        }

        public static void GenerateStart()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = r.Next(0, 270);
                int y = r.Next(40, 60);
                startPos -= y;
                PointF position = new PointF(x, startPos);
                var posType = r.Next(1, 4);
                switch (posType)
                {
                    case 1:
                        var platform = new Platform(position, posType);
                        platforms.Add(platform);
                        break;
                    case 2:
                        platform = new Platform(position, posType);
                        platforms.Add(platform);
                        break;
                    case 3:
                        platform = new Platform(position, posType);
                        platforms.Add(platform);
                        break;

                }
            }
        }
        public static void GenerateRandomPlatform()
        {

            Random r = new Random();
            int x = r.Next(0, 270);
            PointF position = new PointF(x, startPos);
            var posType = r.Next(1, 4);
            var platform = new Platform(position, posType);
            switch (posType)
            {
                case 1:
                    platform = new Platform(position, posType);
                    platforms.Add(platform);
                    break;
                case 2:
                    platform = new Platform(position, posType);
                    platforms.Add(platform);
                    break;
                case 3:
                    platform = new Platform(position, posType);
                    platforms.Add(platform);
                    break;

            }
            var enemy = r.Next(1, 5);
            if (enemy == 1)
                CreateEnemy(platform);
        }

        public static void CreateEnemy(Platform platform)
        {
            Random r = new Random();
            var enemyType = r.Next(1, 4);

            switch (enemyType)
            {
                case 1:
                    var enemy = new Enemy(new PointF(platform.mod.position.X + (platform.mod.position.X / 6) - 30, platform.mod.position.Y - 30), enemyType);
                    enemies.Add(enemy);
                    break;
                case 2:
                    enemy = new Enemy(new PointF(platform.mod.position.X + (platform.mod.position.X / 6) - 30, platform.mod.position.Y - 30), enemyType);
                    enemies.Add(enemy);
                    break;
                case 3:
                    enemy = new Enemy(new PointF(platform.mod.position.X + (platform.mod.position.X / 6) - 30, platform.mod.position.Y - 30), enemyType);
                    enemies.Add(enemy);
                    break;

            }
        }

        public static void CreateShot(PointF pos)
        {
            var shot = new Shooting(pos);
            shots.Add(shot);
        }

        public static void RemoveEnemy(int i)
        {
            enemies.RemoveAt(i);
        }

        public static void RemoveShot(int i)
        {
            shots.RemoveAt(i);
        }

    }
}

