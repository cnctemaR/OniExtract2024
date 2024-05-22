
namespace OniExtract2024
{
    public class OutElement : Element
    {

        public OutElement()
        {
        }

        public static OutElement GetOutElement(Element element)
        {
            OutElement child = new OutElement();
            var parentType = typeof(Element);
            var properties = parentType.GetProperties();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(element, null), null);
                }
            }
            child.substance = OutSubstance.GetOutSubstance(child.substance);
            return child;
        }
    }
}
