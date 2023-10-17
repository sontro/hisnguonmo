using HIS.Desktop.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class SereServEkipADO
    {
        public long? EKIP_TEMP_ID { get; set; }
        public List<HisEkipUserADO> listEkipUser { get; set; }
    }
}
