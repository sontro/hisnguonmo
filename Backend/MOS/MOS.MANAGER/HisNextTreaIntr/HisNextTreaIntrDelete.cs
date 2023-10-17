using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNextTreaIntr
{
    partial class HisNextTreaIntrDelete : BusinessBase
    {
        internal HisNextTreaIntrDelete()
            : base()
        {

        }

        internal HisNextTreaIntrDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_NEXT_TREA_INTR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNextTreaIntrCheck checker = new HisNextTreaIntrCheck(param);
                valid = valid && IsNotNull(data);
                HIS_NEXT_TREA_INTR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisNextTreaIntrDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_NEXT_TREA_INTR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNextTreaIntrCheck checker = new HisNextTreaIntrCheck(param);
                List<HIS_NEXT_TREA_INTR> listRaw = new List<HIS_NEXT_TREA_INTR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisNextTreaIntrDAO.DeleteList(listData);
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
