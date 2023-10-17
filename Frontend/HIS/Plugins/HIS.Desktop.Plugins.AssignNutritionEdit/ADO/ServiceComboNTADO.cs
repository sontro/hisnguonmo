using HIS.Desktop.LocalStorage.BackendData.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignNutritionEdit.ADO
{
    public class ServiceComboNTADO
    {
        public ServiceComboNTADO() { }

        public long PatientTypeId { get; set; }
        public List<SSServiceADO> ServiceIsleafADOs { get; set; }
        public List<ServiceADO> ServiceAllADOs { get; set; }
        public List<ServiceADO> ServiceParentADOs { get; set; }
        public List<ServiceADO> ServiceParentADOForGridServices { get; set; }
        public List<MOS.EFMODEL.DataModels.V_HIS_SERVICE_ROOM> ServiceRooms { get; set; }
        public List<MOS.EFMODEL.DataModels.HIS_SERVICE_RATI> ServiceRati { get; set; }
    }
}
