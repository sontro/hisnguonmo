using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceSame
{
    partial class HisServiceSameDelete : BusinessBase
    {
        internal HisServiceSameDelete()
            : base()
        {

        }

        internal HisServiceSameDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_SAME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceSameCheck checker = new HisServiceSameCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_SAME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceSameDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_SAME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceSameCheck checker = new HisServiceSameCheck(param);
                List<HIS_SERVICE_SAME> listRaw = new List<HIS_SERVICE_SAME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceSameDAO.DeleteList(listData);
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
