using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace MGame
{
    [Serializable]
    public class Map
    {
        [NonSerialized] private string[] TileMap = new string[MapHeight];

        [NonSerialized] private const int MapHeight = 14;
        private Collision MainCollission;

        public Mario mario;

        private List<GraphicObject> Objects = new List<GraphicObject>();
        private List<GraphicObject> Background = new List<GraphicObject>();

        private string _levelName;
        private byte _levelCount;
        public byte LevelCount { get { return _levelCount; } set { _levelCount = value; } }

        [NonSerialized] private Point ButtomRight;
        [NonSerialized] private Point ButtomLeft;
        [NonSerialized] private Point TopRight;
        [NonSerialized] private Point TopLeft;

        private void MapReader ()
        {
            string filename = Path.GetFullPath(@"..\..\Maps\" + _levelName + _levelCount + ".txt");
            string s = "";
            using (StreamReader MapFile = new StreamReader(filename))
            {
                int i = 0;
                while((s = MapFile.ReadLine()) != null)
                {
                    TileMap[i] = s;
                    i++;
                }
            }

            for (int i = 0; i < TileMap.Length; i++)
            {
                for (int j = 0; j < TileMap[i].Length; j++)
                {
                    switch (TileMap[i][j])
                    {
                        case 'G':
                            Objects.Add(new GroundBrick(j, i)); break;
                        case 'M':
                            mario = new Mario(j, i);
                            Objects.Add(mario);
                            AddObject(mario);
                            break;
                        case '1':
                            Background.Add(new Cloud(j, i)); break;
                        case '2':
                            Background.Add(new Bush(j, i)); break;
                        case '3':
                            Background.Add(new Mountain(j, i)); break;
                        case '!':
                            Background.Add(new BigCloud(j, i)); break;
                        case '@':
                            Background.Add(new BigBush(j, i)); break;
                        case '#':
                            Background.Add(new BigMountain(j, i)); break;
                        case 'C':
                            Objects.Add(new Coin(j, i, false)); break;
                        case 'B':
                            Objects.Add(new Brick(j, i));
                            AddObject(Objects[Objects.Count - 1]);
                            break;
                        case 'K':
                            Objects.Add(new MonsterKoopa(j, i)); break;
                        case 'L':
                            Objects.Add(new MonsterGoomba(j, i)); break;
                        case 'P':
                            Objects.Add(new PipeUp(j, i)); break;
                        case '-':
                            Objects.Add(new BlockQuestion(j, i, ObjectType.OT_Coin));
                            AddObject(Objects[Objects.Count - 1]); break;
                        case '_':
                            Objects.Add(new BlockQuestion(j, i, ObjectType.OT_MushLife));
                            AddObject(Objects[Objects.Count - 1]); break;
                        case '=':
                            Objects.Add(new BlockQuestion(j, i, ObjectType.OT_MushRed));
                            AddObject(Objects[Objects.Count - 1]); break;
                        case '+':
                            Objects.Add(new BlockQuestion(j, i, ObjectType.OT_Flower));
                            AddObject(Objects[Objects.Count - 1]); break;
                        case 'E':
                            Objects.Add(new Exit(j, i)); break;
                        case 'S':
                            Objects.Add(new SteelBlock(j, i)); break;
       
                    }
                }
            }
        }

        public  void DrawMap()
        {
            foreach (var count in Background)
                count.Draw();
            foreach (var count in Objects)
                 count.Draw();
             //
             //mario.Draw();

        }


        private void CheckMapCollisions(object sender, EventArgs e)
        {
            int countIntersects = 0;

           // mario.IntersectsObjects.Clear();
            foreach (GraphicObject src in Objects)
            {
                countIntersects = 0;
                foreach (GraphicObject dest in Objects)
                {
                    if (src != dest)
                    {
                        if (src._isVisiable && dest._isVisiable)
                        {
                            Collision C = Intersects(src, dest);

                            if (C != null)
                            {
                                src.Intersection(C, dest);
                                countIntersects++;
                            }
                        }
                    }
                }
                if (countIntersects == 0)
                {
                    src.IntersectionNone();
                }
            }
           
   
        }

        private bool Contains (Point Src, Rectangle Dest)
        {
            if((Src.X >= Dest.X && Src.X <= Dest.Right)
                && (Src.Y >= Dest.Y && Src.Y <= Dest.Bottom))
                return true;
            else
                return false;
        }

        private Collision Intersects(GraphicObject SrcObject, GraphicObject DestObject)
        {
            Rectangle Src = SrcObject.GetObjectRect(); 
            Rectangle Dest = DestObject.GetObjectRect(); 

            if(Src.X + Src.Width < Dest.X )   return null;
            if(Src.Y + Src.Height < Dest.Y )  return null;

            if(Src.X > Dest.Width + Dest.X )  return null;
            if(Src.Y > Dest.Height + Dest.Y ) return null;

            int H, W;

            ButtomRight.X = Src.Right;
            ButtomRight.Y = Src.Bottom;
            ButtomLeft.X = Src.X;
            ButtomLeft.Y = Src.Bottom;
            TopRight.X = Src.Right;
            TopRight.Y = Src.Y;
            TopLeft.X = Src.X;
            TopLeft.Y = Src.Y;

            bool Found = false;

            CollisionDirection direction = CollisionDirection.CD_Up;
            if(Contains(ButtomRight,Dest))
            {
                Found = true;
                W = ButtomRight.X - Dest.X;
                H = ButtomRight.Y - Dest.Y;

                if (W > H)
                    direction = CollisionDirection.CD_Up;
                else if (H> W)
                    direction = CollisionDirection.CD_Left;

            }
            if(Contains(ButtomLeft, Dest))
            {
                Found = true;
                W = Dest.Right - ButtomLeft.X;
                H = ButtomLeft.Y - Dest.Y;

                if (W > H)
                    direction = CollisionDirection.CD_Up;
                else if (H > W)
                    direction = CollisionDirection.CD_Right;
            }
            if (Contains(TopRight, Dest))
            {
                Found = true;
                W = TopRight.X - Dest.X;
                H = Dest.Bottom - TopRight.Y;

                if (W > H)
                    direction = CollisionDirection.CD_Down;
                else 
                    direction = CollisionDirection.CD_Left;
            }
            if (Contains(TopLeft, Dest))
            {

                Found = true;
                W = Dest.Right - TopLeft.X;
                H = Dest.Bottom - TopLeft.Y;

                if (W > H)
                    direction = CollisionDirection.CD_Down;
                else
                    direction = CollisionDirection.CD_Right;
            }

            if (Found == false)
                return null;
            else
            {
                MainCollission = new Collision(Src, Dest, direction);
                return MainCollission;
            }
        }

        private void AddObject(GraphicObject g)
        {
            if(g != null)
            for (byte i = 0; i < ((AnimatedGraphicObject)g).IncomingObject.Count; i++)
                Objects.Add(((AnimatedGraphicObject)g).IncomingObject[i]);
        }

        public void LoadEvent()
        {
            foreach (GraphicObject i in Objects)
            {
               if(i is StaticGraphicObject)
                    ((StaticGraphicObject)i).LoadEvent();
            }
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, CheckMapCollisions);
        }
        public Map()
        {
            _levelCount = 1;
            _levelName = "Level";
            MapReader();
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, CheckMapCollisions);
        }
        public Map(byte NextLevel)
        {
            _levelCount = NextLevel;
            _levelName = "Level";
            MapReader();
            TimerGenerator.AddTimerEventHandler(TimerType.TT_50, CheckMapCollisions);
        }

    }
}
