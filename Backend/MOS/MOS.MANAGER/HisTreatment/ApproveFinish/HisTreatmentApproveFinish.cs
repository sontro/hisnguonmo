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
    class HisTreatmentApproveFinish : BusinessBase
    {
        internal HisTreatmentApproveFinish()
            : base()
        {

        }

        internal HisTreatmentApproveFinish(CommonParam param)
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

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                valid = valid && checker.IsUnpause(raw);
                valid = valid && checker.IsUnapproveFinish(raw);

                if (valid)
                {
                    raw.IS_APPROVE_FINISH = Constant.IS_TRUE;
                    raw.APPROVE_FINISH_NOTE = data.ApproveFinishNote;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET IS_APPROVE_FINISH = :param1, APPROVE_FINISH_NOTE = :param2, MODIFIER = :param3  WHERE ID = :param4", raw.IS_APPROVE_FINISH, raw.APPROVE_FINISH_NOTE, loginname, raw.ID))
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
