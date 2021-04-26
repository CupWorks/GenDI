using System.Text;

namespace GenDI.Builder.Context
{
    public class TextContext : ContextBase, ITextBuilder
    {
        private string? _withText;
        
        public void WithText(string text)
        {
            _withText = text;
        }
        
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}{_withText}");
        }
    }
}