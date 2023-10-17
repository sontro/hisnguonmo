using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebtGoods
{
    partial class HisDebtGoodsDelete : BusinessBase
    {
        internal HisDebtGoodsDelete()
            : base()
        {

        }

        internal HisDebtGoodsDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DEBT_GOODS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebtGoodsCheck checker = new HisDebtGoodsCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEBT_GOODS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDebtGoodsDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DEBT_GOODS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebtGoodsCheck checker = new HisDebtGoodsCheck(param);
                List<HIS_DEBT_GOODS> listRaw = new List<HIS_DEBT_GOODS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDebtGoodsDAO.DeleteList(listData);
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
