using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisOtherPaySource
{
    partial class HisOtherPaySourceDelete : BusinessBase
    {
        internal HisOtherPaySourceDelete()
            : base()
        {

        }

        internal HisOtherPaySourceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_OTHER_PAY_SOURCE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisOtherPaySourceCheck checker = new HisOtherPaySourceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_OTHER_PAY_SOURCE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisOtherPaySourceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_OTHER_PAY_SOURCE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisOtherPaySourceCheck checker = new HisOtherPaySourceCheck(param);
                List<HIS_OTHER_PAY_SOURCE> listRaw = new List<HIS_OTHER_PAY_SOURCE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisOtherPaySourceDAO.DeleteList(listData);
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
