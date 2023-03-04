using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class BlockQuestion : AnimatedGraphicObject
    {
        private bool _hit;
        private int _dirY;
        private float _offY;

        private bool _isOpen;
        public GraphicObject hiddenObject;

        private void MarioHitMe(object sender, Mario.MarioEventArgs e)
        {
            if (e.graphicObject is BlockQuestion && e.collision.Dest  == DEST)
            {
                if (e.collision.Dir is CollisionDirection.CD_Down && e.marioJumpState is Mario.MarioJumpState.Up)
                {

                    if (_hit == false && _isOpen == false)
                    {
                        _isOpen = true;
                        _hit = true;
                        _dirY = -1;
                        _offY = 0;

                        if (hiddenObject is Coin)
                        {
                            ((Coin)hiddenObject).MoveCoin();
                        }
                        if (hiddenObject is MushRed)
                        {
                            hiddenObject._isVisiable = true;
                            ((MushRed)hiddenObject).isLive = true;
                        }
                        if (hiddenObject is MushLife)
                        {
                            hiddenObject._isVisiable = true;
                            ((MushLife)hiddenObject).isLive = true;
                        }
                        if (hiddenObject is Flower)
                        {
                            hiddenObject._isVisiable = true;
                        }
                    }
                }
            }
        }

        protected override void OnAnimate(object sender, EventArgs e)
        {
            if(_isOpen)
            {
                _animated = false;
                _imageIndex = 4;
            }
            else
                base.OnAnimate(sender, e);
        }
        public void OnBlockHit(object sender, EventArgs e)
        {
            if (_hit)
            {
                if (_dirY == -1)
                {
                    _offY += 1;
                    newY += (int)(_dirY * _offY);

                    if (_offY == 2)
                    {
                        _dirY = 1;
                        _offY = 0;
                    }
                }
                else
                {
                    _offY += 1;
                    newY += (int)_offY;
                    if (_offY == 2)
                    {
                        _dirY -= 1;
                        _offY = 0;
                        _hit = false;
                    }
                }
            }
        }
        public void CreateBonus(ObjectType t)
        {
            switch(t)
            {
                case ObjectType.OT_Coin:
                    hiddenObject = new Coin(x, y, true); break;
                case ObjectType.OT_MushRed:
                    hiddenObject = new MushRed(x, y - 1);break;
                case ObjectType.OT_MushLife:
                    hiddenObject = new MushLife(x, y - 1);break;
                case ObjectType.OT_Flower:
                    hiddenObject= new Flower(x, y - 1);break;
            }
            AddObject(hiddenObject);
        }
        public override void LoadEvent()
        {
            base.LoadEvent();
            Mario.IntersectEvent += MarioHitMe;
        }
        public BlockQuestion (int x, int y, ObjectType t) : base (ObjectType.OT_BlockQuestion)
        {
            _animatedCount = 4;
            this.x = x;
            this.y = y;
            SetWidthHeight();

            _isOpen = false;
            _hit = false;
            CreateBonus(t);

            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, OnBlockHit);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);

            Mario.IntersectEvent += MarioHitMe;
        }
    }
}
