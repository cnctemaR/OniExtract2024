using UnityEngine;

namespace OniExtract2024
{
    public class BColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public BColor(Color c)
        {
            r = c.r;
            g = c.g;
            b = c.b;
            a = c.a;
        }

        public BColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}
