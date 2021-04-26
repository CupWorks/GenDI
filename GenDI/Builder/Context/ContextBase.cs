using System.Text;

namespace GenDI.Builder.Context
{
    public abstract class ContextBase
    {
        private const string Intend = "    ";

        public abstract void Append(StringBuilder stringBuilder, uint intend = 0);

        protected static string CreateIntend(uint intend)
        {
            var output = "";
            for (var index = 0; index < intend; index++)
            {
                output += Intend;
            }
            return output;
        }
    }
}