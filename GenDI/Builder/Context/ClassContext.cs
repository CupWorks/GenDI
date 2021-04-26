using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class ClassContext : NameContextBase, IClassBuilder
    {
        private bool _isStatic;
        private readonly Dictionary<string, NameContextBase> _members = new();
        private readonly Dictionary<string, InterfaceImplementationContext> _interfaces = new();
        private string? _fromBase;

        public IClassBuilder AsStatic()
        {
            _isStatic = true;
            return this;
        }

        public IClassBuilder AddFunction(string name, Action<IFunctionBuilder>? functionBuilder = null)
        {
            _members.AddOrCreate<FunctionContext>(name, functionBuilder);
            return this;
        }

        public IClassBuilder AddInterface(string name)
        {
            _interfaces.AddOrCreate(name, null);
            return this;
        }

        public IClassBuilder FromBase(string name)
        {
            _fromBase = name;
            return this;
        }

        public IClassBuilder AddConstructor(Action<IConstructorBuilder>? constructorBuilder = null)
        {
            _members.AddOrCreate<ConstructorContext>(Name, constructorBuilder);
            return this;
        }
        
        public IClassBuilder AddProperty(string name, Action<IPropertyBuilder>? propertyBuilder = null)
        {
            _members.AddOrCreate<PropertyContext>(name, propertyBuilder);
            return this;
        }

        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.Append($"{CreateIntend(intend)}public {(_isStatic ? "static " : "")}class {Name}");

            if (_fromBase != null || _interfaces.Values.Count > 0)
            {
                stringBuilder.Append(" : ");
            }

            if (_fromBase != null)
            {
                stringBuilder.Append($"{_fromBase}");

                if (_interfaces.Values.Count > 0)
                {
                    stringBuilder.Append(", ");
                }
            }
            
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