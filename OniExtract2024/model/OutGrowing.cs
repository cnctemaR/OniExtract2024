
namespace OniExtract2024
{
    public class OutGrowing
    {
        public bool shouldGrowOld = true;
        public float maxAge = 2400f;

        public OutGrowing(Growing obj)
        {
            this.shouldGrowOld = obj.shouldGrowOld;
            this.maxAge = obj.maxAge;
        }
    }
}
