using System.Collections.Generic;
using System.Text;

namespace GenDI.Builder.Context.Extension
{
    public static class StringBuilderExtension
    {
        public static void AppendParameters(this StringBuilder stringBuilder, Dictionary<string, ParameterContext> parameters)
        {
            stringBuilder.Append("(");
            var index = 0;
            foreach (var parameterContext in parameters.Values)
            {
                index++;
                parameterContext.Append(stringBuilder, 0);
                if (index != parameters.Values.Count)
                {
                    stringBuilder.Append(", ");
                }
            }
            stringBuilder.AppendLine(")");
        }
    }
}