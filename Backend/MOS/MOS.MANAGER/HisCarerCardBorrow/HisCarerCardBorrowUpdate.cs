using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowUpdate : BusinessBase
    {
		private List<HIS_CARER_CARD_BORROW> beforeUpdateHisCarerCardBorrows = new List<HIS_CARER_CARD_BORROW>();
		
        internal HisCarerCardBorrowUpdate()
            : base()
        {

        }

        internal HisCarerCardBorrowUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARER_CARD_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardBorrowCheck checker = new HisCarerCardBorrowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARER_CARD_BORROW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisCarerCardBorrowDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCarerCardBorrow that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisCarerCardBorrows.Add(raw);
                    result = true;
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

        internal bool UpdateList(List<HIS_CARER_CARD_BORROW> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisCarerCardBorrowDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCarerCardBorrow that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisCarerCardBorrows.AddRange(listRaw);
                    result = true;
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisCarerCardBorrows))
            {
                if (!DAOWorker.HisCarerCardBorrowDAO.UpdateList(this.beforeUpdateHisCarerCardBorrows))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCardBorrow that bai, can kiem tra lai." + LogUtil.TraceData("HisCarerCardBorrows", this.beforeUpdateHisCarerCardBorrows));
                }
				this.beforeUpdateHisCarerCardBorrows = null;
            }
        }
    }
}
