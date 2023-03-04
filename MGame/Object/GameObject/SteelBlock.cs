using System;
namespace MGame
{
    [Serializable]
    public class SteelBlock : StaticGraphicObject
    {
 
        public SteelBlock(int x, int y) : base(ObjectType.OT_SteelBlock)
        {
            this.x = x;
            this.y = y;

            SetWidthHeight();

        }
    }
}
