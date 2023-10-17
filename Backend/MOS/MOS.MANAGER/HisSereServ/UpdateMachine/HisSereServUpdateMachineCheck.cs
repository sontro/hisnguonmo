using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.UpdateMachine
{
    class HisSereServUpdateMachineCheck : BusinessBase
    {
        internal HisSereServUpdateMachineCheck()
            : base()
        {

        }

        internal HisSereServUpdateMachineCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidUpdateMachine(List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool valid = true;
            try
            {
                if(IsNotNullOrEmpty(serviceReqs))
                {
                    List<long> treatmentIds = serviceReqs.Select(o => o.TREATMENT_ID).ToList();
                    List<HIS_TREATMENT> treatments = new HisTreatmentGet().GetByIds(treatmentIds);
                    if (IsNotNullOrEmpty(treatments))
                    {
                        List<long> hisTreatmentIds = treatments.Where(o => o.IS_LOCK_HEIN == Constant.IS_TRUE && o.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).Select(o => o.ID).ToList();
                        if (IsNotNullOrEmpty(hisTreatmentIds))
                        {
                            List<string> serviceReqCodes = serviceReqs.Where(o => hisTreatmentIds.Contains(o.TREATMENT_ID)).Select(o => o.SERVICE_REQ_CODE).Distinct().ToList();
                            if (IsNotNullOrEmpty(serviceReqCodes))
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_YLenhDaDuyetHoSoBenhAnKhongChoPhepSuaMayYTe, string.Join(",", serviceReqCodes));
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
