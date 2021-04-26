namespace GenDI.Test.Mock
{
    [Bind(Scope = BindScope.Transient)]
    public class ServiceTransient : ServiceBase, IServiceSingle
    {
    }
}