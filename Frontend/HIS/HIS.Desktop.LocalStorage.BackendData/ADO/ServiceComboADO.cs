using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class ServiceComboADO
    {
        public ServiceComboADO() { }

        public long PatientTypeId { get; set; }
        public List<SereServADO> ServiceIsleafADOs { get; set; }
        public List<ServiceADO> ServiceAllADOs { get; set; }
        public List<ServiceADO> ServiceParentADOs { get; set; }
        public List<ServiceADO> ServiceParentADOForGridServices { get; set; }
        public List<MOS.EFMODEL.DataModels.V_HIS_SERVICE_ROOM> ServiceRooms { get; set; }
    }
}
