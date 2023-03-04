using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class BigMountain : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 162;
            _height = 64;
        }
        public BigMountain(int x, int y) : base(ObjectType.OT_BigMountain)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight();

        }
    }
}
