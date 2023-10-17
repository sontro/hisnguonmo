using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestBlood
{
    partial class HisImpMestBloodDelete : BusinessBase
    {
        internal HisImpMestBloodDelete()
            : base()
        {

        }

        internal HisImpMestBloodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_IMP_MEST_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_BLOOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestBloodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_IMP_MEST_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                List<HIS_IMP_MEST_BLOOD> listRaw = new List<HIS_IMP_MEST_BLOOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisImpMestBloodDAO.DeleteList(listData);
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
