using System;

namespace GenDI.Builder.Standard
{
    public interface IMethodeBuilder<out T>
    {
        T AddParameter(string name, Action<IParameterBuilder>? parameterBuilder = null);
        T AddSnippet(string snippet);
        T AddSnippet(Action<ISnippetBuilder>? snippetBuilder = null);
    }
}