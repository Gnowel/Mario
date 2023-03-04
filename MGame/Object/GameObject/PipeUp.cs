using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class PipeUp : StaticGraphicObject
    {
        public override void SetWidthHeight()
        {
            base.SetWidthHeight();
            _width = 64;
            _height = 64;
        }
        public PipeUp (int x, int y) : base(ObjectType.OT_PipeUp)
        {
            this.x = x;
            this.y = y; 
            SetWidthHeight();
        }
    }
}
