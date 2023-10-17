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

namespace MOS.MANAGER.HisTreatment.MediRecord
{
    class HisTreatmentUnrejectStore : BusinessBase
    {
        internal HisTreatmentUnrejectStore()
            : base()
        {

        }

        internal HisTreatmentUnrejectStore(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTreatmentRejectStoreSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                valid = valid && checker.IsRejectStore(raw);

                if (valid)
                {
                    raw.APPROVAL_STORE_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__CHOT;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET APPROVAL_STORE_STT_ID = :param1, MODIFIER = :param2  WHERE ID = :param3", IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__CHOT, loginname, raw.ID))
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
