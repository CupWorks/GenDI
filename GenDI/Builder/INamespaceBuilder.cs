using System;

namespace GenDI.Builder
{
    public interface INamespaceBuilder
    {
        INamespaceBuilder AddClass(string className, Action<IClassBuilder>? classBuilder = null);
        INamespaceBuilder AddClass(string className, out IClassBuilder classBuilder);
        INamespaceBuilder AddEnum(string enumName, Action<IEnumBuilder>? enumBuilder = null);
        INamespaceBuilder AddInterface(string interfaceName, Action<IInterfaceBuilder>? interfaceBuilder = null);
    }
}