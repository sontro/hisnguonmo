using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class DataADO
    {
        public DataADO() { }
        public DataADO(Type dataType, object data) 
        {
            DataType = dataType;
            Data = data;
        }

        public Type DataType { get; set; }
        public object Data { get; set; }
    }
}
