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
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System.Configuration;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisCancelReason;
using YTT.SDO;
using MOS.MANAGER.HisTransaction.Cancel.Epayment;

namespace MOS.MANAGER.HisTransaction.Cancel
{
    public class HisTransactionCancel : BusinessBase
    {
        private HisSereServDepositUpdate hisSereServDepositUpdate;
        private HisSeseDepoRepayUpdate hisSeseDepoRepayUpdate;
        private HisExpMestUpdate hisExpMestUpdate;
        private HisVaccinationUpdate hisVaccinationUpdate;
        private HisTransactionUpdate hisTransactionDebtUpdate;
        private HisSereServDebtUpdate hisSereServDebtUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        private List<HIS_TRANSACTION> beforeUpdateHisTransactions = new List<HIS_TRANSACTION>();

        internal HisTransactionCancel()
            : base()
        {
            this.Init();
        }

        internal HisTransactionCancel(CommonParam paramDelete)
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
            this.hisTransactionDebtUpdate = new HisTransactionUpdate(param);
            this.hisSereServDebtUpdate = new HisSereServDebtUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        /// <summary>
        /// Huy giao dich
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Cancel(HisTransactionCancelSDO data, ref HIS_TRANSACTION resultData)
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
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionCancelCheck cancelChecker = new HisTransactionCancelCheck(param);

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && cancelChecker.VerifyRequireField(data);
                valid = valid && cancelChecker.HasPermission(data.RequestRoomId, ref cashierRoom);
                valid = valid && checker.VerifyId(data.TransactionId, ref raw);
                valid = valid && checker.IsNotCollect(raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && checker.HasNotFinancePeriod(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && cancelChecker.IsValidTime(data.CancelTime, raw.TRANSACTION_TIME);
                valid = valid && checker.HasNoDeptId(raw);
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
                        valid = valid && checker.IsAlowCancelDeposit(raw, ref hisSereServDeposits);
                    }

                    valid = valid && cancelChecker.HasNoProcessedServiceReq(raw, hisTreatment);
                }

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.ProcessTransaction(data, raw, cashierRoom);
                    this.ProcessBillFund(raw);
                    this.ProcessSereServBill(raw, ref sqls);
                    this.ProcessSereServDeposit(raw, hisSereServDeposits);
                    this.ProcessServiceReq(raw, hisSereServDeposits);
                    this.ProcessSereServDept(raw);
                    this.ProcessDebt(raw);
                    this.ProcessSeseDepoRepay(raw);
                    this.ProcessSaleExpMest(raw);
                    this.ProcessVaccination(raw);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls faild. sqls: " + sqls.ToString());
                    }

