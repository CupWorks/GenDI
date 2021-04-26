using System.Collections.Generic;
using System.Text;

namespace GenDI.Builder.Context
{
    public class EnumContext : NameContextBase, IEnumBuilder
    {
        private readonly List<string> _values = new();
        
        public IEnumBuilder AddValue(string name)
        {
            _values.Add(name);
            return this;
        }
        
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}public enum {Name}");
            stringBuilder.AppendLine($"{CreateIntend(intend)}{{");

            for (var index = 0; index < _values.Count; index++)
            {
                var value = _values[index];
                stringBuilder.Append($"{CreateIntend(intend + 1)}{value}");
                if (index != _values.Count)
                {
                    stringBuilder.AppendLine(",");
                }
            }
            
            stringBuilder.AppendLine($"{CreateIntend(intend)}}}");
        }
    }
}