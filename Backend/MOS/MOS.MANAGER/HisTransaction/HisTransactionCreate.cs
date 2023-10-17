using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using System.Linq;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    partial class HisTransactionCreate : BusinessBase
    {
        private HIS_TRANSACTION recentHisTransaction;
        private List<HIS_TRANSACTION> recentHisTransactions = new List<HIS_TRANSACTION>();

        internal HisTransactionCreate()
            : base()
        {
        }

        internal HisTransactionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        public bool Create(HIS_TRANSACTION data, HIS_TREATMENT hisTreatment)
        {
            return this.Create(data, hisTreatment, false);
        }

        public bool Create(HIS_TRANSACTION data, HIS_TREATMENT hisTreatment, bool isAuthorized)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionCheck checker = new HisTransactionCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotCreatingTransaction(hisTreatment);

                if (valid)
                {
                    HisTransactionUtil.SetTdl(data, hisTreatment);

                    //Neu ko phai la giao dich duoc uy quyen --> giao dich do thu ngan tao ra thi lay thong tin thu ngan theo thong tin token
                    if (!isAuthorized)
                    {
                        data.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
                    data.WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                    
                    if (!DAOWorker.HisTransactionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransaction that bai. Du lieu se bi rollback" + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                    }
                    this.recentHisTransaction = data;
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

        public bool CreateList(List<HIS_TRANSACTION> listData, HIS_TREATMENT hisTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    if (valid)
                    {
                        HisTransactionUtil.SetTdl(data, hisTreatment);
                        data.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        data.WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                    }
                }

                if (valid)
                {
                    if (!DAOWorker.HisTransactionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransaction that bai. Du lieu se bi rollback" + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTransactions = listData;
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

        public bool CreateListNotSetCashier(List<HIS_TRANSACTION> listData, HIS_TREATMENT hisTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    if (valid)
                    {
                        HisTransactionUtil.SetTdl(data, hisTreatment);
                    }
                }

                if (valid)
                {
                    if (!DAOWorker.HisTransactionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransaction that bai. Du lieu se bi rollback" + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTransactions = listData;
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

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        internal bool RollbackData()
        {
            bool result = true;
            if (this.recentHisTransaction != null)
            {
                if (!DAOWorker.HisTransactionDAO.Truncate(this.recentHisTransaction))
                {
                    LogSystem.Warn("Rollback thong tin recentHisTransactionDTO that bai. Can kiem tra lai log.");
                    result = false;
                }
                this.recentHisTransaction = null;
            }

            if (IsNotNullOrEmpty(this.recentHisTransactions))
            {
                if (!DAOWorker.HisTransactionDAO.TruncateList(this.recentHisTransactions))
                {
                    LogSystem.Warn("Rollback thong tin recentHisTransactionDTO that bai. Can kiem tra lai log.");
                    result = false;
                }
                this.recentHisTransactions = null;
            }
            return result;
        }
    }
}
