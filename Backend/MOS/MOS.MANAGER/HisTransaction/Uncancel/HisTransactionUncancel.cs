using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBillFund;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisVaccination;

namespace MOS.MANAGER.HisTransaction.Uncancel
{
    public class HisTransactionUncancel : BusinessBase
    {
        private HisSereServDepositUpdate hisSereServDepositUpdate;
        private HisSeseDepoRepayUpdate hisSeseDepoRepayUpdate;
        private HisExpMestUpdate hisExpMestUpdate;
        private HisVaccinationUpdate hisVaccinationUpdate;
        private List<HIS_TRANSACTION> beforeUpdateHisTransactions = new List<HIS_TRANSACTION>();

        internal HisTransactionUncancel()
            : base()
        {
            this.Init();
        }

        internal HisTransactionUncancel(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServDepositUpdate = new HisSereServDepositUpdate(param);
            this.hisSeseDepoRepayUpdate = new HisSeseDepoRepayUpdate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        /// <summary>
        /// Huy giao dich
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionUncancelSDO data, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_CASHIER_ROOM cashierRoom = null;
                HIS_TRANSACTION raw = null;
                List<HIS_TRANSACTION> replaceTransactions = null;
                HIS_TREATMENT hisTreatment = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits = null;
                List<HIS_SERE_SERV_BILL> hisSereServBills = null;
                List<HIS_SESE_DEPO_REPAY> hisSeseDepoRepays = null;
                List<HIS_EXP_MEST> hisExpMests = null;
                List<HIS_SERE_SERV> hisSereServs = null;

                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionUncancelCheck unCancelChecker = new HisTransactionUncancelCheck(param);

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.HasPermission(data.RequestRoomId, ref cashierRoom);
                valid = valid && unCancelChecker.IsAllow(data, cashierRoom, ref raw);
                valid = valid && checker.HasNoReplaceTransaction(raw.ID, ref replaceTransactions); // Check da co thanh toan thay the chua
                valid = valid && unCancelChecker.IsValidBill(raw, ref hisSereServBills, ref hisSereServs);
                valid = valid && unCancelChecker.IsValidDeposit(raw, ref hisSereServDeposits, ref hisSereServs);
                valid = valid && unCancelChecker.IsValidRepay(raw, ref hisSeseDepoRepays);
                valid = valid && unCancelChecker.IsValidExpMest(raw, ref hisExpMests);

                valid = valid && checker.IsNotCollect(raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNotFinancePeriod(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && checker.IsUnlockAccountBook(raw.ACCOUNT_BOOK_ID, ref hisAccountBook);

                //check ho so dieu tri voi cac giao dich gan lien voi ho so dieu tri
                if (raw.TREATMENT_ID.HasValue && !raw.SALE_TYPE_ID.HasValue)
                {
                    if (raw.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO)
                    {
                        valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID.Value, ref hisTreatment);//chi cho cap nhat khi chua duyet khoa tai chinh
                        valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                        valid = valid && treatmentChecker.HasNoHeinApproval(raw.TREATMENT_ID.Value);//chi cho cap nhat khi chua duyet bhyt
                        valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);//chi cho cap nhat khi chua duyet khoa BH
                    }
                }
                
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.ProcessTransaction(raw, ref sqls);
                    this.ProcessSereServBill(hisSereServBills, ref sqls);
                    this.ProcessSereServDeposit(hisSereServDeposits, ref sqls);
                    this.ProcessSeseDepoRepay(hisSeseDepoRepays, ref sqls);
                    this.ProcessExpMest(raw, hisExpMests, ref sqls);

                    if (IsNotNullOrEmpty(sqls) && DAOWorker.SqlDAO.Execute(sqls))
                    {
                        raw.IS_CANCEL = null;
                        raw.CANCEL_REASON = null; 
                        raw.CANCEL_CASHIER_ROOM_ID = null;
                        raw.CANCEL_LOGINNAME = null;
                        raw.CANCEL_USERNAME = null;
                        raw.CANCEL_TIME = null;
                        resultData = raw;
                        result = true;
                        new EventLogGenerator(EventLog.Enum.HisTransaction_KhoiPhucGiaoDich).TransactionCode(raw.TRANSACTION_CODE).Run();
                    }
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

        private void ProcessExpMest(HIS_TRANSACTION transaction, List<HIS_EXP_MEST> hisExpMests, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisExpMests))
            {
                List<long> ids = hisExpMests.Select(o => o.ID).ToList();
                string sql = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_EXP_MEST SET BILL_ID = {0}, CASHIER_LOGINNAME = '{1}', CASHIER_USERNAME = '{1}' WHERE %IN_CLAUSE% ", "ID");
                sql = string.Format(sql, transaction.ID, transaction.CASHIER_LOGINNAME, transaction.CASHIER_USERNAME);
                sqls.Add(sql);
            }
        }

        private void ProcessSeseDepoRepay(List<HIS_SESE_DEPO_REPAY> hisSeseDepoRepays, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisSeseDepoRepays))
            {
                List<long> ids = hisSeseDepoRepays.Select(o => o.ID).ToList();
                string sql = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_SESE_DEPO_REPAY SET IS_CANCEL = NULL WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private void ProcessSereServBill(List<HIS_SERE_SERV_BILL> sereServBills, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(sereServBills))
            {
                List<long> ids = sereServBills.Select(o => o.ID).ToList();
                string sql = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_SERE_SERV_BILL SET IS_CANCEL = NULL WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private void ProcessSereServDeposit(List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(sereServDeposits))
            {
                List<long> ids = sereServDeposits.Select(o => o.ID).ToList();
                string sql = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_SERE_SERV_DEPOSIT SET IS_CANCEL = NULL WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private void ProcessTransaction(HIS_TRANSACTION transaction, ref List<string> sqls)
        {
            string sql = string.Format("UPDATE HIS_TRANSACTION SET IS_CANCEL = NULL, CANCEL_REASON = NULL, CANCEL_CASHIER_ROOM_ID = NULL, CANCEL_LOGINNAME = NULL, CANCEL_USERNAME = NULL, CANCEL_TIME = NULL WHERE ID = {0} ", transaction.ID);
            sqls.Add(sql);
        }
    }
}
