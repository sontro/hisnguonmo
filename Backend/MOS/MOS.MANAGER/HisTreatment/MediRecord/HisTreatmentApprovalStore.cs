﻿using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord
{
    class HisTreatmentApprovalStore : BusinessBase
    {
        internal HisTreatmentApprovalStore()
            : base()
        {

        }

        internal HisTreatmentApprovalStore(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> datas, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_TREATMENT> listRaw = new List<HIS_TREATMENT>();
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);

                valid = valid && IsNotNullOrEmpty(datas);
                valid = valid && checker.VerifyIds(datas, listRaw);
                valid = valid && checker.HasNoDataStoreId(listRaw);
                valid = valid && checker.IsNotApprovalStore(listRaw);

                if (valid)
                {
                    listRaw.ForEach(o =>
                    {
                        o.APPROVAL_STORE_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__CHOT;
                    });
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    long time = (Inventec.Common.DateTime.Get.Now() ?? 0);


                    string sql = DAOWorker.SqlDAO.AddInClause(listRaw.Select(s => s.ID).ToList(),
                        "UPDATE HIS_TREATMENT SET APPROVAL_STORE_STT_ID = :param1, MODIFIER = :param2, APPROVAL_LOGINNAME = :param3, APPROVAL_USERNAME = :param4, APPROVAL_TIME = :param5, UNAPPROVAL_LOGINNAME = NULL, UNAPPROVAL_USERNAME = NULL, UNAPPROVAL_TIME = NULL  WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sql, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__CHOT, loginname, loginname, username, time))
                    {
                        throw new Exception("Update HisTreatment that bai");
                    }

                    result = true;
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
