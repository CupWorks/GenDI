using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class InterfaceContext : NameContextBase, IInterfaceBuilder
    {
        private readonly Dictionary<string, NameContextBase> _members = new();
        private readonly Dictionary<string, InterfaceImplementationContext> _interfaces = new();
        
        public IInterfaceBuilder AddFunction(string name, Action<IFunctionBuilder>? functionBuilder = null)
        {
            _members.AddOrCreate<FunctionContext>(name, functionBuilder);
            return this;
        }

        public IInterfaceBuilder AddInterface(string name)
        {
            _interfaces.AddOrCreate(name, null);
            return this;
        }
        
        public IInterfaceBuilder AddProperty(string name, Action<IPropertyBuilder>? propertyBuilder = null)
        {
            _members.AddOrCreate<PropertyContext>(name, propertyBuilder);
            return this;
        }
        
        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.Append($"{CreateIntend(intend)}public interface {Name}");
            
            var index = 0;
            foreach (var interfaceContext in _interfaces.Values)
            {
                if (index != 0)
                {
                    stringBuilder.Append(", ");
                }
                index++;
                interfaceContext.Append(stringBuilder, 0);
            }
            
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"{CreateIntend(intend)}{{");
            
            index = 0;
            foreach (var methodContext in _members.Values)
            {
                if (index != 0)
                {
                    stringBuilder.AppendLine();
                }
                index++;
                methodContext.Append(stringBuilder, (ushort)(intend + 1));
            }
            
            stringBuilder.AppendLine($"{CreateIntend(intend)}}}");
        }
    }
}