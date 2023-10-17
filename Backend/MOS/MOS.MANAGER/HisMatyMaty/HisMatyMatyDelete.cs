using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMatyMaty
{
    partial class HisMatyMatyDelete : BusinessBase
    {
        internal HisMatyMatyDelete()
            : base()
        {

        }

        internal HisMatyMatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MATY_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMatyMatyCheck checker = new HisMatyMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MATY_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMatyMatyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MATY_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMatyMatyCheck checker = new HisMatyMatyCheck(param);
                List<HIS_MATY_MATY> listRaw = new List<HIS_MATY_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMatyMatyDAO.DeleteList(listData);
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
