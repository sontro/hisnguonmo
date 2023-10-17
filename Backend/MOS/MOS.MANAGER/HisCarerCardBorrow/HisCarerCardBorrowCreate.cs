using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowCreate : BusinessBase
    {
		private List<HIS_CARER_CARD_BORROW> recentHisCarerCardBorrows = new List<HIS_CARER_CARD_BORROW>();
		
        internal HisCarerCardBorrowCreate()
            : base()
        {

        }

        internal HisCarerCardBorrowCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARER_CARD_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCarerCardBorrowCheck checker = new HisCarerCardBorrowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisCarerCardBorrowDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCarerCardBorrow that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCarerCardBorrows.Add(data);
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
		
		internal bool CreateList(List<HIS_CARER_CARD_BORROW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCarerCardBorrowCheck checker = new HisCarerCardBorrowCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCarerCardBorrowDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCarerCardBorrow that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCarerCardBorrows.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCarerCardBorrows))
            {
                if (!DAOWorker.HisCarerCardBorrowDAO.TruncateList(this.recentHisCarerCardBorrows))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCardBorrow that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCarerCardBorrows", this.recentHisCarerCardBorrows));
                }
				this.recentHisCarerCardBorrows = null;
            }
        }
    }
}
