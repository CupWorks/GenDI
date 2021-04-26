using GenDI.Builder.Standard;

namespace GenDI.Builder
{
    public interface IFunctionBuilder : IMethodeBuilder<IFunctionBuilder>, IStaticModifierBuilder<IFunctionBuilder>
    {
        IFunctionBuilder WithReturn(string name);
        IFunctionBuilder AddGeneric(string name);
    }
}