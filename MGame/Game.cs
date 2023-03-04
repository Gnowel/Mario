using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using static MGame.Mario;

namespace MGame
{
    public partial class Game : Form
    {
        private Map _map;
        private Graphics _graphics;
        private bool _isGameOver;
        private bool _isGameCompleted;
        private const byte _fileCount = 2;
        private byte _nextLevel;
        
       public Game(Map map)
        {
            TimerGenerator.RemoveAllTimerEvent();
            InitializeComponent();

            this._map = map;
            timer1.Start();
            _map.LoadEvent();
            Mario.MarioDiedEvent += EndGame;
            Mario.LevelCompletedEvent += LoadNewLvl;


        }
        public Game()
        {
            TimerGenerator.RemoveAllTimerEvent();
            InitializeComponent();

            _map = new Map();
            timer1.Start();
            Mario.MarioDiedEvent += EndGame;
            Mario.LevelCompletedEvent += LoadNewLvl;
        }


        private void OnPaint(object sender, PaintEventArgs e)
        {
            _graphics = e.Graphics;


            Screen.Instance.DrawOnGraphics(_graphics);
            _map.DrawMap();
     
            if(_isGameOver)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Rectangle rectGameOver = new Rectangle(0, 0, this.Width, this.Height);
                Rectangle rectStr = new Rectangle(0, 150, this.Width, this.Height);
                _graphics.DrawRectangle(new Pen(BackColor), 0, 0, this.Width, this.Height);
                _graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, this.Width, this.Height);
                _graphics.DrawString("ВЫ ПРОИГРАЛИ", new Font("Microsoft YaHei UI", 20, FontStyle.Bold), new SolidBrush(Color.White), rectGameOver, format);
                _graphics.DrawString("Нажмите ESC, чтобы выйти.", new Font("Microsoft YaHei UI", 10, FontStyle.Bold), new SolidBrush(Color.White), rectStr, format);
            }
            if(_isGameCompleted)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Rectangle rectGameOver = new Rectangle(0, 0, this.Width, this.Height);
                Rectangle _rectStr = new Rectangle(0, 150, this.Width, this.Height);
                _graphics.DrawRectangle(new Pen(BackColor), 0, 0, this.Width, this.Height);
                _graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, this.Width, this.Height);
                _graphics.DrawString("ВЫ ПРОШЛИ ИГРУ", new Font("Microsoft YaHei UI", 20, FontStyle.Bold), new SolidBrush(Color.White), rectGameOver, format);
                _graphics.DrawString("Нажмите ESC, чтобы выйти.", new Font("Microsoft YaHei UI", 10, FontStyle.Bold), new SolidBrush(Color.White), _rectStr, format);
            
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _map.mario.StopMove(); break;
                case Keys.Left:
                    _map.mario.StopMove(); break;
                case Keys.Up:
                    _map.mario.isUpPressed = false; break;
            }

        }
        public void EndGame()
        {
            _isGameOver = true;
            TimerGenerator.RemoveAllTimerEvent(); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _map.mario.CharacterMove(Mario.MarioMoveState.Left); break;
                case Keys.Right:
                    _map.mario.CharacterMove(Mario.MarioMoveState.Right); break;
                case Keys.Up:
                    _map.mario.Jump(false,0); break;
                case Keys.Space:
                    _map.mario.MarioFireBall(); break;
                case Keys.Escape:
                    if (_isGameOver || _isGameCompleted)
                    {
                        this.Hide();
                        Menu menu = new Menu();
                        menu.Show();
                    }
                    break;
                case Keys.F5:
                    Save();
                    break;
                case Keys.Enter:
                    _map.mario.EnterPressed = true;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
            numCoin.Text = _map?.mario?.NumberOfCollectedCoins.ToString();
            numLife.Text = _map?.mario?.NumberOfCollectedLife.ToString();
        }

        private void LoadNewLvl()
        {
            TimerGenerator.RemoveAllTimerEvent();
            if(_fileCount == _map.LevelCount)
            {
                _isGameCompleted = true;
            }
            else
            {
                byte tempCoin, tempLife;
                MarioType tempType;
                tempType = _map.mario.Type;
                tempCoin = _map.mario.NumberOfCollectedCoins;
                tempLife = _map.mario.NumberOfCollectedLife;
                _nextLevel = (byte)(_map.LevelCount + 1);
                _map = new Map(_nextLevel);
                _map.mario.NumberOfCollectedCoins = tempCoin;
                _map.mario.NumberOfCollectedLife = tempLife;
                _map.mario.Type = tempType ;
                _map.mario.SetWidthHeight();
            }
        }

        private void Save()
        {
            string filename = Path.GetFullPath(@"..\..\GameSaves\") + "CheckPoint_*.bin";

            var binFormatter = new BinaryFormatter();
            FileStream FS = new FileStream(GetNextFileName(filename), FileMode.Create);
            binFormatter.Serialize(FS, _map);
            FS.Close();

        }
        private string GetNextFileName (string mask)
        {
            var counter = 0;
            while (File.Exists(mask.Replace("*", counter.ToString())))
                counter++;
            return mask.Replace("*", counter.ToString());
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
