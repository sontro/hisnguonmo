using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.UpdateIncode
{
    class HisTreatmentUpdateIncode : BusinessBase
    {
        internal HisTreatmentUpdateIncode()
            : base()
        {

        }

        internal HisTreatmentUpdateIncode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_TREATMENT data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentUpdateIncodeCheck checker = new HisTreatmentUpdateIncodeCheck(param);
                HisTreatmentCheck commonChecker = new HisTreatmentCheck(param);

                valid = valid && commonChecker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsValidData(data);
                
                if (valid)
                {
                    string oldIncode = raw.IN_CODE;
                    raw.IN_CODE = data.IN_CODE;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        return false;
                    }
                    result = true;
                    resultData = raw;
                    new EventLogGenerator(EventLog.Enum.HisTreatment_SuaSoVaoVien, oldIncode, data.IN_CODE)
                        .TreatmentCode(resultData.TREATMENT_CODE).Run();
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
