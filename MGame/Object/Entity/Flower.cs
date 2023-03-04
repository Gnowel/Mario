using System;


namespace MGame
{
    [Serializable]
    public class Flower : StaticGraphicObject
    {

        private void MarioAteMe (object sender, Mario.MarioEventArgs e)
        {
           if(e.graphicObject is Flower && e.graphicObject.DEST == DEST)
            {
                _isVisiable = false;
            }
        }

        public override void LoadEvent()
        {
            base.LoadEvent();
            Mario.IntersectEvent += MarioAteMe;
        }
        public Flower (int x, int y) : base (ObjectType.OT_Flower)
        {
            _isVisiable = false;
            this.x = x;
            this.y = y;
            SetWidthHeight();

            Mario.IntersectEvent += MarioAteMe;
 
        }
    }
}
