using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowCreate : BusinessBase
    {
        private List<HIS_TREATMENT_BORROW> recentHisTreatmentBorrows = new List<HIS_TREATMENT_BORROW>();

        internal HisTreatmentBorrowCreate()
            : base()
        {

        }

        internal HisTreatmentBorrowCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && treatmentChecker.VerifyId(data.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.HasDataStoreId(treatment);
                valid = valid && checker.CheckHasBorrow(treatment.ID);
                if (valid)
                {
                    data.GIVER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.GIVER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    data.GIVE_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.RECEIVE_TIME = null;
                    data.RECEIVER_LOGINNAME = null;
                    data.RECEIVER_USERNAME = null;
                    if (!DAOWorker.HisTreatmentBorrowDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBorrow_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentBorrow that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentBorrows.Add(data);
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

        internal bool CreateList(List<HIS_TREATMENT_BORROW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentBorrowDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBorrow_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentBorrow that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTreatmentBorrows.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTreatmentBorrows))
            {
                if (!DAOWorker.HisTreatmentBorrowDAO.TruncateList(this.recentHisTreatmentBorrows))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentBorrow that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTreatmentBorrows", this.recentHisTreatmentBorrows));
                }
                this.recentHisTreatmentBorrows = null;
            }
        }
    }
}
