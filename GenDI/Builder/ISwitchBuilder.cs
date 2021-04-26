using System;

namespace GenDI.Builder
{
    public interface ISwitchBuilder
    {
        ISwitchBuilder AddCase(string name, Action<ICaseBuilder>? caseBuilder = null);
    }
}