using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class SwitchContext : NameContextBase, ISwitchBuilder
    {
        private readonly Dictionary<string, CaseContext> _cases = new();
        
        public ISwitchBuilder AddCase(string name, Action<ICaseBuilder>? caseBuilder)
        {
            _cases.AddOrCreate(name, caseBuilder);
            return this;
        }
        
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}switch({Name})");
            stringBuilder.AppendLine($"{CreateIntend(intend)}{{");

            foreach (var caseContext in _cases.Values)
            {
                caseContext.Append(stringBuilder, intend + 1);
            }
            
            stringBuilder.AppendLine($"{CreateIntend(intend)}}}");
        }
    }
}