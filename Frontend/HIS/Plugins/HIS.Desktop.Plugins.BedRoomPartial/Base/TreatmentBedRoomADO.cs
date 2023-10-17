using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedRoomPartial.Base
{
    public class TreatmentBedRoomADO : MOS.EFMODEL.DataModels.L_HIS_TREATMENT_BED_ROOM
    {
        public string ADD_TIME_STR { get; set; }
        public string PATIENT_CLASSIFY_NAME { get; set; }
        public string DISPLAY_COLOR { get; set; }

        public TreatmentBedRoomADO() { }

        public TreatmentBedRoomADO(MOS.EFMODEL.DataModels.L_HIS_TREATMENT_BED_ROOM data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<TreatmentBedRoomADO>(this, data);

                    this.ADD_TIME_STR = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.ADD_TIME);

                    if (data.TDL_PATIENT_CLASSIFY_ID.HasValue)
                    {
                        HIS_PATIENT_CLASSIFY classify = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<HIS_PATIENT_CLASSIFY>().FirstOrDefault(o => o.ID == data.TDL_PATIENT_CLASSIFY_ID);
                        this.PATIENT_CLASSIFY_NAME = classify != null ? classify.PATIENT_CLASSIFY_NAME : null;
                        this.DISPLAY_COLOR = classify != null ? classify.DISPLAY_COLOR : null;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
