using System;

namespace GenDI.Builder.Standard
{
    public interface IObjectBuilder<out T>
    {
        T AddFunction(string name, Action<IFunctionBuilder>? functionBuilder = null);
        T AddInterface(string name);
        T AddProperty(string name, Action<IPropertyBuilder>? propertyBuilder = null);
    }
}