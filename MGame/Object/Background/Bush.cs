using System;
using System.Drawing;

namespace MGame
{
    [Serializable]
    public class Bush : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 96;
            _height = 32;
        }
        public Bush(int x, int y) : base (ObjectType.OT_Bush)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight();


        }
    }
}
