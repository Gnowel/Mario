using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MGame
{
    [Serializable]
    public class AnimatedGraphicObject : StaticGraphicObject
    {
        protected int _animatedCount;
        protected bool _animated;

        public override void LoadEvent()
        {
            TimerGenerator.AddTimerEventHandler(TimerType.TT_100, OnAnimate);
        }

        protected virtual void OnAnimate (object sender, EventArgs e)
        {
            if(_animated == true)
            {
                _imageIndex++;
                if (_imageIndex >= _animatedCount)
                    _imageIndex = 0;
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
        
        public AnimatedGraphicObject(ObjectType type) : base(type)
        {
            _animated = true;
            _imageCount = (int)Math.Round((float)_bitmap.Width / _bitmap.Height);
        }
    }
}
