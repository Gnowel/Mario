using System;

namespace MGame
{
    [Serializable]
    public class Coin : AnimatedGraphicObject
    {
        bool _isMoveUp;
        public float offY;

        public delegate void CoinHendler();
        public static event CoinHendler TakeEvent;

        public void OnTakeEvent()
        {
            if(TakeEvent != null)
                TakeEvent();
        }
        private void CoinTaken ()
        {
            _isMoveUp = false;
            _isVisiable = false;
            _animated = false;
            OnTakeEvent();
            
        }
        protected override void OnAnimate(object sender, EventArgs e)
        {
            base.OnAnimate(sender, e);
            if(_isMoveUp)
            {
                _isVisiable = true;
                _animated = true;

                offY += 0.5f;
                newY -= 6 + (int)offY;

                if(offY >=2)
                {
                    CoinTaken();
                }
            }
            //Debug.WriteLine($"{ImageIndex}");
        }
        private void MarioAteMe(object sender, Mario.MarioEventArgs e)
        {
            if (e.graphicObject is Coin && e.collision.Dest == DEST)
            {
                CoinTaken();
            }
        }

        public void MoveCoin()
        {
            _isMoveUp = true;
            offY = 0;
        }
        public override void LoadEvent()
        {
            base.LoadEvent();
            Mario.IntersectEvent += MarioAteMe;
        }
        public Coin (int x, int y, bool MovingCoin): base (ObjectType.OT_Coin)
        {
            if(MovingCoin)
            {
                _isVisiable = false;
                _animated = false;
            }
            this.x = x;
            this.y = y;

            _animatedCount = 4;
            SetWidthHeight();
            _isMoveUp = false;

            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);
            Mario.IntersectEvent += MarioAteMe;
        }
    }
}
