namespace GenDI.Builder.Standard
{
    public interface ITypedBuilder<out T>
    {
        T AsType(string type);
    }
}