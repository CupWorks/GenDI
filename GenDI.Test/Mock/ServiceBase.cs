using System;

namespace GenDI.Test.Mock
{
    public abstract class ServiceBase : IService
    {
        public Guid Identifier => Guid.NewGuid();
    }
}