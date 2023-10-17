using Inventec.Core;

namespace SAR.API.Base
{
    public class ApiParam<T>
    {
        public CommonParam CommonParam { get; set; }
        public T ApiData { get; set; }
    }
}