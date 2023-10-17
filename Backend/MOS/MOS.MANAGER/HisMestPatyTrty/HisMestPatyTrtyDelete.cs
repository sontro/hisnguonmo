using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPatyTrty
{
    partial class HisMestPatyTrtyDelete : BusinessBase
    {
        internal HisMestPatyTrtyDelete()
            : base()
        {

        }

        internal HisMestPatyTrtyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_PATY_TRTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatyTrtyCheck checker = new HisMestPatyTrtyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_TRTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestPatyTrtyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_PATY_TRTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatyTrtyCheck checker = new HisMestPatyTrtyCheck(param);
                List<HIS_MEST_PATY_TRTY> listRaw = new List<HIS_MEST_PATY_TRTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestPatyTrtyDAO.DeleteList(listData);
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
