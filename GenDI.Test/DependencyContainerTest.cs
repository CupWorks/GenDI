using GenDI.Test.Mock;
using NUnit.Framework;

namespace GenDI.Test
{
    public class DependencyContainerTest
    {
        [Test]
        public void TestResolve()
        {
            var container = new DependencyContainer();
            
            container.Resolve<IServiceSingle>();
            container.ResolveServiceSingle();
            container.ResolveServiceTransient();
            
            Assert.IsNotNull(container);
        }
    }
}