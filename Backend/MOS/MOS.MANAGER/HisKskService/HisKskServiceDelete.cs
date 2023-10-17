using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceDelete : BusinessBase
    {
        internal HisKskServiceDelete()
            : base()
        {

        }

        internal HisKskServiceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskServiceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                List<HIS_KSK_SERVICE> listRaw = new List<HIS_KSK_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskServiceDAO.DeleteList(listData);
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
