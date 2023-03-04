using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;


namespace MGame
{

    [Serializable]
    public class Mario : AnimatedGraphicObject
    {
        public delegate void MarioDiedDelegate();
        public static event MarioDiedDelegate MarioDiedEvent;

        public delegate void LevelCompletedDelegate();
        public static event LevelCompletedDelegate LevelCompletedEvent;

        public class MarioEventArgs : EventArgs
        {
            public int x, y;
            public Collision collision;
            public GraphicObject graphicObject;
            public MarioType marioType;
            public MarioJumpState marioJumpState;

            public MarioEventArgs() : base() { }
        }

        public delegate void MarioHandler(object sender, MarioEventArgs e);
        public static event MarioHandler IntersectEvent;
        public static event MarioHandler MoveEvent;
        public enum MarioJumpState
        {
            None,
            Up,
            Down
        };
        public enum MarioMoveState
        {
            None,
            Stopping,
            Right,
            Left
        };
        public enum MarioDirection
        {
            Left,
            Right
        };

        public enum MarioType
        {
            Small,
            Big,
            Fire
        };

        private MarioJumpState State;
        private MarioMoveState MoveState;
        private MarioDirection Direction;
        public MarioType Type;

        private bool Moving;
        public bool isUpPressed;
        public bool EnterPressed;
        //Y
        [NonSerialized] private float _startVelocity;
        [NonSerialized] private float _startPosition;
        [NonSerialized] private float _currentPosition;
        [NonSerialized] private float _oldPosition;
        [NonSerialized] private float _timeCount;

        //X
        [NonSerialized] private float _xAdd;
        [NonSerialized] private float _xCount;

        private int _fireBallIndex;

        public byte NumberOfCollectedCoins;
        public byte NumberOfCollectedLife;

        public List<FireBall> FireBalls;

        private bool _isBlinking, _isBlinkingShow;
        private int _blinkValue;

        public void OnMarioDiedEvent ()
        {
            if (MarioDiedEvent != null)
                MarioDiedEvent();
        }
        public void OnLevelCompletedEvent()
        {
            if (LevelCompletedEvent != null)
                LevelCompletedEvent();
        }
        public void MarioDie()
        {
            if (NumberOfCollectedLife == 0)
            {
                OnMarioDiedEvent();
            }
        }
        private void StartBlinking()
        {
            if(_isBlinking == false)
            {
                _isBlinking = true;
                _blinkValue = 0;
            }
            
        }
        protected override void OnAnimate(object sender, EventArgs e)
        {
            if(_isBlinking)
            {
                _blinkValue++;

                _isBlinkingShow = (_blinkValue % 2 == 0);

                if(_blinkValue == 20)
                {
                    _isBlinking = false;
                    _isBlinkingShow = true;

                }
            }
        }
        public void MarioHit()
        {
            if (!_isBlinking) {
                switch (Type)
                {
                    case MarioType.Small:
                        if(!_isBlinking)
                            NumberOfCollectedLife--;
                        StartBlinking();
                        MarioDie();
                        break;
                    case MarioType.Big:
                        Type = MarioType.Small;
                        StartBlinking();
                        SetWidthHeight();
                        break;
                    case MarioType.Fire:
                        Type = MarioType.Big;
                        StartBlinking();
                        SetWidthHeight();
                        break;
                }
            }
        }
        public void MarioFireBall()
        {
            if (Type != MarioType.Fire)
                return;
            FireBall.FireBallDir D;

            if (!FireBalls[_fireBallIndex].Started)
            {
                if (Direction == MarioDirection.Right)
                    D = FireBall.FireBallDir.Right;
                else
                    D = FireBall.FireBallDir.Left;

                FireBalls[_fireBallIndex].RunFireBall(x, y, D);
                _fireBallIndex = (_fireBallIndex + 1) % 2;
            }
        }
        public override Rectangle GetObjectRect()
        {
            if (Type == MarioType.Small)
                return new Rectangle(x, y, 32, 20);
            if (Type == MarioType.Big)
                return new Rectangle(x, y, 32, 32);
            if (Type == MarioType.Fire)
                return new Rectangle(x, y, 32, 32);

            else
                return Rectangle.Empty;
        }

