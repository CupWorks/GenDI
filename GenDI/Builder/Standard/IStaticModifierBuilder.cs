namespace GenDI.Builder.Standard
{
    public interface IStaticModifierBuilder<out T>
    {
        T AsStatic();
    }
}