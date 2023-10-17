using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.GetNextStoreBordereauCode
{
    class GetNextStoreBordereauCodeProcessor : GetBase
    {
        internal GetNextStoreBordereauCodeProcessor()
            : base()
        {
        }

        internal GetNextStoreBordereauCodeProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(GetStoreBordereauCodeSDO data, ref string resultData)
        {
            bool result = false;
            try
            {
                string nextStoreBordereauCode = null;

                GetNextStoreBordereauCodeCheck checker = new GetNextStoreBordereauCodeCheck(param);
                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);

                if (!valid) return result;

                long heinLockTimeFrom = Inventec.Common.DateTime.Get.StartDay(data.HeinLockTime).Value;
                long heinLockTimeTo = Inventec.Common.DateTime.Get.EndDay(data.HeinLockTime).Value;
                bool isNoiTru = data.TreatmentTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU;

                HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                filter.HEIN_LOCK_TIME_FROM = heinLockTimeFrom;
                filter.HEIN_LOCK_TIME_TO = heinLockTimeTo;
                filter.IS_NOI_TRU_TREATMENT_TYPE = isNoiTru;

                List<HIS_TREATMENT> treatments = new HisTreatmentGet().Get(filter);
                if (IsNotNullOrEmpty(treatments))
                {
                    var treat = treatments.OrderByDescending(o => o.STORE_BORDEREAU_CODE).FirstOrDefault();
                    if (treat != null && !string.IsNullOrWhiteSpace(treat.STORE_BORDEREAU_CODE))
                    {
                        nextStoreBordereauCode = string.Format("{0:00000}", Convert.ToInt64(treat.STORE_BORDEREAU_CODE) + 1);
                    }
                    else
                    {
                        nextStoreBordereauCode = string.Format("{0:00000}", 1);
                    }
                }
                else
                {
                    nextStoreBordereauCode = string.Format("{0:00000}", 1);
                }

                resultData = nextStoreBordereauCode;
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
