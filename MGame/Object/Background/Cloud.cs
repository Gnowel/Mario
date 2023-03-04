using System;
using System.Drawing;

namespace MGame
{
    [Serializable]
    public class Cloud : StaticGraphicObject
    {

        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 96;
            _height = 64;
        }
        public Cloud (int x, int y) : base (ObjectType.OT_Cloud)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight ();
        }
    }
}