        private void ChangeCoinCount() => NumberOfCollectedCoins++;
        private void ChangeLifeCount() => NumberOfCollectedLife++;
        public void OnIntersectEvent (object sender, MarioEventArgs ev)
        {
            if(IntersectEvent != null)
                IntersectEvent(this, ev);
        }
        public void OnMoveEvent(object sender, MarioEventArgs ev)
        {
            if (MoveEvent != null)
                MoveEvent(this, ev);
        }
        public override void Intersection(Collision c, GraphicObject g)
        {
            base.Intersection(c, g);

            MarioEventArgs ev = new MarioEventArgs();

            ev.collision = c;
            ev.graphicObject = g;
            ev.marioType = Type;
            ev.marioJumpState = State;

            OnIntersectEvent(this, ev);

            if(g is MonsterGoomba || g is MonsterKoopa)
            {
                if (c.Dir == CollisionDirection.CD_Up )
                {
                    if(isUpPressed)
                        Jump(true, 0);
                    else
                      Jump(true, -20);
                }
                return;
            }

            if(g is Flower)
            {
                if(Type != MarioType.Fire)
                {
                    Type = MarioType.Fire;
                    SetWidthHeight();
                }
            }
            if ( g is MushRed)
            {
                if (Type == MarioType.Small)
                {
                    Type = MarioType.Big;
                    SetWidthHeight();
                }
            }
            if (g is Exit)
            {
                if (EnterPressed)
                {
                    EnterPressed = false;
                    OnLevelCompletedEvent();
                }
            }

            if (g is Brick || g is PipeUp || g is GroundBrick || g is SteelBlock || g is BlockQuestion) 
            {
                if (c.Dir == CollisionDirection.CD_Up)
                {
                    this.y = g.newY - this._height;
                    State = MarioJumpState.None;
                    //SetDirections();
                }
                if (c.Dir == CollisionDirection.CD_Down)
                {
                    if (State == MarioJumpState.Up)
                    {
                        State = MarioJumpState.Down;
                        _startPosition = 0;
                        _timeCount = 0;
                        _startVelocity = 0;
                    }
                }
                if (c.Dir == CollisionDirection.CD_Right)
                {
                    this.x = g.newX + this._width;
                    SetDirections();
                }
                if (c.Dir == CollisionDirection.CD_Left)
                {
                    this.x = g.newX - this._width;
                    SetDirections();
                }
            }
        }
        public override void IntersectionNone()
        {

            if(State == MarioJumpState.None)
            {
                State = MarioJumpState.Down;
                _startPosition = y;
                _timeCount = 0;
                _startVelocity = 0;
            }
        }

