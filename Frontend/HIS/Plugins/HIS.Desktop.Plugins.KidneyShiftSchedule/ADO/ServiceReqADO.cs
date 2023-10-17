using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule.ADO
{
    internal class ServiceReqADO : MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ_9
    {
        public string EXP_MEST_TEMPLATE_NAME { get; set; }
        public string MERCHINE_CODE { get; set; }
        public string SERVICE_REQ_STT_NAME { get; set; }

        internal ServiceReqADO() { }

        internal ServiceReqADO(MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ_9 data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ServiceReqADO>(this, data);
                    if (this.EXP_MEST_TEMPLATE_ID > 0 && GlobalDatas.ExpMestTemplates.ContainsKey(this.EXP_MEST_TEMPLATE_ID.Value))
                        this.EXP_MEST_TEMPLATE_NAME = GlobalDatas.ExpMestTemplates[this.EXP_MEST_TEMPLATE_ID.Value].EXP_MEST_TEMPLATE_NAME;                  

                    var mc = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_MACHINE>().Where(o => o.ID == this.MACHINE_ID).FirstOrDefault();
                    MERCHINE_CODE = mc != null ? mc.MACHINE_CODE : "";

                    if (String.IsNullOrEmpty(this.PATIENT_TYPE_NAME))
                    {
                        var pt = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE>().Where(o => o.ID == this.TDL_PATIENT_TYPE_ID).FirstOrDefault();
                        this.PATIENT_TYPE_NAME = pt != null ? pt.PATIENT_TYPE_NAME : "";                        
                    }
                    var stt = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ_STT>().Where(o => o.ID == this.SERVICE_REQ_STT_ID).FirstOrDefault();
                    SERVICE_REQ_STT_NAME = stt != null ? stt.SERVICE_REQ_STT_NAME : "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
