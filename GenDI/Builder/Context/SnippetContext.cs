using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class SnippetContext : ContextBase, ISnippetBuilder
    {
        private readonly List<ContextBase> _snippets = new();
        
        public ISnippetBuilder AddText(string text)
        {
            _snippets.CreateAndAdd<TextContext>(context => context.WithText(text));
            return this;
        }

        public ISnippetBuilder AddSwitch(string name, Action<ISwitchBuilder>? switchBuilder)
        {
            _snippets.AddOrCreate<SwitchContext>(name, switchBuilder);
            return this;
        }

        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            foreach (var context in _snippets)
            {
                context.Append(stringBuilder, intend);
            }
        }
    }
}