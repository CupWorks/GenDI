using System.Text;
using GenDI.Builder;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GenDI
{
    [Generator]
    public class DependencyContainerGenerator : ISourceGenerator
    {
        private const string GeneratedNamespace = nameof(GenDI);
        private const string InterfaceName = "IDependencyContainer";
        private const string ClassName = "DependencyContainer";
        private const string ExtensionClassName = "DependencyContainerExtension";

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var source = new SourceBuilder()
                 .AddNamespace(GeneratedNamespace, n => n
                     .AddInterface(InterfaceName)
                     .AddClass(ClassName, c => c
                         .AddInterface(InterfaceName)
                         .AddFunction("Resolve", m => m
                             .AddGeneric("T")
                             .AddSnippet(s => s
                                 .AddSwitch("typeof(T)"))))
                     .AddClass(ExtensionClassName, ec => ec
                         .AsStatic()
                         .AddFunction("Test", tf => tf
                             .AsStatic()
                             .AddParameter("self", fp => fp
                                 .WithThis()
                                 .AsType(ClassName)))))
                 .Create();
            
            context.AddSource($"{ClassName}.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}