using System.Text;

namespace GenDI.Builder.Context
{
    public class ParameterContext : NameContextBase, IParameterBuilder
    {
        private string _asType = "object";
        private bool _withThis;
        
        public IParameterBuilder AsType(string type)
        {
            _asType = type;
            return this;
        }

        public IParameterBuilder WithThis()
        {
            _withThis = true;
            return this;
        }

        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.Append($"{(_withThis ? "this " : "")}{_asType} {Name}");
        }
    }
}