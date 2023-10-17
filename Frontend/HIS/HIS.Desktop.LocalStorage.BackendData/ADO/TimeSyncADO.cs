using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class TimeSyncADO
    {
        public TimeSyncADO() { }
        public TimeSyncADO(Type dataType, string data) 
        {
            DataType = dataType;
            Data = data;
        }
        public Type DataType { get; set; }
        public string Data { get; set; }
    }
}
