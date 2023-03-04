using System.Drawing;


namespace MGame
{
    public class Screen
    {
        private static Screen instance = null;
        private Rectangle SRC, DEST;

        public SubsidiaryScreen Output;
        public SubsidiaryScreen allWindow;

        public static Screen Instance
        { 
            get 
            { 
                if (instance == null)
                    instance = new Screen();
                return instance;
            }
        }

        public void DrawOnGraphics(Graphics g)
        {
            
            DEST.X = 0 ;
            DEST.Y = 0;
            DEST.Width = Output.width;
            DEST.Height = Output.height;

            SRC.X = Output.x;
            SRC.Y = Output.y - allWindow.y;
            SRC.Width = Output.width;
            SRC.Height = Output.height;
            
            g.DrawImage(allWindow.bitmap, DEST,SRC,GraphicsUnit.Pixel);

            allWindow.graphics.Clear(Color.Transparent);
        }
        public void UpdateScreenX(object sender, Mario.MarioEventArgs e)
        {
            if (e.x >= Screen.Instance.Output.width / 2)
                Screen.Instance.Output.x = e.x - Screen.Instance.Output.width / 2;
            else
                Screen.Instance.Output.x = 0;

            if (e.x + Screen.Instance.Output.width / 2 >= allWindow.width)
                Screen.Instance.Output.x = allWindow.width - Screen.Instance.Output.width;

            //Debug.WriteLine($"Mario X: {mario.GetX}, Screen X: {Screen.Instance.Output.x}");
        }

        public void UpdateScreenY(object sender, Mario.MarioEventArgs e)
        {
            if (allWindow.height - e.y >= Screen.Instance.Output.height / 2)
            {
                Screen.Instance.Output.y = allWindow.height - e.y - Screen.Instance.Output.height / 2;
                //Debug.WriteLine("1");
            }
            else
                Screen.Instance.Output.y = 0;

            if (allWindow.height - e.y + Screen.Instance.Output.height / 2 >= allWindow.height)
            {
                Screen.Instance.Output.y = allWindow.height - Screen.Instance.Output.height;
                // Debug.WriteLine("2");

            }
        }
        public Screen()
        {
            Mario.MoveEvent += UpdateScreenY;
            Mario.MoveEvent += UpdateScreenX;

            allWindow = new SubsidiaryScreen(4972, 448); //448
            Output = new SubsidiaryScreen(800, 455); //600

            SRC = new Rectangle(0, 0, 0, 0);
            DEST = new Rectangle(0, 0, 0, 0);
        }
    }
}
