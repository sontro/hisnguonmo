using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDocHoldType
{
    partial class HisDocHoldTypeDelete : BusinessBase
    {
        internal HisDocHoldTypeDelete()
            : base()
        {

        }

        internal HisDocHoldTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DOC_HOLD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDocHoldTypeCheck checker = new HisDocHoldTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DOC_HOLD_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDocHoldTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DOC_HOLD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDocHoldTypeCheck checker = new HisDocHoldTypeCheck(param);
                List<HIS_DOC_HOLD_TYPE> listRaw = new List<HIS_DOC_HOLD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDocHoldTypeDAO.DeleteList(listData);
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
