using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServDeposit
{
    partial class HisSereServDepositDelete : BusinessBase
    {
        internal HisSereServDepositDelete()
            : base()
        {

        }

        internal HisSereServDepositDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERE_SERV_DEPOSIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_DEPOSIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSereServDepositDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_SERE_SERV_DEPOSIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServDepositCheck checker = new HisSereServDepositCheck(param);
                List<HIS_SERE_SERV_DEPOSIT> listRaw = new List<HIS_SERE_SERV_DEPOSIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSereServDepositDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
