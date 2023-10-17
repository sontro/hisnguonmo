using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient.Redis
{
    public enum RedisSaveType
    {
        [Description("Lưu trữ dưới dạng key - value cơ bản")]
        RawKeyValue = 1,
        [Description("Lưu trữ qua IRedisTypedClient")]
        IRedisList = 2,
        [Description("Lưu trữ qua Urn Entry")]
        Urn = 3
    }
}
