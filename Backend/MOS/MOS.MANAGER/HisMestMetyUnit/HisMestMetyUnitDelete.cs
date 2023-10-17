using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestMetyUnit
{
    partial class HisMestMetyUnitDelete : BusinessBase
    {
        internal HisMestMetyUnitDelete()
            : base()
        {

        }

        internal HisMestMetyUnitDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_METY_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyUnitCheck checker = new HisMestMetyUnitCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_METY_UNIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestMetyUnitDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_METY_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMetyUnitCheck checker = new HisMestMetyUnitCheck(param);
                List<HIS_MEST_METY_UNIT> listRaw = new List<HIS_MEST_METY_UNIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestMetyUnitDAO.DeleteList(listData);
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
