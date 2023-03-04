using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class StaticGraphicObject : GraphicObject
    {
        protected int _imageCount;
        protected int  _offSetIndex;
        protected int _imageIndex;
        
        
        public override void Draw()
        {
            if (_isVisiable)
            {
                Graphics graphics;
                graphics = Screen.Instance.allWindow.graphics;

                if (_bitmap != null)
                {
                    DEST.X = newX;
                    DEST.Y = newY;
                    DEST.Width = _width;
                    DEST.Height = _height;

                    SRC.X = (16 * (_imageIndex + _offSetIndex)); 
                    SRC.Y = 0;
                    SRC.Width = _bitmap.Width / _imageCount;
                    SRC.Height = _bitmap.Height;

                    graphics.DrawImage(_bitmap, DEST, SRC, GraphicsUnit.Pixel);
                }
            }
        }
        public virtual void  LoadEvent()
        {

        }
        public StaticGraphicObject (ObjectType Type)
        {
            _imageCount = 1;
            OT = Type;
            _bitmap = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(OT.ToString()));

            _offSetIndex = 0;
        }
    }
}
