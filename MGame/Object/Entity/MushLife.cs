using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class MushLife : MoveableAnimatedObject
    {
        public delegate void MushLifeHandler();
        public static MushLifeHandler TakeEvent;

        public void OnTakeEvent()
        {
            if(TakeEvent != null)
                TakeEvent();
        }
        protected override void onWalk(object sender, EventArgs e)
        {
            if(isLive)
                base.onWalk(sender, e);
        }

        private void MarioAteMe(object sender, Mario.MarioEventArgs e)
        {
            if (e.graphicObject is MushLife && e.graphicObject.DEST == DEST)
            {
                _isVisiable = false;
                _animated = false;
                isLive = false;
                OnTakeEvent();
            }
        }
        public override void LoadEvent()
        {
            base.LoadEvent();
            Mario.IntersectEvent += MarioAteMe;
        }
        public MushLife (int x, int y) : base (ObjectType.OT_MushLife)
        {
            _imageCount = 1;
            _imageIndex = 0;
            this.x = x;
            this.y = y;
            _walkStep = 2;
            SetWidthHeight();
            _isVisiable = false;
            isLive = false;

            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onWalk);

            Mario.IntersectEvent += MarioAteMe;
        }
    }
}
