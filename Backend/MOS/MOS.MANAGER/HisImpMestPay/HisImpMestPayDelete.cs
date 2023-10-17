using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestPay
{
    partial class HisImpMestPayDelete : BusinessBase
    {
        internal HisImpMestPayDelete()
            : base()
        {

        }

        internal HisImpMestPayDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_IMP_MEST_PAY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestPayCheck checker = new HisImpMestPayCheck(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_PAY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestPayDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_IMP_MEST_PAY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestPayCheck checker = new HisImpMestPayCheck(param);
                List<HIS_IMP_MEST_PAY> listRaw = new List<HIS_IMP_MEST_PAY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestPayDAO.DeleteList(listData);
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
