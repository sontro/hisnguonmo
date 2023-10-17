using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransaction
{
    partial class HisTransactionUpdate : BusinessBase
    {
        private List<HIS_TRANSACTION> beforeUpdateHisTransactions = new List<HIS_TRANSACTION>();

        internal HisTransactionUpdate()
            : base()
        {

        }

        internal HisTransactionUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRANSACTION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HIS_TREATMENT hisTreatment = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNoNationalCode(raw);

                //check ho so dieu tri voi cac giao dich gan lien voi ho so dieu tri
                if (data.TREATMENT_ID.HasValue)
                {
                    valid = valid && treatmentChecker.IsUnLock(data.TREATMENT_ID.Value, ref hisTreatment);//chi cho cap nhat khi chua duyet khoa tai chinh
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);//chi cho cap nhat khi chua duyet khoa BH
                    valid = valid && checker.IsUnlockAccountBook(data.ACCOUNT_BOOK_ID, ref hisAccountBook);
                    valid = valid && checker.IsValidNumOrder(data, hisAccountBook);
                }

                if (valid)
                {
                    this.beforeUpdateHisTransactions.Add(raw);
                    if (!DAOWorker.HisTransactionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("data", data));
                    }

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

        internal bool UpdateList(List<HIS_TRANSACTION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionCheck checker = new HisTransactionCheck(param);
                List<HIS_TRANSACTION> listRaw = new List<HIS_TRANSACTION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisTransactions.AddRange(listRaw);
                    if (!DAOWorker.HisTransactionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("listData", listData));
                    }
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

        internal bool UpdateList(List<HIS_TRANSACTION> listData, List<HIS_TRANSACTION> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisTransactions.AddRange(beforeUpdates);
                    if (!DAOWorker.HisTransactionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("listData", listData));
                    }
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

        internal bool UpdateListWithoutCheckLock(List<HIS_TRANSACTION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionCheck checker = new HisTransactionCheck(param);
                List<HIS_TRANSACTION> listRaw = new List<HIS_TRANSACTION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisTransactions.AddRange(listRaw);
                    if (!DAOWorker.HisTransactionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("listData", listData));
                    }
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

        internal bool UpdateListWithoutCheckLock(List<HIS_TRANSACTION> listData, List<HIS_TRANSACTION> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionCheck checker = new HisTransactionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisTransactions.AddRange(befores);
                    if (!DAOWorker.HisTransactionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("listData", listData));
                    }
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

        internal bool UpdateWithoutCheckLock(HIS_TRANSACTION data, HIS_TRANSACTION before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNull(data);
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisTransactionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisTransactions.Add(before);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransactions))
            {
                if (!new HisTransactionUpdate(param).UpdateList(this.beforeUpdateHisTransactions))
                {
                    LogSystem.Warn("Rollback du lieu HisTransaction that bai, can kiem tra lai." + LogUtil.TraceData("HisTransactions", this.beforeUpdateHisTransactions));
                }
            }
        }
    }
}
