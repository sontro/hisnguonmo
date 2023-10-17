using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServMaty
{
    partial class HisSereServMatyDelete : BusinessBase
    {
        internal HisSereServMatyDelete()
            : base()
        {

        }

        internal HisSereServMatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                HIS_SERE_SERV_MATY raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSereServMatyDAO.Delete(raw);
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

        internal bool DeleteList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                List<HIS_SERE_SERV_MATY> listRaw = new List<HIS_SERE_SERV_MATY>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSereServMatyDAO.DeleteList(listRaw);
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
