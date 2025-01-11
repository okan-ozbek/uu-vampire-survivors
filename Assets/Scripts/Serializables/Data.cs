using System;

namespace Serializables
{
    public abstract class Data
    {
        public Guid Guid { get; } = Guid.NewGuid();
    }
}