using System.Text;

namespace GenDI.Builder.Context
{
    public class PropertyContext : NameContextBase, IPropertyBuilder
    {
        private string? _asType;
        
        public IPropertyBuilder AsType(string type)
        {
            _asType = type;
            return this;
        }
        
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}public {(_asType ?? "object")} {Name} {{ get; init; }}");
        }
    }
}