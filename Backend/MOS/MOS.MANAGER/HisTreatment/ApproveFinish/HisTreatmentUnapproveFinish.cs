using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.ApproveFinish
{
    class HisTreatmentUnapproveFinish : BusinessBase
    {
        internal HisTreatmentUnapproveFinish()
            : base()
        {

        }

        internal HisTreatmentUnapproveFinish(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTreatmentApproveFinishSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                valid = valid && checker.IsUnpause(raw);
                valid = valid && checker.IsApproveFinish(raw);

                if (valid)
                {
                    raw.IS_APPROVE_FINISH = null;
                    raw.APPROVE_FINISH_NOTE = null;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET IS_APPROVE_FINISH = NULL, APPROVE_FINISH_NOTE = NULL, MODIFIER = :param1  WHERE ID = :param2", loginname, raw.ID))
                    {
                        throw new Exception("Update HisTreatment that bai");
                    }
                    resultData = raw;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
