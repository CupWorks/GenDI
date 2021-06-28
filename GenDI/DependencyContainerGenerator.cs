using System.Collections.Generic;
using System.Text;
using GenDI.Builder;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GenDI
{
    [Generator]
    public sealed class DependencyContainerGenerator : ISourceGenerator
    {
        private struct Dependency
        {
            public string Type { get; set; }
            public string FullType { get; set; }
        }
        
        private const string GeneratedNamespace = nameof(GenDI);
        private const string InterfaceName = "IDependencyContainer";
        private const string ClassName = "DependencyContainer";
        private const string ExtensionClassName = "DependencyContainerExtension";

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new BindSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;

            List<Dependency> dependencies = new ();
            var sourceBuilder = new SourceBuilder();
            if (context.SyntaxReceiver is BindSyntaxReceiver receiver)
            {
                foreach (var candidateTypeNode in receiver.Candidates)
                {
                    var model = compilation.GetSemanticModel(candidateTypeNode.SyntaxTree);
                    if (model.GetDeclaredSymbol(candidateTypeNode) is ITypeSymbol candidateTypeSymbol)
                    {
                        dependencies.Add(new Dependency
                        {
                            Type = candidateTypeSymbol.Name,
                            FullType = candidateTypeSymbol.ToDisplayString(new SymbolDisplayFormat(
                                SymbolDisplayGlobalNamespaceStyle.Omitted,
                                SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                                SymbolDisplayGenericsOptions.IncludeTypeParameters,
                                miscellaneousOptions: SymbolDisplayMiscellaneousOptions.ExpandNullable))
                        });
                    }
                }
            }

            IClassBuilder containerClassBuilder = null!;
            IClassBuilder extensionClassBuilder = null!;
            
            sourceBuilder
                .AddNamespace(GeneratedNamespace, n => n
                    .AddInterface(InterfaceName, i => i
                        .AddFunction("Resolve", m => m
                            .AddGeneric("T")))
                    .AddClass(ClassName, out containerClassBuilder)
                    .AddClass(ExtensionClassName, out extensionClassBuilder));
                
            containerClassBuilder?
                .AddInterface(InterfaceName)
                .AddFunction("Resolve", m => m
                    .AddGeneric("T")
                    .AddSnippet(s => s
                        .AddSwitch("typeof(T)")));

            extensionClassBuilder
                .AsStatic();

            CreateImplementation(dependencies, containerClassBuilder!, extensionClassBuilder!);

            context.AddSource($"{ClassName}.cs", SourceText.From(sourceBuilder.Create(), Encoding.UTF8));
        }

        private static void CreateImplementation(IEnumerable<Dependency> dependencies, IClassBuilder containerClassBuilder, IClassBuilder extensionClassBuilder)
        {
            foreach (var dependency in dependencies)
            {
                containerClassBuilder.AddProperty(dependency.FullType);
                
                extensionClassBuilder.AddFunction($"Resolve{dependency.Type}", f => f
                    .AsStatic()
                    .AddParameter("self", fp => fp
                        .WithThis()
                        .AsType(InterfaceName))
                    .AddSnippet($"return self.Resolve<{dependency.FullType}>;"));
            }
        }
    }
}