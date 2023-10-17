using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPaanLiquid
{
    partial class HisPaanLiquidDelete : BusinessBase
    {
        internal HisPaanLiquidDelete()
            : base()
        {

        }

        internal HisPaanLiquidDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PAAN_LIQUID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPaanLiquidCheck checker = new HisPaanLiquidCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PAAN_LIQUID raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPaanLiquidDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PAAN_LIQUID> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPaanLiquidCheck checker = new HisPaanLiquidCheck(param);
                List<HIS_PAAN_LIQUID> listRaw = new List<HIS_PAAN_LIQUID>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPaanLiquidDAO.DeleteList(listData);
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
