using UnityEngine;

namespace OniExtract2024
{
    public class BVector2
    {
        public float x;
        public float y;

        public BVector2(Vector2 v)
        {
            x = v.x;
            y = v.y;
        }

        public BVector2(BVector2 v)
        {
            x = v.x;
            y = v.y;
        }

        public BVector2(CellOffset c)
        {
            if (c == null)
            {
                x = -99;
                y = -99;
            }
            else
            {
                x = c.x;
                y = c.y;
            }
        }

        public BVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
