
namespace OniExtract2024
{
    public class OutDecorProvider
    {
        public float baseRadius;
        public float baseDecor;

        public OutDecorProvider(DecorProvider obj)
        {
            this.baseDecor = obj.baseDecor;
            this.baseRadius = obj.baseRadius;
        }
    }
}
