using System.Text;

namespace GenDI.Builder.Context
{
    public class CaseContext : NameContextBase, ICaseBuilder
    {
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}case {Name}:");
            stringBuilder.AppendLine($"{CreateIntend(intend + 1)}break;");
        }
    }
}