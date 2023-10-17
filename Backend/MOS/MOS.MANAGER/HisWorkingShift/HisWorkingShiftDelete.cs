using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWorkingShift
{
    partial class HisWorkingShiftDelete : BusinessBase
    {
        internal HisWorkingShiftDelete()
            : base()
        {

        }

        internal HisWorkingShiftDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_WORKING_SHIFT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                valid = valid && IsNotNull(data);
                HIS_WORKING_SHIFT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisWorkingShiftDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_WORKING_SHIFT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                List<HIS_WORKING_SHIFT> listRaw = new List<HIS_WORKING_SHIFT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisWorkingShiftDAO.DeleteList(listData);
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
