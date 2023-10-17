using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBillGoods
{
    partial class HisBillGoodsDelete : BusinessBase
    {
        internal HisBillGoodsDelete()
            : base()
        {

        }

        internal HisBillGoodsDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BILL_GOODS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBillGoodsCheck checker = new HisBillGoodsCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BILL_GOODS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBillGoodsDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BILL_GOODS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBillGoodsCheck checker = new HisBillGoodsCheck(param);
                List<HIS_BILL_GOODS> listRaw = new List<HIS_BILL_GOODS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBillGoodsDAO.DeleteList(listData);
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
