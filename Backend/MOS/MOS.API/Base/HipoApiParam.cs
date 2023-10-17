using Inventec.Core;

namespace MOS.API.Base
{
    public class HipoApiParam<T>
    {
        public CommonParam CommonParam { get; set; }
        public T data { get; set; }
    }
}