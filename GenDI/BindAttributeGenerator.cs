using System;
using System.Text;
using GenDI.Builder;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GenDI
{
    [Generator]
    public class BindAttributeGenerator : ISourceGenerator
    {
        private const string GeneratedNamespace = nameof(GenDI);
        private const string ClassName = "BindAttribute";
        private const string EnumName = "BindScope";
        
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var source = new SourceBuilder()
                .AddUsing("System")
                .AddNamespace(GeneratedNamespace, n => n
                    .AddEnum(EnumName, e => e
                        .AddValue("Single")
                        .AddValue("Transient"))
                    .AddClass(ClassName, c => c
                        .FromBase(nameof(Attribute))
                        .AddProperty("Scope", p => p
                            .AsType(EnumName))))
                .Create();
            
            context.AddSource($"{ClassName}.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}