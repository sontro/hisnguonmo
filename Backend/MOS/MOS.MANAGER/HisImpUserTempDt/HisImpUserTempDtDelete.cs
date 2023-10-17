using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtDelete : BusinessBase
    {
        internal HisImpUserTempDtDelete()
            : base()
        {

        }

        internal HisImpUserTempDtDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_IMP_USER_TEMP_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_USER_TEMP_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisImpUserTempDtDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_IMP_USER_TEMP_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                List<HIS_IMP_USER_TEMP_DT> listRaw = new List<HIS_IMP_USER_TEMP_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisImpUserTempDtDAO.DeleteList(listData);
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
