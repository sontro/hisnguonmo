using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDispenseType
{
    partial class HisDispenseTypeDelete : BusinessBase
    {
        internal HisDispenseTypeDelete()
            : base()
        {

        }

        internal HisDispenseTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DISPENSE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDispenseTypeCheck checker = new HisDispenseTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DISPENSE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDispenseTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DISPENSE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDispenseTypeCheck checker = new HisDispenseTypeCheck(param);
                List<HIS_DISPENSE_TYPE> listRaw = new List<HIS_DISPENSE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDispenseTypeDAO.DeleteList(listData);
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
