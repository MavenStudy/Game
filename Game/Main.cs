using Game.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Game
{
    public partial class Main : Form
    {
        Player player;
        Timer timer;
        List<Score> scoreData = new List<Score>();
        string scorePath = Path.Combine(Environment.CurrentDirectory, "scores.txt");
        public Main()
        {
            InitializeComponent();
            Init();
            timer = new Timer();
            timer.Interval = 30;
            PlatformGenerate.score = 0;
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

            scoreDisplay.Text = PlatformGenerate.score.ToString();

            if (player.physics.mod.position.Y >= PlatformGenerate.platforms[0].mod.position.Y + 200 || player.physics.CollideWithObjects(true))
            {
                GameStop();
                menuPanel.Show();
            }

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
            MenuConfig(this);

            ScoreReader();
            GetStringScoreData();
        }

        private void MenuConfig(Form formMain)
        {
            scoreDisplay.ForeColor = Color.White;
            scoreDisplay.BackColor = Color.DimGray;
            scoreDisplay.Font = new Font("Arial", 12, FontStyle.Bold);
            scoreDisplay.Hide();

            Size menuSize = new Size(formMain.Width, formMain.Height);
            menuPanel.Size = menuSize;
            menuPanel.Location = new Point(0, 0);
            menuPanel.BackgroundImage = Properties.Resources.back;
            menuPanel.BackgroundImageLayout = ImageLayout.Stretch;
            menuPanel.BringToFront();

            stateLabel.Text = "Меню";
            stateLabel.Font = new Font("Arial", 28, FontStyle.Bold);
            stateLabel.ForeColor = Color.White;
            stateLabel.Location = new Point((int)(menuPanel.Width * 0.45) - (int)(stateLabel.Width * 0.5), (int)(stateLabel.Height * 0.2));
            stateLabel.BackColor = Color.Transparent;

            startButton.Size = new Size((int)(menuPanel.Width * 0.5), 60);
            startButton.Location = new Point((int)(menuPanel.Width * 0.5) - (int)(startButton.Width * 0.5), (int)(menuPanel.Height * 0.2));
            startButton.Text = "Играть";
            startButton.Font = new Font("Arial", 12, FontStyle.Bold);
            startButton.ForeColor = Color.White;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.BackColor = Color.FromArgb(178, 115, 72, 171);

            name.Size = new Size((int)(menuPanel.Width * 0.5), 60);
            name.Font = new Font("Arial", 12, FontStyle.Bold); 
            name.Text = "Введите имя";
            name.ForeColor = Color.Black;
            name.Location = new Point((int)(menuPanel.Width * 0.5) - name.Width + 100, startButton.Location.Y + name.Height * 3);
            

            exitButton.Size = new Size((int)(menuPanel.Width * 0.5), 60);
            exitButton.Font = new Font("Arial", 12, FontStyle.Bold);
            exitButton.Location = new Point((int)(menuPanel.Width * 0.5) - name.Width + 100, name.Location.Y + exitButton.Height + 170);
            exitButton.Text = "Выход";
            exitButton.ForeColor = Color.White;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.BackColor = Color.FromArgb(178, 115, 72, 171);

            scoreList.Location = new Point((int)(menuPanel.Width * 0.4) - 60, startButton.Location.Y + name.Height * 5);
            scoreList.Size = new Size((int)(menuPanel.Width * 0.5), exitButton.Height * 2 + name.Height);
            scoreList.Font = new Font("Arial", 12);
        }
        private void ScoreReader()
        {
            if (File.Exists(scorePath))
            {
                using (var sr = new StreamReader(scorePath))
                {
                    string temp = default;

                    Regex firstItemRegex = new Regex(@"^\w+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex secondItemRegex = new Regex(@"\w+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    while ((temp = sr.ReadLine()) != null)
                    {
                        scoreData.Add(new Score(Convert.ToString(firstItemRegex.Match(temp)),
                            Convert.ToInt32(Convert.ToString(secondItemRegex.Match(temp)))));
                    }
                }
                scoreData = scoreData.OrderByDescending(s => s.ScoreValue).ToList();
            }
        }

        private void ScoreWriter()
        {
            using (StreamWriter outputFile = new StreamWriter(scorePath))
            {
                string temp = default;

                for (int i = 0; i < scoreData.Count; i++)
                {
                    temp += scoreData[i].Name + "-" + scoreData[i].ScoreValue + Environment.NewLine;
                }

                outputFile.Write("");
                outputFile.Write(temp);
            }
        }

        public void GetStringScoreData()
        {
            List<string> tempScoreList = new List<string>();
            foreach (Score line in scoreData)
            {
                tempScoreList.Add(line.Name + " - " + line.ScoreValue);
            }
            scoreList.DataSource = tempScoreList;
        }

        private void AddPlayerScoreToList()
        {
            bool has = false;

            if (scoreData.Count > 0)
            {
                for (int i = 0; i < scoreData.Count; i++)
                    if (scoreData[i].Name == name.Text)
                        has = true;

                if (has)
                {
                    for (int i = 0; i < scoreData.Count; i++)
                    {
                        if (scoreData[i].Name == name.Text)
                        {
                            if (scoreData[i].ScoreValue < PlatformGenerate.score)
                            {
                                scoreData[i].ScoreValue = PlatformGenerate.score;
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
                else
                    scoreData.Add(new Score(name.Text, PlatformGenerate.score));
            }
            else
                scoreData.Add(new Score(name.Text, PlatformGenerate.score));

            scoreData = scoreData.OrderByDescending(s => s.ScoreValue).ToList();
        }

        private bool CheckInputText(string data)
        {
            Regex itemRegex = new Regex(@"\W", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (itemRegex.IsMatch(data) || data.Length == 0 || data.Length > 16)
            {
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GameStart()
        {
            Init();
            timer = new Timer();
            timer.Interval = 30;
            PlatformGenerate.score = 0;
            timer.Tick += new EventHandler(Update);
            timer.Start();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.BackgroundImage = Properties.Resources.back;
            this.Paint += new PaintEventHandler(OnRepaint);
            scoreDisplay.Show();
        }

        private void GameStop()
        {
            timer.Stop();
            timer.Enabled = false;

            scoreDisplay.Hide();

            stateLabel.Text = "Cчет: " + PlatformGenerate.score;

            AddPlayerScoreToList();
            GetStringScoreData();
            ScoreWriter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckInputText(name.Text))
            {
                GameStart();
                menuPanel.Hide();
                this.Focus();
            }
            else
            {
                MessageBox.Show("Ваш ник содержит недопустимые символы", "Ошибка", MessageBoxButtons.OK);
                name.Text = "";
                name.Focus();
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void scoreDisplay_Click(object sender, EventArgs e)
        {

        }

        private void name_Enter(object sender, EventArgs e)
        { 
            
        }

        private void name_Leave(object sender, EventArgs e)
        {
         
        }
    }
}
