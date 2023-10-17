using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    partial class HisMediRecordBorrowCreate : BusinessBase
    {
		private List<HIS_MEDI_RECORD_BORROW> recentHisMediRecordBorrows = new List<HIS_MEDI_RECORD_BORROW>();
		
        internal HisMediRecordBorrowCreate()
            : base()
        {

        }

        internal HisMediRecordBorrowCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_RECORD_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordBorrowCheck checker = new HisMediRecordBorrowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDI_RECORD_BORROW_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMediRecordBorrowDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordBorrow_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediRecordBorrow that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediRecordBorrows.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMediRecordBorrows))
            {
                if (!DAOWorker.HisMediRecordBorrowDAO.TruncateList(this.recentHisMediRecordBorrows))
                {
                    LogSystem.Warn("Rollback du lieu HisMediRecordBorrow that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediRecordBorrows", this.recentHisMediRecordBorrows));
                }
				this.recentHisMediRecordBorrows = null;
            }
        }
    }
}
