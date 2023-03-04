using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class BigCloud : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 96;
            _height = 64;
        }
        public BigCloud(int x, int y) : base(ObjectType.OT_BigCloud)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight();

        }
    }
}
