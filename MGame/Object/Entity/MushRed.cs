using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class MushRed : MoveableAnimatedObject
    {

        private void MarioAteMe(object sender, Mario.MarioEventArgs e)
        {
            if (e.graphicObject is MushRed && e.graphicObject.DEST == DEST)
            {
                _isVisiable = false;
                _animated = false;
                isLive = false;
            }
        }
        protected override void onWalk(object sender, EventArgs e)
        {
            if(isLive)
                base.onWalk(sender, e);
        }
        public override void LoadEvent()
        {
            base.LoadEvent();
            Mario.IntersectEvent += MarioAteMe;
        }
        public MushRed(int x, int y) : base (ObjectType.OT_MushRed)
        {
            _imageCount = 2;
            _imageIndex = 0;
            this.x = x;
            this.y = y;
            _walkStep = 2;
            SetWidthHeight();
            isLive = false;
            _isVisiable = false;

            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onWalk);

            Mario.IntersectEvent += MarioAteMe;
        }
    }
}
