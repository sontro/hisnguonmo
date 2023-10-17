using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskUneiVaty
{
    partial class HisKskUneiVatyDelete : BusinessBase
    {
        internal HisKskUneiVatyDelete()
            : base()
        {

        }

        internal HisKskUneiVatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_UNEI_VATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskUneiVatyCheck checker = new HisKskUneiVatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_UNEI_VATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskUneiVatyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_UNEI_VATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskUneiVatyCheck checker = new HisKskUneiVatyCheck(param);
                List<HIS_KSK_UNEI_VATY> listRaw = new List<HIS_KSK_UNEI_VATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskUneiVatyDAO.DeleteList(listData);
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
