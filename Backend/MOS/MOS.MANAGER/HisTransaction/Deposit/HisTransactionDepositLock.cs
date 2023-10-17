using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServDeposit;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Deposit
{
    class HisTransactionDepositLock : BusinessBase
    {
        private HIS_TRANSACTION beforeHisTransaction;

        private HisSereServDepositUpdate hisSereServDepositUpdate;

        internal HisTransactionDepositLock()
            : base()
        {
            this.Init();
        }

        internal HisTransactionDepositLock(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServDepositUpdate = new HisSereServDepositUpdate(param);
        }

        internal bool DepositLock(long id, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsGreaterThanZero(id);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && checker.IsDeposit(raw);
                valid = valid && checker.IsAlowLock(raw);
                if (valid)
                {
                    this.ProcessLockHisTransaction(raw);
                    this.ProcessLockHisSereServDeposit(raw);
                    resultData = raw;
                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_KhoaGiaoDichTamUng).TreatmentCode(raw.TDL_TREATMENT_CODE ?? "").TransactionCode(raw.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessLockHisTransaction(HIS_TRANSACTION data)
        {
            Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
            this.beforeHisTransaction = Mapper.Map<HIS_TRANSACTION>(data);
            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
            data.UNLOCK_TIME = null;
            if (data.BEFORE_UL_CASHIER_ROOM_ID != null)
            {
                data.CASHIER_ROOM_ID = data.BEFORE_UL_CASHIER_ROOM_ID ?? 0;
            }
            if (data.BEFORE_UL_CASHIER_LOGINNAME != null)
            {
                data.CASHIER_LOGINNAME = data.BEFORE_UL_CASHIER_LOGINNAME;
            }
            if (data.BEFORE_UL_CASHIER_USERNAME != null)
            {
                data.CASHIER_USERNAME = data.BEFORE_UL_CASHIER_USERNAME;
            }
            if (!DAOWorker.HisTransactionDAO.Update(data))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                throw new Exception("Ket thuc nghiep vu");
            }
        }

        private void ProcessLockHisSereServDeposit(HIS_TRANSACTION data)
        {
            List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits = new HisSereServDepositGet().GetByDepositId(data.ID);
            if (IsNotNullOrEmpty(hisSereServDeposits))
            {
                Mapper.CreateMap<HIS_SERE_SERV_DEPOSIT, HIS_SERE_SERV_DEPOSIT>();
                List<HIS_SERE_SERV_DEPOSIT> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV_DEPOSIT>>(hisSereServDeposits);
                hisSereServDeposits.ForEach(o => o.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE);
                if (!this.hisSereServDepositUpdate.UpdateList(hisSereServDeposits, beforeUpdates, true))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal bool DepositUnlock(TransactionLockSDO sdo, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TRANSACTION raw = null;
                WorkPlaceSDO workPlace = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && checker.VerifyRequireFieldLockSDO(sdo);
                valid = valid && checker.VerifyId(sdo.TransactionId, ref raw);
                valid = valid && checker.IsLock(raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && checker.IsDeposit(raw);
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                if (valid)
                {
                    this.ProcessUnlockHisTransaction(raw, workPlace);
                    this.ProcessUnlockHisSereServDeposit(raw);
                    resultData = raw;
                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_MoKhoaGiaoDichTamUng).TreatmentCode(raw.TDL_TREATMENT_CODE ?? "").TransactionCode(raw.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessUnlockHisTransaction(HIS_TRANSACTION data, WorkPlaceSDO workPlace)
        {
            Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
            this.beforeHisTransaction = Mapper.Map<HIS_TRANSACTION>(data);
            if (data.BEFORE_UL_CASHIER_LOGINNAME == null)
            {
                data.BEFORE_UL_CASHIER_LOGINNAME = data.CASHIER_LOGINNAME;
                data.BEFORE_UL_CASHIER_ROOM_ID = data.CASHIER_ROOM_ID;
                data.BEFORE_UL_CASHIER_USERNAME = data.CASHIER_USERNAME; 
            }
            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            data.CASHIER_ROOM_ID = workPlace.CashierRoomId ?? 0;
            data.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            data.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            data.UNLOCK_TIME = Inventec.Common.DateTime.Get.Now();
            if (!DAOWorker.HisTransactionDAO.Update(data))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                throw new Exception("Ket thuc nghiep vu");
            }
        }

        private void ProcessUnlockHisSereServDeposit(HIS_TRANSACTION data)
        {
            List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits = new HisSereServDepositGet().GetByDepositId(data.ID);
            if (IsNotNullOrEmpty(hisSereServDeposits))
            {
                Mapper.CreateMap<HIS_SERE_SERV_DEPOSIT, HIS_SERE_SERV_DEPOSIT>();
                List<HIS_SERE_SERV_DEPOSIT> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV_DEPOSIT>>(hisSereServDeposits);
                hisSereServDeposits.ForEach(o => o.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE);
                if (!this.hisSereServDepositUpdate.UpdateList(hisSereServDeposits, beforeUpdates, false))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void RollbackData()
        {
            this.hisSereServDepositUpdate.RollbackData();
            if (this.beforeHisTransaction != null)
            {
                if (!DAOWorker.HisTransactionDAO.Update(this.beforeHisTransaction))
                {
                    LogSystem.Warn("Rollback HIS_TRANSACTION that bai. Kiem tra lai du lieu");
                }
            }
        }
    }
}
