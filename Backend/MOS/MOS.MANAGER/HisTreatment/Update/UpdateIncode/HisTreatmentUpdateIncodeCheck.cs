using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.UpdateIncode
{
    class HisTreatmentUpdateIncodeCheck : BusinessBase
    {
        internal HisTreatmentUpdateIncodeCheck()
            : base()
        {

        }

        internal HisTreatmentUpdateIncodeCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool IsValidData(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (!HisTreatmentCFG.IS_MANUAL_IN_CODE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongChoPhepSuaSoVaoVien);
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(data.IN_CODE))
                {
                    HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                    filter.IN_CODE__EXACT = data.IN_CODE;
                    filter.ID__NOT_EQUAL = data.ID;
                    List<HIS_TREATMENT> exists = new HisTreatmentGet().Get(filter);

                    if (IsNotNullOrEmpty(exists))
                    {
                        List<string> treatmentCodes = exists.Select(o => o.TREATMENT_CODE).ToList();
                        string treatmentCodeStr = string.Join(",", treatmentCodes);

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TonTaiHoSoCoSoVaoVien, treatmentCodeStr, data.IN_CODE);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
