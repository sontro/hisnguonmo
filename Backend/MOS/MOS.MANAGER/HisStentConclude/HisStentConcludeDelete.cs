using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeDelete : BusinessBase
    {
        internal HisStentConcludeDelete()
            : base()
        {

        }

        internal HisStentConcludeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_STENT_CONCLUDE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_STENT_CONCLUDE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisStentConcludeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_STENT_CONCLUDE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                List<HIS_STENT_CONCLUDE> listRaw = new List<HIS_STENT_CONCLUDE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisStentConcludeDAO.DeleteList(listData);
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
