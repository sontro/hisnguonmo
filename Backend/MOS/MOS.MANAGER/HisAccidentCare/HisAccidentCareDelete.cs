using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentCare
{
    partial class HisAccidentCareDelete : BusinessBase
    {
        internal HisAccidentCareDelete()
            : base()
        {

        }

        internal HisAccidentCareDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ACCIDENT_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentCareCheck checker = new HisAccidentCareCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_CARE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentCareDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ACCIDENT_CARE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentCareCheck checker = new HisAccidentCareCheck(param);
                List<HIS_ACCIDENT_CARE> listRaw = new List<HIS_ACCIDENT_CARE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentCareDAO.DeleteList(listData);
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
