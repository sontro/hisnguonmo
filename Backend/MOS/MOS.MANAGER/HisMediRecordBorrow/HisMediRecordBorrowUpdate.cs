using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    partial class HisMediRecordBorrowUpdate : BusinessBase
    {
        private List<HIS_MEDI_RECORD_BORROW> beforeUpdateHisMediRecordBorrows = new List<HIS_MEDI_RECORD_BORROW>();

        internal HisMediRecordBorrowUpdate()
            : base()
        {

        }

        internal HisMediRecordBorrowUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_RECORD_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordBorrowCheck checker = new HisMediRecordBorrowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_RECORD_BORROW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisMediRecordBorrowDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecordBorrow that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMediRecordBorrows.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDI_RECORD_BORROW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediRecordBorrowCheck checker = new HisMediRecordBorrowCheck(param);
                List<HIS_MEDI_RECORD_BORROW> listRaw = new List<HIS_MEDI_RECORD_BORROW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediRecordBorrowDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecordBorrow that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisMediRecordBorrows.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediRecordBorrows))
            {
                if (!DAOWorker.HisMediRecordBorrowDAO.UpdateList(this.beforeUpdateHisMediRecordBorrows))
                {
                    LogSystem.Warn("Rollback du lieu HisMediRecordBorrow that bai, can kiem tra lai." + LogUtil.TraceData("HisMediRecordBorrows", this.beforeUpdateHisMediRecordBorrows));
                }
                this.beforeUpdateHisMediRecordBorrows = null;
            }
        }
    }
}
