using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule.ADO
{
    internal class TreatmentBedRoomADO : MOS.EFMODEL.DataModels.V_HIS_TREATMENT_BED_ROOM
    {
        public string PATIENT_TYPE_NAME { get; set; }

        internal TreatmentBedRoomADO() { }

        internal TreatmentBedRoomADO(MOS.EFMODEL.DataModels.V_HIS_TREATMENT_BED_ROOM data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<TreatmentBedRoomADO>(this, data);
                    var pt = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().FirstOrDefault(o => o.ID == data.TDL_PATIENT_TYPE_ID);
                    this.PATIENT_TYPE_NAME = pt != null ? pt.PATIENT_TYPE_NAME : "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
