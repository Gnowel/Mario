using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class Brick : AnimatedGraphicObject
    {
        public BrickPiece TopRight;
        public BrickPiece TopLeft;
        public BrickPiece ButtomRight;
        public BrickPiece ButtomLeft;

        private void MarioHitMe(object sender, Mario.MarioEventArgs e)
        {
            if (e.graphicObject is Brick && e.collision.Dest == DEST && e.marioJumpState is Mario.MarioJumpState.Up)
            {
                if (e.marioType == Mario.MarioType.Fire || e.marioType == Mario.MarioType.Big)
                {
                    if (e.collision.Dir == CollisionDirection.CD_Down)
                    {
                        _animated = false;
                        _isVisiable = false;

                        TopRight._start = true;
                        TopLeft._start = true;
                        ButtomRight._start = true;
                        ButtomLeft._start = true;
                    }
                }
            }
        }
        public override void LoadEvent()
        {
            base.LoadEvent();
            Mario.IntersectEvent += MarioHitMe;
        }
        public Brick (int x, int y) : base(ObjectType.OT_Brick)
        {
            _animatedCount = 4;
            this.x = x;
            this.y = y; 
            SetWidthHeight();

            TopRight = new BrickPiece(newX, newY, -15, 1);
            TopLeft = new BrickPiece(newX, newY, -15, -1);
            ButtomRight = new BrickPiece(newX, newY, -7, 1);
            ButtomLeft = new BrickPiece(newX, newY, -7, -1);

            AddObject(TopRight);
            AddObject(TopLeft);
            AddObject(ButtomRight);
            AddObject(ButtomLeft);

            TimerGenerator.AddTimerEventHandler(TimerType.TT_100,OnAnimate);

            Mario.IntersectEvent += MarioHitMe;
        }
    }
}
