
namespace OniExtract2024
{
    public class OutSubstance : Substance
    {

        public OutSubstance()
        {
        }

        public static OutSubstance GetOutSubstance(Substance substance)
        {
            OutSubstance child = new OutSubstance();
            var parentType = typeof(Substance);
            var properties = parentType.GetProperties();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(substance, null), null);
                }
            }
            child.material = null;
            return child;
        }
    }
}
