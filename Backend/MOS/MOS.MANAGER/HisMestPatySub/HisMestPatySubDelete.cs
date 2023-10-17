using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPatySub
{
    partial class HisMestPatySubDelete : BusinessBase
    {
        internal HisMestPatySubDelete()
            : base()
        {

        }

        internal HisMestPatySubDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_PATY_SUB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPatySubCheck checker = new HisMestPatySubCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_SUB raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestPatySubDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_PATY_SUB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPatySubCheck checker = new HisMestPatySubCheck(param);
                List<HIS_MEST_PATY_SUB> listRaw = new List<HIS_MEST_PATY_SUB>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestPatySubDAO.DeleteList(listData);
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
