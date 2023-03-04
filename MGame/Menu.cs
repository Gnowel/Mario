using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace MGame
{
    public partial class Menu : Form
    {
        Bitmap _bitmap;
        Graphics _graphics;
        Rectangle _src, _dest;
        Rectangle _rectInfo;
        Rectangle _rectStr;
        Game _game;
        Map _map;
        string _stringHelp, _stringAbout;
        bool isInformationVisible, isHelp, isAbout;
        int tabIndex;
        public Menu()
        {
            InitializeComponent();

            _rectStr = new Rectangle(0, 150, this.Width, this.Height);
            _bitmap = new Bitmap(Properties.Resources.pointer);
            _graphics = this.CreateGraphics();
            _dest = new Rectangle(Play.Location.X - 20, Play.Location.Y + 7, 14, 14);
            _src = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
            isInformationVisible = false;


         

            _stringAbout = "Mario Bros. (64 - разрядная версия) \n"
                         + "Версия 1.0 \n"
                         + "Язык Русский \n"
                         + "© Alex, 2022. \n"
                         + "Все права защищены.";

;           _stringHelp  = "\nУправление марио осуществляется с помощью стрелок на клавиатуре ←↑→↓.\n "
                         + "\nЕсть 3 разных типа марио: обычный (маленький), большой и огненный.\n"
                         + "Большой и огненный тип добавляет новые способности:\n"
                         + " • большой марио может разбивать золотые кирпичные блоки;\n"
                         + " • огненный - выпускать огненный снаряд на клавишу SPACE.\n"
                         + "\nЧтобы улучшить марио, нужно разбить блок знак вопрос.\n"
                         + "В этих блоках хранятся разные улучшения для марио:\n"
                         + " • золотая монета - добавляет 1 золотую монету в счет;\n"
                         + " • зеленный гриб - добавляет 1 хп;\n"
                         + " • красный гриб - делает марио большим;\n"
                         + " • огненный цветок - делает марио огенным.\n"
                         + "Золотую монету также можно найти на карте (не только в блоке вопрос).\n"
                         + "\nИзначально у марио 3 жизни:\n"
                         + "• если марио обычный и его задел монстр, то -1 хп;\n"
                         + "• если марио большой или огненный, то у марио забирается 1 улучшение (огненный -> большой -> маленький);\n"
                         + "• если у марио 0 жизней, игра заканчивается.\n"
                         + "\nЕсть два монстра:\n"
                         + "• гумба. Умирает, если прыгнуть ему на голову;\n"
                         + "• купа. Умирает, если прыгнуть ему на голову 2 раза.\n"
                         + "\nНа карте вы можете найти дверь, если вы подойдете к ней и нажмете ENTER, то вы перейдете на другой уровень\n"
                         + "\nЧтобы сделать сохранение в игре, нажмите F5.\n"

                         ;
        }



        private void timer1_Tick(object sender, EventArgs e) {

            Invalidate();
            WhoIsFocused();
        }

        private void Menu_Paint(object sender, PaintEventArgs e) 
        {
            _graphics = e.Graphics;
            if (!isInformationVisible)
                _graphics.DrawImage(_bitmap, _dest, _src, GraphicsUnit.Pixel);

            if (isAbout)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                _graphics.DrawString(_stringAbout, Play.Font, new SolidBrush(Color.White), _rectInfo = new Rectangle(this.Width / 4, this.Height / 4, this.Width / 2, this.Height / 2));
                _graphics.DrawString("Нажмите ESC, чтобы выйти.", new Font("Microsoft YaHei UI", 10, FontStyle.Bold), new SolidBrush(Color.White), _rectStr, format);
            }

            if (isHelp)
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                _graphics.DrawString(_stringHelp, Font, new SolidBrush(Color.White), _rectInfo = new Rectangle(0, 0, this.Width, this.Height));
                _graphics.DrawString("Нажмите ESC, чтобы выйти.", new Font("Microsoft YaHei UI", 10, FontStyle.Bold), new SolidBrush(Color.White), _rectStr, format);
            }
            }

            private void Exit_Click(object sender, EventArgs e) => this.Close();

        private void Play_Click(object sender, EventArgs e)
        {
            _game = new Game();
            _game.Show();
            this.Hide();
             //this.Close();
        }

        private void Help_Click(object sender, EventArgs e)
        {
            foreach (MyButton mbt in Controls.OfType<MyButton>())
            {
                mbt.Enabled = false;
                mbt.Visible = false;
            }
            pictureBox1.Visible = false;

            isInformationVisible = true;
            isHelp = true;

        }

        private void loadSave_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetFullPath(@"..\..\GameSaves");

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var binFormatter = new BinaryFormatter();
            string filename = openFileDialog.FileName;
            FileStream FS = File.OpenRead(filename);

            _map = (Map)binFormatter.Deserialize(FS);
            FS.Close();

            _game = new Game(_map);
            _game.Show();
            this.Hide();

        }

        private void About_Click(object sender, EventArgs e)
        {
            foreach (MyButton mbt in Controls.OfType<MyButton>())
            {
                mbt.Enabled = false;
                mbt.Visible = false;
            }
            pictureBox1.Visible = false;
            isInformationVisible = true;
            isAbout = true;
        }

        private void Menu_KeyUp(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Escape && isInformationVisible)
            {
                foreach (MyButton mbt in Controls.OfType<MyButton>())
                {
                    mbt.Enabled = true;
                    mbt.Visible = true;
                    mbt.Focus();  

                }
                pictureBox1.Visible = true;
                _dest.Y = Play.Location.Y + 7;
                isInformationVisible = false;
                isAbout = false;
                isHelp = false;
            }
            //Debug.WriteLine($"{ _dest.Y}");
        }

        private void  WhoIsFocused()
        {
            foreach (MyButton mbt in Controls.OfType<MyButton>())
            {
                if (mbt.Focused)
                {
                    _dest.Y = mbt.Location.Y + 7;
                }

            }
        }
    }
}
