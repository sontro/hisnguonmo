using Inventec.Core;

namespace MOS.API.Base
{
    public class ApiParam<T>
    {
        public CommonParam CommonParam { get; set; }
        public T ApiData { get; set; }
    }
}