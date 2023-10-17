using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverDelete : BusinessBase
    {
        internal HisKskDriverDelete()
            : base()
        {

        }

        internal HisKskDriverDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_DRIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_DRIVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskDriverDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_DRIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                List<HIS_KSK_DRIVER> listRaw = new List<HIS_KSK_DRIVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskDriverDAO.DeleteList(listData);
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
