using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGame
{
    [Serializable]
    public class BrickPiece : AnimatedGraphicObject
    {
        public bool _start = false;
        private float _startVelocity;
        private float _startPosition;
        private int _dirX;
        private float _timeCount;

        private void BrickPieceFall(object sender, EventArgs e)
        {
            if(_start)
            {
                _timeCount += (460.0f / 1000.0f);

                newY = (int)CalcBrickPiecePosition();
                newX = newX + _dirX * 2;

                if(newY > 448)
                {
                    _start = false;
                    _isVisiable = false;
                }
            }
        }
        private float CalcBrickPiecePosition()
        {
            return _startPosition + _startVelocity * _timeCount + 4f * _timeCount * _timeCount;
        }

        public override void Draw()
        {
            if(_start)
                base.Draw();
        }
        public override void SetWidthHeight()
        {
            _width = 16;
            _height = 16;

            newX = x;
           // newY = y * height;
        }

        public override void LoadEvent()
        {
            base.LoadEvent();
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, BrickPieceFall);
        }

        public BrickPiece (int x, int y, float SV, int D) : base (ObjectType.OT_BrickPiece)
        {

            this.x = x;
            this.y = y;


            SetWidthHeight();
            _startVelocity = SV;
            _dirX = D;
            _startPosition = y;

            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, BrickPieceFall);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);
        }
    }
}
