using System.Collections.Generic;

namespace Interfaces
{
    public interface IDataContainer<in T>
    {
        public T data { set; }
    }
}