        public override void SetWidthHeight()
        {
            newX = x;
            newY = y;
            if(Type == MarioType.Small)
            {
                _width = 32;
                _height = 20;
                y += 12;
                _bitmap = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(ObjectType.OT_MarioSmall.ToString()));
            }
            if(Type == MarioType.Big)
            {
                _width = 32;
                _height = 32;
                y -= 12;
                _bitmap = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(ObjectType.OT_MarioBig.ToString()));
            }
            if(Type == MarioType.Fire)
            {
                _width = 32;
                _height = 32;
                y -= 12;
                _bitmap = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(ObjectType.OT_MarioFire.ToString()));
            }

        }

        public override void Draw()
        {
            if (_isBlinkingShow)
            {
                Graphics graphics;
                graphics = Screen.Instance.allWindow.graphics;

                DEST.X = x;
                DEST.Y = y;
                DEST.Width = _width;
                DEST.Height = _height;

                SRC.X = (int)(15.9f * _imageIndex);
                SRC.Y = 0;
                SRC.Width = _bitmap.Width / 6;
                SRC.Height = _bitmap.Height;
                graphics.DrawImage(_bitmap, DEST, SRC, GraphicsUnit.Pixel);
            }
        }

        /*
        private bool CheckCollisionRight()
        {
            bool res = false;
            for(int i = 0; i < IntersectsObjects.Count; i ++)
                if(IntersectsObjects[i].Dir == CollisionDirection.CD_Right && IntersectsObjects[i] != null)
                    res = true;
            return res;
        }
        private bool CheckCollisionLeft()
        {
            bool res = false;
            if (x <= 0)
                return true;

            for (int i = 0; i < IntersectsObjects.Count; i++)
                if (IntersectsObjects[i].Dir == CollisionDirection.CD_Left && IntersectsObjects[i] != null)
                    res = true;
            return res;
        }
        */
        public void OnMoveTick(object sender, EventArgs e)
        {
           // Debug.WriteLine($"x = {x}, y = {y}");
            if (MoveState != MarioMoveState.Stopping && MoveState != MarioMoveState.None)
            {
                Moving = true;
                SetDirections();
                if (_xAdd < 10) 
                    _xAdd += 3;  //0.5

                if (MoveState == MarioMoveState.Right)
                {
                    SetDirections();
                    if(x <= 4764)
                        x += (int)_xAdd;
 
                }
                if (MoveState == MarioMoveState.Left)
                {
                    SetDirections();
                     if(x >= 9)
                         x -= (int)_xAdd;
                }
            }
            if(MoveState == MarioMoveState.Stopping)
            {
                Moving = true;
                SetDirections();
                _xCount = (float)Math.Sqrt(_xCount);


                if (MoveState == MarioMoveState.Right)
                    x += (int)_xCount;
                if (MoveState == MarioMoveState.Left)
                    x -= (int)_xCount;
                if(_xCount < 1.05)
                {
                    MoveState = MarioMoveState.None;
                    Moving = false;
                }

            }
            MarioEventArgs ev = new MarioEventArgs();
            ev.y = y;
            OnMoveEvent(this,ev);
        }

        public void StopJump()
        {
            isUpPressed = false;
            if (State != MarioJumpState.None)
            {
                State = MarioJumpState.Down;
                _startPosition = y;
                _timeCount = 0;
                _startVelocity = 0;
            }
        }
        public void Jump(bool kill,float Velocity)
        {
            if (kill == false)
                isUpPressed = true;

            if (State == MarioJumpState.None || kill == true)
            {
                State = MarioJumpState.Up;
                //onGround = false;
                _startPosition = y;
                _oldPosition = y;
                _currentPosition = y;
                _timeCount = 0;
                if (Velocity != 0)
                    _startVelocity = Velocity;
                else
                    _startVelocity = -39;// -30
            }
        }
        public void onJumpTick(object sender, EventArgs e)
        {
            if (y >= 448)
                OnMarioDiedEvent();
            SetDirections();
            if (State != MarioJumpState.None)
            {
                _timeCount += (460.0f / 1000.0f); //300
                _oldPosition = _currentPosition;
                _currentPosition = CalcMarioJump();
                if (State == MarioJumpState.Up)
                    y = (int)(_currentPosition);
                else 
                    y += (int)(6 + (int)_timeCount);

                if (State == MarioJumpState.Up)
                    if (_currentPosition > _oldPosition)
                    {
                        State = MarioJumpState.Down;
                        _timeCount = 0;
                    }
            }
            else
                _timeCount = 0;

            MarioEventArgs ev = new MarioEventArgs();
            ev.x = x;
            OnMoveEvent(this, ev);
        }

        private float CalcMarioJump()
        {
            return _startPosition + _startVelocity * _timeCount + 4f * _timeCount * _timeCount;
        }

        public void CharacterMove(MarioMoveState s)
        {
            MoveState = s;
            if (Direction != MarioDirection.Left)
                if (s == MarioMoveState.Left)
                    Direction = MarioDirection.Left;
            if (Direction != MarioDirection.Right)
                if (s == MarioMoveState.Right)
                    Direction = MarioDirection.Right;
        }

        public void StopMove()
        {
            if (MoveState != MarioMoveState.Stopping)
            {
                switch (MoveState)
                {
                    case MarioMoveState.Left:
                        Direction = MarioDirection.Left; break;

                    case MarioMoveState.Right:
                        Direction = MarioDirection.Right; break;
                }
                MoveState = MarioMoveState.Stopping;
                _xAdd = 0;
                // XIn = 10000;
                /*if (!onGround)
                {
                    _xCount = 5;
                    _xAdd = 0;
                }*/
            }
        }
        private void SetDirections()
        {
            if (State != MarioJumpState.None)
            {
                if (Direction == MarioDirection.Left)
                    _imageIndex = 4;
                if (Direction == MarioDirection.Right)
                    _imageIndex = 5;
            }
            else if(Moving)
            {
                if (Direction == MarioDirection.Left)
                    if (_imageIndex == 0)
                        _imageIndex = 1;
                    else
                        _imageIndex = 0;
                if(Direction == MarioDirection.Right)
                    if (_imageIndex == 2)
                        _imageIndex = 3;
                    else
                        _imageIndex = 2;
            }
            else
            {
                if (Direction == MarioDirection.Right)
                    _imageIndex = 2;
                if (Direction == MarioDirection.Left)
                    _imageIndex = 0;
            }
        }

        public override void LoadEvent()
        {
            //base.LoadEvent();
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, OnMoveTick);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onJumpTick);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);


            Coin.TakeEvent += ChangeCoinCount;
            MushLife.TakeEvent += ChangeLifeCount;
            MonsterGoomba.HitEvent += MarioHit;
            MonsterKoopa.HitEvent += MarioHit;
        }
        public Mario(int x, int y) : base(ObjectType.OT_MarioSmall)
        {

            SetWidthHeight();

            this.x = x * 32;
            this.y = y * 32 + 12;

            NumberOfCollectedLife = 3;

            _isBlinking = false;
            _isBlinkingShow = true;

            Direction = MarioDirection.Right;
            State = MarioJumpState.None;
            MoveState = MarioMoveState.None;
            Type = MarioType.Small;

            FireBalls = new List<FireBall>();
            for (byte i = 0; i < 2; i++)
                FireBalls.Add(new FireBall(0, 0));

            for(byte i = 0; i < 2; i++)
                AddObject(FireBalls[i]);

            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, OnMoveTick);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, onJumpTick);
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);

            Coin.TakeEvent += ChangeCoinCount;
            MushLife.TakeEvent += ChangeLifeCount;
            MonsterGoomba.HitEvent += MarioHit;
            MonsterKoopa.HitEvent += MarioHit;
        }

        
    }
}
