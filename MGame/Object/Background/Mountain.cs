using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class Mountain : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 96;
            _height = 32;
        }
        public Mountain (int x, int y) : base (ObjectType.OT_Mountain)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight();
        }
    }
}
