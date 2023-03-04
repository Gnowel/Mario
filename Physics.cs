using System;

public class Physics
{
	public Physics()
	{
		public float gravity = 0;
		public float a = 0.4f;

        public void Gravity(float posY)
        {
            if (posY < 576 || a < 0)
            {
                if (a != 0.4f)
                {
                    a += 0.005f;

                }

                posY += gravity;
                gravity += a;
            }
        }

        public void AddForce(float forceValue, bool IsJumping)
        {
            if (IsJumping)
            {
                gravity = 0;
                a = -0.125f;
            }
        }

        public void ApplyPhesics()
        {
            Physics();
        }
}


