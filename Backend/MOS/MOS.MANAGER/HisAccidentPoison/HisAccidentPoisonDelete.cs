using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentPoison
{
    partial class HisAccidentPoisonDelete : BusinessBase
    {
        internal HisAccidentPoisonDelete()
            : base()
        {

        }

        internal HisAccidentPoisonDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ACCIDENT_POISON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentPoisonCheck checker = new HisAccidentPoisonCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_POISON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentPoisonDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ACCIDENT_POISON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentPoisonCheck checker = new HisAccidentPoisonCheck(param);
                List<HIS_ACCIDENT_POISON> listRaw = new List<HIS_ACCIDENT_POISON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentPoisonDAO.DeleteList(listData);
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
