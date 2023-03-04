using System;

namespace MGame
{
    [Serializable]
    public class MoveableAnimatedObject : AnimatedGraphicObject
    {
        public int _dirX;
        protected int _dirY;
        public bool isLive;
        protected int _walkStep;
        protected bool _isFall;

        public override void IntersectionNone()
        {
            base.IntersectionNone();
            if (_isFall == false)
                _isFall = true;
        }

        public override void Intersection(Collision c, GraphicObject g)
        {
            base.Intersection(c, g);
            if(g is StaticGraphicObject)
            {
                if (c.Dir == CollisionDirection.CD_Down)
                    _isFall = true;
                if (c.Dir == CollisionDirection.CD_Up)
                    _isFall = false;
                if(c.Dir == CollisionDirection.CD_Left || c.Dir == CollisionDirection.CD_Right)
                {
                    _isFall = false;
                    _dirX *= -1;
                }
            }
        }

        protected virtual void onWalk (object sender, EventArgs e)
        {
            newX += _dirX * _walkStep;

            if (_isFall)
                newY += 2;
        }

        public override void LoadEvent()
        {
            base.LoadEvent();
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onWalk);
        }
        public MoveableAnimatedObject (ObjectType OT) : base (OT)
        {
            _dirX = 1;
            _dirY = 0;
            isLive = true;
            _walkStep = 1;
            _isFall = false;


        }
    }
}
