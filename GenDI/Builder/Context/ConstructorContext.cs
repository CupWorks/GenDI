using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class ConstructorContext : NameContextBase, IConstructorBuilder
    {
        private bool _isStatic;
        private readonly Dictionary<string, ParameterContext> _parameters = new();
        private readonly List<SnippetContext> _snippets = new();

        public IConstructorBuilder AsStatic()
        {
            _isStatic = true;
            return this;
        }

        public IConstructorBuilder AddParameter(string name, Action<IParameterBuilder>? parameterBuilder = null)
        {
            _parameters.AddOrCreate(name, parameterBuilder);
            return this;
        }

        public IConstructorBuilder AddSnippet(string snippet)
        {
            AddSnippet(builder => builder.AddText(snippet));
            return this;
        }

        public IConstructorBuilder AddSnippet(Action<ISnippetBuilder>? snippetBuilder)
        {
            _snippets.CreateAndAdd(snippetBuilder);
            return this;
        }
        
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.Append($"{CreateIntend(intend)}public{(_isStatic ? "static " : "")} {Name}");
            stringBuilder.AppendParameters(_parameters);
            
            stringBuilder.AppendLine($"{CreateIntend(intend)}{{");
            foreach (var snippetContext in _snippets)
            {
                snippetContext.Append(stringBuilder, intend + 1);
            }
            stringBuilder.AppendLine($"{CreateIntend(intend)}}}");
        }
    }
}