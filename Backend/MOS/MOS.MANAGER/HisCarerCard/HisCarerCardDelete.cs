using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCarerCard
{
    partial class HisCarerCardDelete : BusinessBase
    {
        internal HisCarerCardDelete()
            : base()
        {

        }

        internal HisCarerCardDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CARER_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCarerCardDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CARER_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCarerCardCheck checker = new HisCarerCardCheck(param);
                List<HIS_CARER_CARD> listRaw = new List<HIS_CARER_CARD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCarerCardDAO.DeleteList(listData);
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
