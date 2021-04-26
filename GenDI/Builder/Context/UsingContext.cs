using System.Text;

namespace GenDI.Builder.Context
{
    public class UsingContext : NameContextBase
    {
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}using {Name};");
        }
    }
}