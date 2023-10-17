using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMetyProduct
{
    partial class HisMetyProductDelete : BusinessBase
    {
        internal HisMetyProductDelete()
            : base()
        {

        }

        internal HisMetyProductDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_METY_PRODUCT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyProductCheck checker = new HisMetyProductCheck(param);
                valid = valid && IsNotNull(data);
                HIS_METY_PRODUCT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMetyProductDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_METY_PRODUCT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMetyProductCheck checker = new HisMetyProductCheck(param);
                List<HIS_METY_PRODUCT> listRaw = new List<HIS_METY_PRODUCT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMetyProductDAO.DeleteList(listData);
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
