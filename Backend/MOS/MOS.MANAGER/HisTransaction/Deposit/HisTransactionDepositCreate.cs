using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTransaction.Deposit.Epayment;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransaction.Deposit
{
    partial class HisTransactionDepositCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServDepositCreate hisSereServDepositCreate;
        private HisDepositReqUpdate hisDepositReqUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        private HIS_TRANSACTION recentHisTransaction;

        internal HisTransactionDepositCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        public bool CreateDeposit(HisTransactionDepositSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);

                WorkPlaceSDO workPlace = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTransactionDepositCheck depositChecker = new HisTransactionDepositCheck(param);
                HisTransactionDepositEpaymentCheck epaymentChecker = new HisTransactionDepositEpaymentCheck(param);

                string theBranchCode = null;
                HIS_CARD hisCard = null;
                long? epaymentAmount = 0;

                bool valid = true;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.Transaction);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.HasNotFinancePeriod(data.Transaction);
                valid = valid && checker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && checker.IsValidNumOrder(data.Transaction, hisAccountBook);
                valid = valid && (!data.Transaction.TREATMENT_ID.HasValue || treatmentChecker.VerifyId(data.Transaction.TREATMENT_ID.Value, ref treatment));
                valid = valid && epaymentChecker.Run(data, treatment, ref epaymentAmount, ref theBranchCode, ref hisCard);
                if (valid)
                {
                    //Xu ly nghiep vu giao dich the (thanh toan dien tu)
                    if (!new HisTransactionDepositEpaymentProcessor(param).Run(data, epaymentAmount, treatment.PATIENT_ID, theBranchCode, hisCard))
                    {
                        return false;
                    }

                    if (!workPlace.CashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }

                    List<HIS_SERE_SERV> sereServs = null;

                    data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                    data.Transaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;

                    this.ProcessTransactionDeposit(data, treatment);
                    this.ProcessSereServDeposit(data, ref sereServs);
                    this.ProcessServiceReq(data, sereServs);
                    this.ProcessDepositReq(data);
                    this.PassResult(ref resultData);
                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichTamUng, this.recentHisTransaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServDepositCreate = new HisSereServDepositCreate(param);
            this.hisDepositReqUpdate = new HisDepositReqUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        private void ProcessTransactionDeposit(HisTransactionDepositSDO data, HIS_TREATMENT treatment)
        {
            if (IsNotNullOrEmpty(data.SereServDeposits))
            {
                data.Transaction.TDL_SERE_SERV_DEPOSIT_COUNT = (long)data.SereServDeposits.Count;
            }
            HisTransactionUtil.SetTreatmentFeeInfo(data.Transaction);

            if (!this.hisTransactionCreate.Create(data.Transaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = data.Transaction;
        }

        private void ProcessDepositReq(HisTransactionDepositSDO data)
        {
            if (data.DepositReqId.HasValue)
            {
                HIS_DEPOSIT_REQ depositReq = new HisDepositReqGet().GetById(data.DepositReqId.Value);
                if (depositReq == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("depositReq null");
                }
                if (depositReq.DEPOSIT_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDeposit_TonTaiDuLieu);
                    throw new Exception("depositReq da ton tai giao dich tam ung, ko cho tao phieu tam ung" + LogUtil.TraceData("depositReq", depositReq));
                }
                if (depositReq.AMOUNT != data.Transaction.AMOUNT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("depositReq.AMOUNT != data.Transaction.AMOUNT");
                }
                depositReq.DEPOSIT_ID = this.recentHisTransaction.ID;
                if (!this.hisDepositReqUpdate.Update(depositReq, false))
                {
                    throw new Exception("rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Xu ly trong truong hop giao dich tam ung co gan voi cac dich vu (sere_serv)
        /// </summary>
        /// <param name="data"></param>
        private void ProcessSereServDeposit(HisTransactionDepositSDO data, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(data.SereServDeposits))
            {
                List<long> sereServIds = data.SereServDeposits.Select(o => o.SERE_SERV_ID).ToList();

                //Lay danh sach thong tin "cong no" (va chua bi huy) tuong ung voi sere_serv
                List<HIS_SERE_SERV_DEBT> existsDebts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                if (IsNotNullOrEmpty(existsDebts))
                {
                    List<string> names = existsDebts.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList();
                    string nameStr = string.Join(",", names);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaChotNo, nameStr);
                    throw new Exception("Cac dich vu da duoc chot no: " + names);
                }

                sereServs = new HisSereServGet().GetByIds(sereServIds);

                //Lay ra cac danh sach sere_serv khong thuc hien
                List<string> noExecuteServices = sereServs
                    .Where(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE)
                    .Select(o => o.TDL_SERVICE_NAME).ToList();

                if (IsNotNullOrEmpty(noExecuteServices))
                {
                    string names = string.Join(",", noExecuteServices);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDeposit_CacDichVuSauKhongThucHienKhongChoTamUng, names);
                    throw new Exception("Cac dich vu 'ko thuc hien' ko cho tam ung" + names);
                }

                if (!HisTransactionCFG.IS_ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION)
                {
                    List<string> presInServices = sereServs.Where(o => (o.MEDICINE_ID.HasValue || o.MATERIAL_ID.HasValue) && o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT).Select(s => s.TDL_SERVICE_NAME).ToList();
                    if (IsNotNullOrEmpty(presInServices))
                    {
                        string names = string.Join(",", presInServices);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDeposit_CacThuocVatTuSauThuocDonNoiTru, names);
                        throw new Exception("Cac thuoc/vat tu sua thuoc don noi tru khong cho phep tam ung dich vu" + names);
                    }
                }

                if (!new HisSereServCheck(param).HasNoDeposit(sereServIds, true))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                decimal totalAmount = 0;
                foreach (HIS_SERE_SERV_DEPOSIT d in data.SereServDeposits)
                {
                    totalAmount += d.AMOUNT;
                    d.DEPOSIT_ID = this.recentHisTransaction.ID;
                    HisSereServDepositUtil.SetTdl(d, sereServs.FirstOrDefault(o => o.ID == d.SERE_SERV_ID));
                }

                if (totalAmount != data.Transaction.AMOUNT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Tong so tien cua HIS_SERE_SERV_DEPOSIT khong khop voi so tien tam ung trong transaction. totalDereAmount: " + totalAmount);
                }

                if (!this.hisSereServDepositCreate.CreateList(data.SereServDeposits))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessServiceReq(HisTransactionDepositSDO data, List<HIS_SERE_SERV> sereServs)
        {
            if (data.IsCollected.HasValue && data.IsCollected.Value && IsNotNullOrEmpty(sereServs))
            {
                List<long> serviceReqIds = sereServs.Select(s => s.SERVICE_REQ_ID.Value).Distinct().ToList();
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                List<HIS_SERVICE_REQ> befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                serviceReqs.ForEach(o => o.IS_COLLECTED = Constant.IS_TRUE);

                if (!this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                {
                    throw new Exception("hisServiceReqUpdate. Ket thuc nghiep vu");
                }
            }
        }

        private void PassResult(ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet(param).GetViewById(this.recentHisTransaction.ID);
        }

        private void RollbackData()
        {
            if (this.hisServiceReqUpdate != null) this.hisServiceReqUpdate.RollbackData();
            if (this.hisSereServDepositCreate != null) this.hisSereServDepositCreate.RollbackData();
            if (this.hisDepositReqUpdate != null) this.hisDepositReqUpdate.RollbackData();
            if (this.hisTransactionCreate != null) this.hisTransactionCreate.RollbackData();
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisTransactionDepositSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                if (data.Transaction != null)
                {
                    data.Transaction.TRANSACTION_TIME = now;
                }
            }
        }
    }
}