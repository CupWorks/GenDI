using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder.Context
{
    public class NamespaceContext : NameContextBase, INamespaceBuilder
    {
        private readonly Dictionary<string, NameContextBase> _objects = new();

        public INamespaceBuilder AddClass(string name, Action<IClassBuilder>? classBuilder = null)
        {
            _objects.AddOrCreate<ClassContext>(name, classBuilder);
            return this;
        }

        public INamespaceBuilder AddClass(string name, out IClassBuilder classBuilder)
        {
            classBuilder = _objects.AddOrCreate<ClassContext>(name, null);
            return this;
        }

        public INamespaceBuilder AddEnum(string name, Action<IEnumBuilder>? enumBuilder = null)
        {
            _objects.AddOrCreate<EnumContext>(name, enumBuilder);
            return this;
        }

        public INamespaceBuilder AddInterface(string name, Action<IInterfaceBuilder>? interfaceBuilder = null)
        {
            _objects.AddOrCreate<InterfaceContext>(name, interfaceBuilder);
            return this;
        }

        public override void Append(StringBuilder stringBuilder, uint intend = 0)
        {
            stringBuilder.AppendLine($"{CreateIntend(intend)}namespace {Name}");
            stringBuilder.AppendLine($"{CreateIntend(intend)}{{");

            var index = 0;
            foreach (var context in _objects.Values)
            {
                if (index != 0)
                {
                    stringBuilder.AppendLine();
                }
                index++;
                context.Append(stringBuilder, intend + 1);
            }
            
            stringBuilder.AppendLine($"{CreateIntend(intend)}}}");
        }
    }
}