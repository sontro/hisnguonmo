using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowUpdate : BusinessBase
    {
        private List<HIS_TREATMENT_BORROW> beforeUpdateHisTreatmentBorrows = new List<HIS_TREATMENT_BORROW>();

        internal HisTreatmentBorrowUpdate()
            : base()
        {

        }

        internal HisTreatmentBorrowUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_BORROW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentBorrowDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentBorrow that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisTreatmentBorrows.Add(raw);
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

        internal bool UpdateList(List<HIS_TREATMENT_BORROW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                List<HIS_TREATMENT_BORROW> listRaw = new List<HIS_TREATMENT_BORROW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentBorrowDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentBorrow that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisTreatmentBorrows.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentBorrows))
            {
                if (!DAOWorker.HisTreatmentBorrowDAO.UpdateList(this.beforeUpdateHisTreatmentBorrows))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentBorrow that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentBorrows", this.beforeUpdateHisTreatmentBorrows));
                }
                this.beforeUpdateHisTreatmentBorrows = null;
            }
        }
    }
}
