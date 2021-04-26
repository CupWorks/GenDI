using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class FunctionContext : NameContextBase, IFunctionBuilder
    {
        private bool _isStatic;
        private readonly Dictionary<string, ParameterContext> _parameters = new();
        private readonly Dictionary<string, GenericContext> _generics = new();
        private readonly List<SnippetContext> _snippets = new();
        private string? _withReturn;

        public IFunctionBuilder AsStatic()
        {
            _isStatic = true;
            return this;
        }

        public IFunctionBuilder AddParameter(string name, Action<IParameterBuilder>? parameterBuilder = null)
        {
            _parameters.AddOrCreate(name, parameterBuilder);
            return this;
        }

        public IFunctionBuilder AddSnippet(string snippet)
        {
            AddSnippet(builder => builder.AddText(snippet));
            return this;
        }

        public IFunctionBuilder AddSnippet(Action<ISnippetBuilder>? snippetBuilder)
        {
            _snippets.CreateAndAdd(snippetBuilder);
            return this;
        }

        public IFunctionBuilder WithReturn(string name)
        {
            _withReturn = name;
            return this;
        }

        public IFunctionBuilder AddGeneric(string name)
        {
            _generics.AddOrCreate(name, null);
            return this;
        }

        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.Append($"{CreateIntend(intend)}public {(_isStatic ? "static " : "")}{(_withReturn ?? "void")} {Name}");

            var index = 0;
            foreach (var genericContext in _generics.Values)
            {
                if (index == 0)
                {
                    stringBuilder.Append("<");
                }
                index++;
                genericContext.Append(stringBuilder, 0);
                if (index == _generics.Values.Count)
                {
                    stringBuilder.Append(">");
                }
            }
            
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