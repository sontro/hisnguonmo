using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class SqlParamSDO
    {
        public long SqlParamId { get; set; }
        public object Value { get; set; }
    }

    public class ExecuteSqlSDO
    {
        public long SqlId { get; set; }
        public List<SqlParamSDO> SqlParams { get; set; }
    }
}
