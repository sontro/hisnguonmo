using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBillFund;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisVaccination;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Truncate
{
    class HisTransactionTruncate : BusinessBase
    {
        internal HisTransactionTruncate()
            : base()
        {

        }

        internal HisTransactionTruncate(CommonParam paramDelete)
            : base(paramDelete)
        {
            
        }

        /// <summary>
        /// Huy giao dich
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionDeleteSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_CASHIER_ROOM cashierRoom = null;
                HIS_TRANSACTION raw = null;
                HIS_TREATMENT hisTreatment = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits = null;
                HisTransactionCheck commonChecker = new HisTransactionCheck(param);
                HisTransactionTruncateCheck checker = new HisTransactionTruncateCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.HasPermission(data.RequestRoomId, ref cashierRoom);
                valid = valid && commonChecker.VerifyId(data.TransactionId, ref raw);
                //valid = valid && checker.IsCreator(raw);
                valid = valid && commonChecker.IsNotCollect(raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && commonChecker.IsUnCancel(raw);
                valid = valid && commonChecker.HasNotFinancePeriod(raw);
                valid = valid && commonChecker.HasNoNationalCode(raw);
                valid = valid && commonChecker.HasNoDeptId(raw);
                valid = valid && commonChecker.IsNotHasTigCode(raw);
                valid = valid && commonChecker.IsUnlockAccountBook(raw.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && commonChecker.IsNotGenTransactionNumOrder(hisAccountBook);

                //check ho so dieu tri voi cac giao dich gan lien voi ho so dieu tri
                if (raw.TREATMENT_ID.HasValue && !raw.SALE_TYPE_ID.HasValue)
                {
                    if (raw.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO)
                    {
                        valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID.Value, ref hisTreatment);//chi cho cap nhat khi chua duyet khoa tai chinh
                        valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                        valid = valid && treatmentChecker.HasNoHeinApproval(raw.TREATMENT_ID.Value);//chi cho cap nhat khi chua duyet bhyt
                        valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);//chi cho cap nhat khi chua duyet khoa BH
                        valid = valid && commonChecker.IsAlowCancelDeposit(raw, ref hisSereServDeposits);
                    }
                }

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.ProcessBillFund(raw, ref sqls);
                    this.ProcessSereServBill(raw, ref sqls);
                    this.ProcessSereServDeposit(raw, hisSereServDeposits, ref sqls);
                    this.ProcessSereServDept(raw, ref sqls);
                    this.ProcessDeptGoods(raw, ref sqls);
                    this.ProcessDebt(raw, ref sqls);
                    this.ProcessSeseDepoRepay(raw, ref sqls);
                    this.ProcessSaleExpMest(raw, ref sqls);
                    this.ProcessVaccination(raw, ref sqls);
                    this.ProcessDepositReq(raw, ref sqls);
                    this.ProcessBillGoods(raw, ref sqls);
                    this.ProcessTransaction(raw, ref sqls);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls faild. sqls: " + sqls.ToString());
                    }

                    result = true;
                    HisTransactionLog.LogDelete(data, raw, hisAccountBook, LibraryEventLog.EventLog.Enum.HisTransaction_XoaGiaoDich);
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

        private void ProcessDebt(HIS_TRANSACTION raw, ref List<string> sqls)
        {
            if (raw.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT
                && raw.IS_DEBT_COLLECTION == Constant.IS_TRUE)
            {
                List<HIS_TRANSACTION> debts = new HisTransactionGet().GetByDebtBillId(raw.ID);
                if (IsNotNullOrEmpty(debts))
                {
                    HisTransactionCheck checker = new HisTransactionCheck(param);
                    if (!checker.IsUnLock(debts))
                    {
                        throw new Exception("debts islock");
                    }
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(debts.Select(s => s.ID).ToList(), "UPDATE HIS_TRANSACTION SET DEBT_BILL_ID = NULL WHERE %IN_CLAUSE%", "ID"));
                }
            }
        }

        private void ProcessTransaction(HIS_TRANSACTION raw, ref List<string> sqls)
        {
            sqls.Add(String.Format("DELETE HIS_TRANSACTION WHERE ID = {0}", raw.ID));
        }

        private void ProcessBillFund(HIS_TRANSACTION raw, ref List<string> sqls)
        {
            if (raw.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
            {
                HisBillFundFilterQuery fundFilter = new HisBillFundFilterQuery();
                fundFilter.BILL_ID = raw.ID;
                List<HIS_BILL_FUND> billFunds = new HisBillFundGet().Get(fundFilter);
                if (IsNotNullOrEmpty(billFunds))
                {
                    HisBillFundCheck fundChecker = new HisBillFundCheck(param);
                    if (!fundChecker.IsUnLock(billFunds))
                    {
                        throw new Exception("billFunds is lock");
                    }
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(billFunds.Select(s => s.ID).ToList(), "DELETE HIS_BILL_FUND WHERE %IN_CLAUSE%", "ID"));
                }
            }
        }

        private void ProcessSereServBill(HIS_TRANSACTION data, ref List<string> sqls)
        {
            //Lay ra cac sereServ co billId
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT
                || !data.TREATMENT_ID.HasValue)
            {
                return;
            }
            List<V_HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetViewByBillId(data.ID);

            if (IsNotNullOrEmpty(sereServBills))
            {
                //Kiem tra xem BN co don nao da linh thuoc va chua thu hoi het hay ko
                List<long> serviceReqIds = sereServBills
                        .Where(o => o.SERVICE_REQ_ID.HasValue && o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK && o.AMOUNT > 0)
                        .Select(o => o.SERVICE_REQ_ID.Value)
                        .ToList();

                //Kiem tra neu don phong kham da thuc xuat thi khong cho phep huy thanh toan
                if (IsNotNullOrEmpty(serviceReqIds))
                {
                    HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                    filter.SERVICE_REQ_IDs = serviceReqIds;
                    filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);

                    if (IsNotNullOrEmpty(expMests))
                    {
                        List<string> expMestCodes = expMests.Select(o => o.EXP_MEST_CODE).ToList();
                        string expMestCodeStr = string.Join(",", expMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_BenhNhanDaLinhThuoc, expMestCodeStr);
                        throw new Exception("Benh nhan da linh thuoc, khong cho phep huy hoa don neu chua thu hoi het");
                    }
                }

                if (!new HisSereServCheck(param).HasNoInvoice(sereServBills.Select(s => s.SERE_SERV_ID).ToList()))
                {
                    throw new Exception("Rollback du lieu. ket thuc nghiep vu");
                }


                Mapper.CreateMap<V_HIS_SERE_SERV_BILL, HIS_SERE_SERV_BILL>();
                List<HIS_SERE_SERV_BILL> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV_BILL>>(sereServBills);

                if (!new HisSereServBillCheck(param).IsUnLock(beforeUpdates))
                {
                    throw new Exception("HisSereServBillCheck.IsUnLock. ket thuc nghiep vu");
                }
                string sql = String.Format("DELETE HIS_SERE_SERV_BILL WHERE BILL_ID = {0}", data.ID);
                sqls.Add(sql);
            }
        }

        private void ProcessSereServDeposit(HIS_TRANSACTION data, List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU || !IsNotNullOrEmpty(sereServDeposits))
                return;

            HisSereServDepositCheck ssDepositChecker = new HisSereServDepositCheck(param);
            if (!ssDepositChecker.IsUnLock(sereServDeposits))
            {
                throw new Exception("HisSereServDepositCheck.IsUnLock");
            }
            sqls.Add(DAOWorker.SqlDAO.AddInClause(sereServDeposits.Select(s => s.ID).ToList(), "DELETE HIS_SERE_SERV_DEPOSIT WHERE %IN_CLAUSE%", "ID"));
        }

        private void ProcessSeseDepoRepay(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU)
                return;
            List<HIS_SESE_DEPO_REPAY> seseDepoRepays = new HisSeseDepoRepayGet().GetByRepayId(data.ID);
            if (IsNotNullOrEmpty(seseDepoRepays))
            {
                //get view de lay sereServId check trang thang dang thuc hien
                List<V_HIS_SESE_DEPO_REPAY> views = new HisSeseDepoRepayGet().GetViewByRepayId(data.ID);
                List<long> sereServIds = views.Select(s => s.SERE_SERV_ID).Distinct().ToList();
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet(param).GetByIds(sereServIds);

                //Neu dich vu o trang thai khong thuc hien thi khong cho huy hoan ung, nguoi dung phai tich thuc hien thi moi cho phep huy
                if (!new HisSereServCheck().HasExecute(hisSereServs))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuKhongThucHienKhongChoPhepHuyHoanUng);
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                HisSeseDepoRepayCheck ssDepoRepayChecker = new HisSeseDepoRepayCheck(param);
                if (!ssDepoRepayChecker.IsUnLock(seseDepoRepays))
                {
                    throw new Exception("HisSereServDepositCheck.IsUnLock");
                }
                sqls.Add(DAOWorker.SqlDAO.AddInClause(seseDepoRepays.Select(s => s.ID).ToList(), "DELETE HIS_SESE_DEPO_REPAY WHERE %IN_CLAUSE%", "ID"));
            }
        }

        private void ProcessSereServDept(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO
                && (data.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__SERVICE || data.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__TREAT))
            {
                sqls.Add(string.Format("DELETE HIS_SERE_SERV_DEBT WHERE DEBT_ID = {0}", data.ID));
            }
        }

        private void ProcessDeptGoods(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && data.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__DRUG_STORE)
            {
                sqls.Add(string.Format("DELETE HIS_DEBT_GOODS WHERE DEBT_ID = {0}", data.ID));
            }
        }

        private void ProcessSaleExpMest(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT
                && data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP)
            {
                List<HIS_EXP_MEST> saleExpMests = new HisExpMestGet().GetByBillId(data.ID);
                if (IsNotNullOrEmpty(saleExpMests))
                {
                    if (HisExpMestCFG.EXPORT_SALE_MUST_BILL)
                    {
                        List<string> codes = saleExpMests.Where(e => e.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE).Select(s => s.EXP_MEST_CODE).ToList();

                        if (IsNotNullOrEmpty(codes))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_PhieuXuatBanChuaHuyThucXuat, String.Join(",", codes));
                            throw new Exception("Phieu xuat ban chua xoa thuc xuat. Khong cho phep xoa giao dich");
                        }
                    }

                    HisExpMestCheck expMestChecker = new HisExpMestCheck(param);
                    if (!expMestChecker.IsUnlock(saleExpMests))
                    {
                        throw new Exception("HisExpMestCheck.IsUnLock");
                    }
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(saleExpMests.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST SET BILL_ID = NULL WHERE %IN_CLAUSE%", "ID"));
                }
                else
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Xoa giao dich thanh toan xuat ban, nhung khong lay duoc phieu xuat ban tuong ung");
                }
            }
            else if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && data.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__DRUG_STORE)
            {
                sqls.Add(string.Format("UPDATE HIS_EXP_MEST SET DEBT_ID = NULL WHERE DEBT_ID = {0}", data.ID));
            }
        }

        private void ProcessVaccination(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT
                && data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_VACCIN)
            {
                List<HIS_VACCINATION> vaccinations = new HisVaccinationGet().GetByBillId(data.ID);
                if (IsNotNullOrEmpty(vaccinations))
                {
                    HisVaccinationCheck vaccChecker = new HisVaccinationCheck(param);
                    if (!vaccChecker.IsUnLock(vaccinations))
                    {
                        throw new Exception("HisVaccinationCheck.IsUnLock");
                    }
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(vaccinations.Select(s => s.ID).ToList(), "UPDATE HIS_VACCINATION SET BILL_ID = NULL WHERE %IN_CLAUSE%", "ID"));
                }
                else
                {
                    LogSystem.Warn("Xoa giao dich thanh toan vaccin, nhung khong lay duoc yeu cau tiem vaccin nao tuong ung");
                }
            }
        }

        private void ProcessBillGoods(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT
                && (data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_OTHER || data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP))
            {
                string sql = string.Format("DELETE FROM HIS_BILL_GOODS WHERE BILL_ID = {0} ", data.ID);
                sqls.Add(sql);
            }
        }

        private void ProcessDepositReq(HIS_TRANSACTION data, ref List<string> sqls)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                return;
            List<HIS_DEPOSIT_REQ> depositReqs = new HisDepositReqGet().GetByDepositId(data.ID);
            if (IsNotNullOrEmpty(depositReqs))
            {
                HisDepositReqCheck depositReqChecker = new HisDepositReqCheck(param);
                if (!depositReqChecker.IsUnLock(depositReqs))
                {
                    throw new Exception("HisDepositReqCheck.IsUnLock");
                }
                sqls.Add(DAOWorker.SqlDAO.AddInClause(depositReqs.Select(s => s.ID).ToList(), "UPDATE HIS_DEPOSIT_REQ SET DEPOSIT_ID = NULL WHERE %IN_CLAUSE%", "ID"));
            }
        }

    }
}
