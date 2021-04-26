namespace GenDI.Test.Mock
{
    [Bind(Scope = BindScope.Single)]
    public class ServiceSingle : ServiceBase, IServiceSingle
    {
    }
}