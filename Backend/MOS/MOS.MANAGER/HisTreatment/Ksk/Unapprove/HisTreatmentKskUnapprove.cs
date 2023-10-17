using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Ksk.Unapprove
{
    class HisTreatmentKskUnapprove : BusinessBase
    {
        internal HisTreatmentKskUnapprove()
            : base()
        {

        }

        internal HisTreatmentKskUnapprove(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> data, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TREATMENT> listRaw = new List<HIS_TREATMENT>();
                HisTreatmentKskCheck checker = new HisTreatmentKskCheck(param);
                HisTreatmentCheck commonChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNullOrEmpty(data);
                valid = valid && commonChecker.VerifyIds(data, listRaw);
                valid = valid && commonChecker.IsUnpause(listRaw);
                valid = valid && commonChecker.IsUnTemporaryLock(listRaw);
                valid = valid && commonChecker.IsUnLock(listRaw);
                valid = valid && commonChecker.IsUnLockHein(listRaw);
                valid = valid && checker.IsKskTreatment(listRaw);
                valid = valid && checker.IsApprove(listRaw);
                valid = valid && checker.IsNotExistsReqFinishOrProcessing(data);

                if (valid)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    List<string> sqls = new List<string>();
                    string sqlTreat = DAOWorker.SqlDAO.AddInClause(data, String.Format("UPDATE HIS_TREATMENT SET MODIFIER = '{0}', IS_KSK_APPROVE = NULL WHERE %IN_CLAUSE%", loginname), "ID");
                    sqls.Add(sqlTreat);
                    string sqlReq = DAOWorker.SqlDAO.AddInClause(data, "UPDATE HIS_SERVICE_REQ SET TDL_IS_KSK_APPROVE = NULL WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND %IN_CLAUSE%", "TREATMENT_ID");
                    sqls.Add(sqlReq);

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }
                    result = true;
                    listRaw.ForEach(o => o.IS_KSK_APPROVE = null);
                    resultData = listRaw;
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
