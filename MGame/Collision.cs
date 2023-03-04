using System;
using System.Drawing;

public enum CollisionDirection{CD_Right, CD_Left, CD_Up, CD_Down }

[Serializable]
public class Collision
{
	public Rectangle Srs;
	public Rectangle Dest;
	public CollisionDirection Dir;
    public Collision(Rectangle S, Rectangle D, CollisionDirection C)
    {
        this.Srs = S;
        this.Dest = D;
        this.Dir = C;
    }
}
