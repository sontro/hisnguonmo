using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowDelete : BusinessBase
    {
        internal HisCarerCardBorrowDelete()
            : base()
        {

        }

        internal HisCarerCardBorrowDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CARER_CARD_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardBorrowCheck checker = new HisCarerCardBorrowCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD_BORROW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCarerCardBorrowDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CARER_CARD_BORROW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCarerCardBorrowCheck checker = new HisCarerCardBorrowCheck(param);
                List<HIS_CARER_CARD_BORROW> listRaw = new List<HIS_CARER_CARD_BORROW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCarerCardBorrowDAO.DeleteList(listData);
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
