using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GenDI
{
    public sealed class BindSyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Candidates { get; } = new();
            
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax) return;
                
            foreach (var attributeList in typeDeclarationSyntax.AttributeLists)
            {
                foreach (var attribute in attributeList.Attributes)
                {
                    if(attribute.Name.ToString() == "Bind" ||
                       attribute.Name.ToString() == "BindAttribute")
                    {
                        Candidates.Add(typeDeclarationSyntax);
                    }
                }
            }
        }
    }
}