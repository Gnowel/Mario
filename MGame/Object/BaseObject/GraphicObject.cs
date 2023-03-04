using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MGame
{
    public enum ObjectType { OT_GroundBrick, OT_MarioSmall, OT_FireBall, OT_Cloud, OT_Bush, OT_Mountain, OT_BigCloud, OT_BigBush, OT_BigMountain, OT_Coin,
     OT_Brick, OT_BrickPiece, OT_Goomba, OT_PipeUp, OT_BlockQuestion, OT_Flower, OT_MushLife, OT_MushRed, OT_MarioBig, OT_MarioFire, OT_Koopa, OT_Exit, OT_SteelBlock
    };
    [Serializable]
    public class GraphicObject
    {
        public List<GraphicObject> IncomingObject;

        public int newX;
        public int newY;

        protected Rectangle ObjectRect;
        public Rectangle SRC;
        public Rectangle DEST;

        protected int x, y;
        protected Bitmap _bitmap;
        public  bool _isVisiable;
 
        protected int _width, _height;

        public ObjectType OT;
        
        public virtual void Draw() { }

        protected void AddObject(GraphicObject a)
        {
            if (IncomingObject == null)
                IncomingObject = new List<GraphicObject>();
            IncomingObject.Add(a);
        }

        public virtual void Intersection (Collision c, GraphicObject g)
        {
        }
        public virtual void IntersectionNone() { }

        public virtual void SetWidthHeight()
        {
            _width = 32;
            _height = 32;

            newX = x * _width;
            newY = y * _height;
        }

        public virtual Rectangle GetObjectRect()
        {
            ObjectRect.X = newX;
            ObjectRect.Y = newY;
            ObjectRect.Width = _width;
            ObjectRect.Height = _height;

            return ObjectRect;
        }
        public GraphicObject ()
        {
            ObjectRect = new Rectangle(0, 0, 0, 0);
            SRC = new Rectangle(0, 0, 0, 0);
            DEST = new Rectangle(0, 0, 0, 0);

            _isVisiable = true;
        }
    }
}
