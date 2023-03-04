using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable
        ]
    public  class Exit : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _height = 64;
        }
        public Exit (int x, int y) : base (ObjectType.OT_Exit)
        {
            this.x = x;
            this.y = y;
            SetWidthHeight();
            
        }
    }
}
