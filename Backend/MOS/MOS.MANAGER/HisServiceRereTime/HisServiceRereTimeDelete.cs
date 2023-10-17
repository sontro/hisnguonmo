using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRereTime
{
    partial class HisServiceRereTimeDelete : BusinessBase
    {
        internal HisServiceRereTimeDelete()
            : base()
        {

        }

        internal HisServiceRereTimeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_RERE_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRereTimeCheck checker = new HisServiceRereTimeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RERE_TIME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceRereTimeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_RERE_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRereTimeCheck checker = new HisServiceRereTimeCheck(param);
                List<HIS_SERVICE_RERE_TIME> listRaw = new List<HIS_SERVICE_RERE_TIME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceRereTimeDAO.DeleteList(listData);
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
