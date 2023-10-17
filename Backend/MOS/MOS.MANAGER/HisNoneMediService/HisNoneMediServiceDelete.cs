using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNoneMediService
{
    partial class HisNoneMediServiceDelete : BusinessBase
    {
        internal HisNoneMediServiceDelete()
            : base()
        {

        }

        internal HisNoneMediServiceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_NONE_MEDI_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNoneMediServiceCheck checker = new HisNoneMediServiceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_NONE_MEDI_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisNoneMediServiceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_NONE_MEDI_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNoneMediServiceCheck checker = new HisNoneMediServiceCheck(param);
                List<HIS_NONE_MEDI_SERVICE> listRaw = new List<HIS_NONE_MEDI_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisNoneMediServiceDAO.DeleteList(listData);
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
