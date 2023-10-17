using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUnlimitType
{
    partial class HisUnlimitTypeDelete : BusinessBase
    {
        internal HisUnlimitTypeDelete()
            : base()
        {

        }

        internal HisUnlimitTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_UNLIMIT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUnlimitTypeCheck checker = new HisUnlimitTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_UNLIMIT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisUnlimitTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_UNLIMIT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUnlimitTypeCheck checker = new HisUnlimitTypeCheck(param);
                List<HIS_UNLIMIT_TYPE> listRaw = new List<HIS_UNLIMIT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisUnlimitTypeDAO.DeleteList(listData);
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
