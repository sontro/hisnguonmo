using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoDelete : BusinessBase
    {
        internal HisSevereIllnessInfoDelete()
            : base()
        {

        }

        internal HisSevereIllnessInfoDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SEVERE_ILLNESS_INFO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SEVERE_ILLNESS_INFO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSevereIllnessInfoDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SEVERE_ILLNESS_INFO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                List<HIS_SEVERE_ILLNESS_INFO> listRaw = new List<HIS_SEVERE_ILLNESS_INFO>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSevereIllnessInfoDAO.DeleteList(listData);
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
