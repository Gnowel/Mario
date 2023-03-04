using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class MonsterKoopa : MoveableAnimatedObject
    {
        public enum KoopaState { KS_Walking, KS_Shield, KS_Returning, KS_ShieldMoving}
        public enum KoopaDir { KD_Right, KD_Left}

        private KoopaState State;
        private KoopaDir Dir;
        private int _returnTime;

        public delegate void MonsterKoopaHandler();
        public static MonsterKoopaHandler HitEvent;

        public override void IntersectionNone()
        {
            base.IntersectionNone();
        }
        public void onHitEvent()
        {
            if(HitEvent != null)
                HitEvent();
        }
        protected override void onWalk(object sender, EventArgs e)
        {
            if(State == KoopaState.KS_Shield )
                base.onWalk(sender, e);

            if( State == KoopaState.KS_ShieldMoving || State == KoopaState.KS_Walking)
            {
                if( State == KoopaState.KS_ShieldMoving )
                {
                    if(isLive)
                    {
                        _animated = false;
                        isLive = false;
                        _isVisiable = false;
                    }
                }

                base.onWalk(sender, e);

                if (_dirX > 0)
                    Dir = KoopaDir.KD_Right;
                else
                    Dir = KoopaDir.KD_Left;

                if(State != KoopaState.KS_ShieldMoving)
                {
                    if (Dir == KoopaDir.KD_Right)
                        _offSetIndex = 2;
                    else
                        _offSetIndex = 0;
                }
            }
        }

        public override void Intersection(Collision c, GraphicObject g)
        {
            base.Intersection(c, g);

            if(g is MonsterGoomba)
            {
                if(State == KoopaState.KS_Walking)
                {
                    ((MonsterGoomba)g)._dirX *=-1;
                    _dirX *= -1;
                }
                if(State == KoopaState.KS_Shield)
                {
                    ((MonsterGoomba)g)._dirX *= -1;
                }
            }

            if(g is Mario)
            {
                if (State == KoopaState.KS_Shield && _returnTime >= 3)
                {
                    if (c.Dir == CollisionDirection.CD_Left)
                        _dirX = -1;

                    if (c.Dir == CollisionDirection.CD_Right)
                        _dirX = 1;

                    SetKoopaState(KoopaState.KS_ShieldMoving);
                }
                //Размер Марио уменьшается при столкновении с купой, но не в состоянии щита Или когда он только что пришел в движение
                if (State != KoopaState.KS_Shield) 
                {
                    if (!(State == KoopaState.KS_ShieldMoving 
                        && (_dirX == -1 && c.Dir == CollisionDirection.CD_Left) || (_dirX == 1 && c.Dir == CollisionDirection.CD_Right)))
                    {
                        if (c.Dir != CollisionDirection.CD_Down)
                        {
                            onHitEvent();
                        }
                    }
                }
            }
           
        }

        private void KoopaDieFromFireBall(GraphicObject graphicObject)
        {
            if(graphicObject.DEST == DEST )
            {
                _animated = false;
                isLive = false;
                _isVisiable = false;
            }
        }

        private void CooperationWithMario(object sender, Mario.MarioEventArgs e)
        {
            if(e.collision.Dir == CollisionDirection.CD_Up && e.graphicObject.DEST == DEST)
            {
                if(State == KoopaState.KS_Walking)
                {
                    SetKoopaState(KoopaState.KS_Shield);
                }
                else
                {
                    if(State == KoopaState.KS_Shield && _returnTime >=3)
                    {
                        SetKoopaState(KoopaState.KS_ShieldMoving);
                    }

                }
            }
        }

        protected override void OnAnimate(object sender, EventArgs e)
        {
            base.OnAnimate(sender, e);
            if (State == KoopaState.KS_Shield)
            {
                _returnTime++;
                if (_returnTime > 20)
                    SetKoopaState(KoopaState.KS_Returning);
            }
            if(State == KoopaState.KS_Returning)
            {
                _returnTime++;
                _imageIndex = (_returnTime % 2) * 4 + 4; // 4 ИЛИ 9

                if (_returnTime > 40)
                {
                    SetKoopaState(KoopaState.KS_Walking);
                    _returnTime = 0;
                }
            }
        }

        private void SetKoopaState(KoopaState S)
        { 
            State = S;
            switch (State)
            {
                case KoopaState.KS_Walking:
                    _width = 32;
                    _height = 54;
                    _animatedCount = 2;
                    newY -= 22;
                    _walkStep = 1;
                    _animated = true;
                    break;
                case KoopaState.KS_Shield:
                    _width = 32;
                    _height = 54;
                    _returnTime = 0;
                    _offSetIndex = 0;
                    _imageIndex = 4;
                    _animated = false;
                    break;
                case KoopaState.KS_Returning:
                    _offSetIndex = 0;
                    break;
                case KoopaState.KS_ShieldMoving:
                    _width = 32;
                    _height = 54;
                    _walkStep = 4;
                    _animatedCount = 4;
                    _offSetIndex = 4;
                    _animated = true;
                    break;
            }
        }
        public override void LoadEvent()
        {
            base.LoadEvent();
            FireBall.FireEvent += KoopaDieFromFireBall;
            Mario.IntersectEvent += CooperationWithMario;
        }
        public MonsterKoopa (int x , int y) : base (ObjectType.OT_Koopa)
        {
            this.x = x;
            this.y = y;
            _imageCount = 10;

            SetWidthHeight();
            SetKoopaState(KoopaState.KS_Walking);
            Dir = KoopaDir.KD_Right;

            FireBall.FireEvent += KoopaDieFromFireBall;
            Mario.IntersectEvent += CooperationWithMario;
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onWalk);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);
        }
    }
}
