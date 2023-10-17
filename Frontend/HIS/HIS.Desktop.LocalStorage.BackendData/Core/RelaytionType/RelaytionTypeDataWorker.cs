using HIS.Desktop.LocalStorage.BackendData.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Core.RelaytionType
{
    public class RelaytionTypeDataWorker
    {
        public static List<RelaytionTypeADO> RelaytionTypeADOs
        {
            get
            {
                List<RelaytionTypeADO> relaytionTypes = new List<RelaytionTypeADO>();
                relaytionTypes.Add(new RelaytionTypeADO() { Name = "Cha" });
                relaytionTypes.Add(new RelaytionTypeADO() { Name = "Mẹ" });
                relaytionTypes.Add(new RelaytionTypeADO() { Name = "Khác" });
                return relaytionTypes;
            }
        }
    }
}
