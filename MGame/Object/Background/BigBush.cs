using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class BigBush : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 128;
            _height = 32;

            //newX = x * width;
            //newY = y * height;
        }
        public BigBush(int x, int y) : base(ObjectType.OT_BigBush)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight();
            //width = 128;
           // height = 32;

        }
    }
}
