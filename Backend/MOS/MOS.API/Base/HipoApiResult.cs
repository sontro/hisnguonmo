using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOS.API.Base
{
    public class HipoApiResult<T>
    {
        public T data { get; set; }
        public string message { get; set; }
        public long status { get; set; }

        public HipoApiResult(T data)
        {
            this.data = data;
        }

        public void SetValue(T data, bool success, List<string> message)
        {
            this.data = data;
            this.status = success ? 1 : 0;
            this.message = String.Join(".", message);
        }
    }
}