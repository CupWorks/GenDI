using System.Text;

namespace GenDI.Builder.Context
{
    public class GenericContext : NameContextBase
    {
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.Append(Name);
        }
    }
}