using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisIcdService
{
    partial class HisIcdServiceDelete : BusinessBase
    {
        internal HisIcdServiceDelete()
            : base()
        {

        }

        internal HisIcdServiceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ICD_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdServiceCheck checker = new HisIcdServiceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ICD_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisIcdServiceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ICD_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisIcdServiceCheck checker = new HisIcdServiceCheck(param);
                List<HIS_ICD_SERVICE> listRaw = new List<HIS_ICD_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisIcdServiceDAO.DeleteList(listData);
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
