using System;

namespace GenDI.Builder
{
    public interface ISnippetBuilder
    {
        ISnippetBuilder AddText(string text);
        ISnippetBuilder AddSwitch(string name, Action<ISwitchBuilder>? switchBuilder = null);
    }
}