using System.Drawing;


namespace MGame
{
    public class SubsidiaryScreen
    {
        public int x , y;
        public int width, height;
        public Bitmap bitmap;
        public Graphics graphics;

        public SubsidiaryScreen(int width, int heighth)
        {
            this.width = width;
            this.height = heighth;
            x = y = 0;
            bitmap = new Bitmap(this.width, this.height);
            graphics = Graphics.FromImage(bitmap) ;
        }
    }
}
