using System;
using GenDI.Builder.Standard;

namespace GenDI.Builder
{
    public interface IClassBuilder : IObjectBuilder<IClassBuilder>, IStaticModifierBuilder<IClassBuilder>
    {
        IClassBuilder FromBase(string name);
        IClassBuilder AddConstructor(Action<IConstructorBuilder>? constructorBuilder = null);
    }
}