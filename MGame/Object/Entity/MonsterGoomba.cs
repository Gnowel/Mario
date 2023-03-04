using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class MonsterGoomba : MoveableAnimatedObject
    {

        public delegate void GoombaHendler();
        public static event GoombaHendler HitEvent;

        public void OnHitEvent ()
        {
            if (HitEvent != null)
                HitEvent();
        }
        public void GoombaDieFromMario(object sender, Mario.MarioEventArgs e)
        {
            if (e.graphicObject is MonsterGoomba)
            {
                if (e.collision.Dir == CollisionDirection.CD_Up && e.graphicObject.DEST == DEST)
                {
                    _animated = false;
                    isLive = false;
                }
            }
        }
        public void GoombaDieFromFireBall(GraphicObject graphicObject)
        {
            if (graphicObject.DEST == DEST)
                GoombaDied();
        }
        private void GoombaDied ()
        {
            _animated = false;
            isLive = false;
        }

        public override void Intersection(Collision c, GraphicObject g)
        {
            base.Intersection(c, g);
            if(g is MoveableAnimatedObject)
            {
                if(g is MonsterGoomba)
                {
                    _dirX *= -1;
                    ((MonsterGoomba)g)._dirX *= -1;
                }
            }
            if (g is Mario)
            {
                if (c.Dir != CollisionDirection.CD_Down)
                {
                    OnHitEvent();
                }
            }
        }
        protected override void OnAnimate(object sender, EventArgs e)
        {
            if(_isVisiable)
            {
                if(isLive)
                {
                    base.OnAnimate(sender, e);
                }
                else
                {
                    if (_imageIndex != 2)
                        _imageIndex = 2; // рисунок смерти
                    else
                        _isVisiable = false;
                }
            }
        }
        protected override void onWalk(object sender, EventArgs e)
        {
                base.onWalk(sender, e);
          
        }

        public override void LoadEvent()
        {
            base.LoadEvent();
            FireBall.FireEvent += GoombaDieFromFireBall;
            Mario.IntersectEvent += GoombaDieFromMario;
        }
        public MonsterGoomba (int x, int y) : base (ObjectType.OT_Goomba)
        {
            _animatedCount = 2;
            this.x = x;
            this.y = y;

            SetWidthHeight();

            FireBall.FireEvent += GoombaDieFromFireBall;
            Mario.IntersectEvent += GoombaDieFromMario;
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onWalk);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);
        }
    }
}
