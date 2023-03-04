using System;
using System.Drawing;
using System.IO;


namespace MGame
{
    [Serializable]
    public class GroundBrick : StaticGraphicObject
    {
        public override void Draw()
        {
            base.Draw();
        }
        public GroundBrick(int x, int y ): base (ObjectType.OT_GroundBrick)
        {
            this.x = x ;
            this.y = y ;
          //  OT = ObjectType.OT_GroundBrick;

            SetWidthHeight();
           // bitmap = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(OT.ToString()));
        }
    }
}
