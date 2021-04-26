using System;
using System.Collections.Generic;
using System.Text;
using GenDI.Builder.Context;
using GenDI.Builder.Context.Extension;

namespace GenDI.Builder
{
    public class SourceBuilder
    {
        private readonly StringBuilder _stringBuilder = new();

        private readonly Dictionary<string, UsingContext> _usings = new();
        private readonly Dictionary<string, NamespaceContext> _namespaces = new();

        public SourceBuilder AddUsing(string name)
        {
            _usings.AddOrCreate(name, null);
            return this;
        }

        public SourceBuilder AddNamespace(string name, Action<INamespaceBuilder>? namespaceBuilder = null)
        {
            _namespaces.AddOrCreate(name, namespaceBuilder);
            return this;
        }
        
        public string Create()
        {
            foreach (var usingContext in _usings.Values)
            {
                usingContext.Append(_stringBuilder, 0);
            }
            
            foreach (var namespacesContext in _namespaces.Values)
            {
                _stringBuilder.AppendLine();
                namespacesContext.Append(_stringBuilder, 0);
            }
            
            return _stringBuilder.ToString();
        }
    }
}