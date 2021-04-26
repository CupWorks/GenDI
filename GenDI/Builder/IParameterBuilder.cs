using GenDI.Builder.Standard;

namespace GenDI.Builder
{
    public interface IParameterBuilder : ITypedBuilder<IParameterBuilder>
    {
        IParameterBuilder WithThis();
    }
}