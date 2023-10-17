using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareTempDetail
{
    partial class HisCareTempDetailDelete : BusinessBase
    {
        internal HisCareTempDetailDelete()
            : base()
        {

        }

        internal HisCareTempDetailDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CARE_TEMP_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTempDetailCheck checker = new HisCareTempDetailCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TEMP_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCareTempDetailDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CARE_TEMP_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareTempDetailCheck checker = new HisCareTempDetailCheck(param);
                List<HIS_CARE_TEMP_DETAIL> listRaw = new List<HIS_CARE_TEMP_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCareTempDetailDAO.DeleteList(listData);
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