                    resultData = raw;
                    result = true;
                    if (raw.TREATMENT_ID.HasValue)
                    {
                        new EventLogGenerator(EventLog.Enum.HisTransaction_HuyGiaoDichBenhNhan).TreatmentCode(raw.TDL_TREATMENT_CODE).TransactionCode(raw.TRANSACTION_CODE).Run();
                    }
                    else
                    {
                        new EventLogGenerator(EventLog.Enum.HisTransaction_HuyGiaoDich).TransactionCode(raw.TRANSACTION_CODE).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessDebt(HIS_TRANSACTION raw)
        {
            if (raw != null && raw.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
            {
                List<HIS_TRANSACTION> debts = new HisTransactionGet().GetByDebtBillId(raw.ID);
                if (IsNotNullOrEmpty(debts))
                {
                    Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                    List<HIS_TRANSACTION> befores = Mapper.Map<List<HIS_TRANSACTION>>(debts);
                    debts.ForEach(o => o.DEBT_BILL_ID = null);

                    if (!this.hisTransactionDebtUpdate.UpdateList(debts, befores))
                    {
                        throw new Exception("Cap nhat DEBT_BILL_ID that bai");
                    }
                }
            }
        }

        private void ProcessTransaction(HisTransactionCancelSDO data, HIS_TRANSACTION raw, V_HIS_CASHIER_ROOM cashierRoom)
        {
            Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
            HIS_TRANSACTION before = Mapper.Map<HIS_TRANSACTION>(raw);

            raw.CANCEL_REASON = data.CancelReason;
            raw.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE;
            raw.CANCEL_CASHIER_ROOM_ID = cashierRoom.ID;
            raw.CANCEL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            raw.CANCEL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            raw.CANCEL_TIME = data.CancelTime;
            raw.CANCEL_REASON_ID = data.CancelReasonId;

            //Xu ly nghiep vu giao dich the (thanh toan dien tu)
            bool condition = false;
            if (((raw.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT) || (raw.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU))
                && raw.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE
                && data.IsInternal == false)
            {
                condition = true;
            }

            if (condition == true)
            {
                if (!new HisTransactionCancelEpaymentProccessor(param).Run(data, raw, cashierRoom))
                {
                    throw new Exception("Goi sang he thong the Huy giao dich that bai");
                }
            }

            if (!DAOWorker.HisTransactionDAO.Update(raw))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                throw new Exception("Huy giao dich that bai" + LogUtil.TraceData("raw", raw));
            }
            this.beforeUpdateHisTransactions.Add(before);
        }

        private void ProcessBillFund(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
            {
                if (!new HisBillFundTruncate(param).TruncateByBillId(data.ID))
                {
                    throw new Exception("Khong xoa duoc HIS_BILL_FUND.Rollback du lieu");
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
            List<V_HIS_SERE_SERV_BILL> hisSereServBills = new HisSereServBillGet().GetViewByBillId(data.ID);

            if (IsNotNullOrEmpty(hisSereServBills))
            {
                //Kiem tra xem BN co don nao da linh thuoc va chua thu hoi het hay ko
                List<long> serviceReqIds = hisSereServBills
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

                if (!new HisSereServCheck(param).HasNoInvoice(hisSereServBills.Select(s => s.SERE_SERV_ID).ToList()))
                {
                    throw new Exception("Rollback du lieu. ket thuc nghiep vu");
                }


                Mapper.CreateMap<V_HIS_SERE_SERV_BILL, HIS_SERE_SERV_BILL>();
                //List<HIS_SERE_SERV_BILL> toUpdates = Mapper.Map<List<HIS_SERE_SERV_BILL>>(hisSereServBills);
                List<HIS_SERE_SERV_BILL> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV_BILL>>(hisSereServBills);

                if (!new HisSereServBillCheck(param).IsUnLock(beforeUpdates))
                {
                    throw new Exception("HisSereServBillCheck.IsUnLock. ket thuc nghiep vu");
                }
                string sql = String.Format("UPDATE HIS_SERE_SERV_BILL SET IS_CANCEL = 1 WHERE BILL_ID = {0}", data.ID);
                sqls.Add(sql);
            }
        }

        private void ProcessSereServDeposit(HIS_TRANSACTION data, List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU || !IsNotNullOrEmpty(hisSereServDeposits))
                return;
            Mapper.CreateMap<HIS_SERE_SERV_DEPOSIT, HIS_SERE_SERV_DEPOSIT>();
            List<HIS_SERE_SERV_DEPOSIT> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV_DEPOSIT>>(hisSereServDeposits);
            hisSereServDeposits.ForEach(o => o.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE);
            if (!this.hisSereServDepositUpdate.UpdateList(hisSereServDeposits, beforeUpdates, true))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void ProcessSeseDepoRepay(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU)
            {
                return;
            }

            List<HIS_SESE_DEPO_REPAY> hisSeseDepoRepays = new HisSeseDepoRepayGet().GetByRepayId(data.ID);
            if (IsNotNullOrEmpty(hisSeseDepoRepays))
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

                Mapper.CreateMap<HIS_SESE_DEPO_REPAY, HIS_SESE_DEPO_REPAY>();
                List<HIS_SESE_DEPO_REPAY> beforeUpdates = Mapper.Map<List<HIS_SESE_DEPO_REPAY>>(hisSeseDepoRepays);
                hisSeseDepoRepays.ForEach(o => o.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE);
                if (!this.hisSeseDepoRepayUpdate.UpdateList(hisSeseDepoRepays, beforeUpdates))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSereServDept(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO)
            {
                return;
            }

            List<HIS_SERE_SERV_DEBT> sereServDepts = new HisSereServDebtGet().GetByDebtId(data.ID);
            if (IsNotNullOrEmpty(sereServDepts))
            {
                Mapper.CreateMap<HIS_SERE_SERV_DEBT, HIS_SERE_SERV_DEBT>();

                List<HIS_SERE_SERV_DEBT> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV_DEBT>>(sereServDepts);

                sereServDepts.ForEach(o => o.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE);
                if (!this.hisSereServDebtUpdate.UpdateList(sereServDepts, beforeUpdates))
                {
                    throw new Exception("hisSereServDebtUpdate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSaleExpMest(HIS_TRANSACTION data)
        {
            //Neu la phieu xuat ban hoac phieu chot no nha thuoc thi can cap nhat lai phieu xuat ban
            if ((data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP)
                || (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && data.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__DRUG_STORE))
            {
                List<HIS_EXP_MEST> hisSaleExpMests = null;
                if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP)
                {
                    hisSaleExpMests = new HisExpMestGet().GetByBillId(data.ID);
                }
                else
                {
                    hisSaleExpMests = new HisExpMestGet().GetByDebtId(data.ID);
                }

                if (IsNotNullOrEmpty(hisSaleExpMests))
                {
                    if (((data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP)
                        || data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO)
                        && HisExpMestCFG.EXPORT_SALE_MUST_BILL)
                    {
                        List<string> codes = hisSaleExpMests.Where(e => e.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE).Select(s => s.EXP_MEST_CODE).ToList();

                        if (IsNotNullOrEmpty(codes))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_PhieuXuatBanChuaHuyThucXuat, String.Join(",", codes));
                            throw new Exception("Phieu xuat ban chua huy thuc xuat. Khong cho phep huy giao dich");
                        }
                    }

                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    List<HIS_EXP_MEST> befores = Mapper.Map<List<HIS_EXP_MEST>>(hisSaleExpMests);
                    hisSaleExpMests.ForEach(o =>
                    {
                        if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                        {
                            o.BILL_ID = null;
                        }
                        if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO)
                        {
                            o.DEBT_ID = null;
                        }
                        o.CASHIER_LOGINNAME = null;
                        o.CASHIER_USERNAME = null;
                    });
                    if (!this.hisExpMestUpdate.UpdateTransactionId(hisSaleExpMests, befores))
                    {
                        throw new Exception("hisExpMestUpdate. Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                else
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Huy giao dich thanh toan xuat ban, nhung khong lay duoc phieu xuat ban tuong ung");
                }
            }
        }

        private void ProcessVaccination(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT
                && data.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_VACCIN)
            {
                List<HIS_VACCINATION> vaccinations = new HisVaccinationGet().GetByBillId(data.ID);
                if (IsNotNullOrEmpty(vaccinations))
                {
                    Mapper.CreateMap<HIS_VACCINATION, HIS_VACCINATION>();
                    List<HIS_VACCINATION> befores = Mapper.Map<List<HIS_VACCINATION>>(vaccinations);

                    vaccinations.ForEach(o =>
                    {
                        o.BILL_ID = null;
                    });
                    if (!this.hisVaccinationUpdate.UpdateList(vaccinations, befores))
                    {
                        throw new Exception("hisVaccinationUpdate. Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                else
                {
                    LogSystem.Warn("Huy giao dich thanh toan vaccin, nhung khong lay duoc yeu cau tiem vaccin nao tuong ung");
                }
            }
        }

        private void ProcessServiceReq(HIS_TRANSACTION data, List<HIS_SERE_SERV_DEPOSIT> hisSereServDeposits)
        {
            if (data.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU && IsNotNullOrEmpty(hisSereServDeposits))
            {
                List<long> serviceReqIds = hisSereServDeposits.Where(o => o.TDL_SERVICE_REQ_ID != null).Select(s => s.TDL_SERVICE_REQ_ID.Value).Distinct().ToList();
                if (IsNotNullOrEmpty(serviceReqIds))
                {
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);
                    serviceReqs = serviceReqs.Where(o => o.IS_COLLECTED == Constant.IS_TRUE).ToList();
                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                        List<HIS_SERVICE_REQ> befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                        serviceReqs.ForEach(o => o.IS_COLLECTED = null);

                        if (!this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                        {
                            throw new Exception("hisServiceReqUpdate. Ket thuc nghiep vu");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransactions))
            {
                if (!DAOWorker.HisTransactionDAO.UpdateList(this.beforeUpdateHisTransactions))
                {
                    LogSystem.Warn("Rollback du lieu HisTransaction that bai, can kiem tra lai." + LogUtil.TraceData("HisTransactions", this.beforeUpdateHisTransactions));
                }
            }
            this.hisTransactionDebtUpdate.RollbackData();
            //if (this.hisSereServBillUpdate != null) this.hisSereServBillUpdate.RollbackData();
            if (this.hisSereServDepositUpdate != null) this.hisSereServDepositUpdate.RollbackData();
            if (this.hisSeseDepoRepayUpdate != null) this.hisSeseDepoRepayUpdate.RollbackData();
            if (this.hisExpMestUpdate != null) this.hisExpMestUpdate.RollbackData();
            if (this.hisVaccinationUpdate != null) this.hisVaccinationUpdate.RollbackData();
        }
    }
}
