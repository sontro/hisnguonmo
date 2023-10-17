using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Ksk.Approve
{
    class HisTreatmentKskApprove : BusinessBase
    {
        internal HisTreatmentKskApprove()
            : base()
        {

        }

        internal HisTreatmentKskApprove(CommonParam param)
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
                valid = valid && checker.IsNotApprove(listRaw);

                if (valid)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    List<string> sqls = new List<string>();
                    long time = Inventec.Common.DateTime.Get.Now().Value;
                    string sqlTreat = DAOWorker.SqlDAO.AddInClause(data, String.Format("UPDATE HIS_TREATMENT SET MODIFIER = '{0}', IS_KSK_APPROVE = 1 WHERE %IN_CLAUSE%", loginname), "ID");
                    string sqlTreatInTime = DAOWorker.SqlDAO.AddInClause(data, String.Format("UPDATE HIS_TREATMENT SET IN_TIME = {0} WHERE IN_TIME > {1} AND %IN_CLAUSE%", time, time), "ID");
                    sqls.Add(sqlTreat);
                    sqls.Add(sqlTreatInTime);
                    string sqlReq = DAOWorker.SqlDAO.AddInClause(data, String.Format("UPDATE HIS_SERVICE_REQ SET TDL_IS_KSK_APPROVE = 1, INTRUCTION_TIME = {0} WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND %IN_CLAUSE%", time), "TREATMENT_ID");
                    sqls.Add(sqlReq);

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }
                    result = true;
                    listRaw.ForEach(o => o.IS_KSK_APPROVE = Constant.IS_TRUE);
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
