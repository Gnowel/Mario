using System;
using System.Drawing;
using System.IO;

namespace MGame
{
    [Serializable]
    public class FireBall : AnimatedGraphicObject
    {
        public delegate void FireBallHendler(GraphicObject graphicObject);
        public static event FireBallHendler FireEvent;
        public enum FireBallDir {Right, Left };

        public FireBallDir Directory;
        private int _dirX;

        private float _startVelocity;
        private float _startPosition;
        private float _timeCount;

        public bool Started = false;
        private bool _fire;

        public void OnFireEvent(GraphicObject graphicObject)
        {
            if (FireEvent != null)
                FireEvent(graphicObject);
        }

        public override void SetWidthHeight()
        {
            //this.x = x;
            //this.y = y;
            base.SetWidthHeight();
            _width = 16;
            _height = 18;
  
        }

        public override void Draw()
        {
            base.Draw();
        }
        protected override void OnAnimate(object sender, EventArgs e)
        {
            base.OnAnimate(sender, e);
        }

        public override void Intersection(Collision c, GraphicObject g)
        {
            base.Intersection(c, g);

            if (g is Brick || g is PipeUp || g is GroundBrick || g is SteelBlock || g is BlockQuestion)
            {
                StartFireBall();
            }

            if(g is MonsterGoomba || g is MonsterKoopa)
            {
                FireEvent(g);
                Started = false;
                _isVisiable = false;
            }
        }

        public void RunFireBall (int x, int y, FireBallDir D)
        {
            Directory = D;
            if (D == FireBallDir.Right)
                _dirX = 1;
            else
                _dirX = -1;

            SetWidthHeight();
            newX = x;
            newY = y;

            StartFireBall();
        }

        private void StartFireBall()
        {
            _fire = true;
            _isVisiable = true;
            _startPosition = newY;
            if (Started == false)
                _startVelocity = 0;
            else
                _startVelocity = -15;
            Started = true;
            _timeCount = 0;
        }
        private void OnFire(object sender, EventArgs e)
        {
            if(Started)
            {
                if(_fire)
                {
                    _timeCount += 250.0f / 1000.0f;

                    newY = (int)CalcFireBallPosition();
                    newX += 5 * _dirX;

                    
                }
                if(newX >= Screen.Instance.Output.x + Screen.Instance.Output.width)
                {
                    Started = false;
                    _isVisiable = false;
                }
                if (newX < Screen.Instance.Output.x)
                {
                    Started = false;
                    _isVisiable = false;
                }
            }

        }

        private float CalcFireBallPosition()
        {
            return _startPosition + _startVelocity * _timeCount + 4.9f * _timeCount * _timeCount;
        }

        public override void LoadEvent()
        {
            base.LoadEvent();
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, OnFire);

        }
        public FireBall(int x, int y) : base(ObjectType.OT_FireBall)
        {
            this.x = x;
            this.y = y;

            _fire = false;
            _isVisiable = false;
            _animatedCount = 2;

            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, OnFire);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);
            // this.image = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Image\\Mario\\fireball.png"));
        }

    }
}
