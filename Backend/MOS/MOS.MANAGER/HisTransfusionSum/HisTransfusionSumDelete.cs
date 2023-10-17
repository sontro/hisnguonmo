using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransfusionSum
{
    partial class HisTransfusionSumDelete : BusinessBase
    {
        internal HisTransfusionSumDelete()
            : base()
        {

        }

        internal HisTransfusionSumDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TRANSFUSION_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSFUSION_SUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTransfusionSumDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TRANSFUSION_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
                List<HIS_TRANSFUSION_SUM> listRaw = new List<HIS_TRANSFUSION_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTransfusionSumDAO.DeleteList(listData);
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
