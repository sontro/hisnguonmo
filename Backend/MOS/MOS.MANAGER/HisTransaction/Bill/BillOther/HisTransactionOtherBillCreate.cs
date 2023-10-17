using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatient.UpdateInfo;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.BillOther
{
    class HisTransactionOtherBillCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisPatientUpdateInfo hisPatientUpdate;

        private HIS_TRANSACTION recentTransaction;

        internal HisTransactionOtherBillCreate()
            : base()
        {
            this.Init();
        }

        internal HisTransactionOtherBillCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisPatientUpdate = new HisPatientUpdateInfo(param);
        }

        internal bool Run(HisTransactionOtherBillSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HIS_TREATMENT treatment = null;
                this.SetServerTime(data);
                data.HisTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                HisTransactionCheck transactionChecker = new HisTransactionCheck(param);
                HisTransactionOtherBillCheck checker = new HisTransactionOtherBillCheck(param);
                HisTransactionCheck commonChecker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisTransaction);
                valid = valid && IsNotNullOrEmpty(data.HisBillGoods);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && commonChecker.IsCashierRoom(workPlace);
                valid = valid && checker.IsValidAmount(data.HisTransaction);
                valid = valid && commonChecker.IsUnlockAccountBook(data.HisTransaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && commonChecker.HasNotFinancePeriod(data.HisTransaction);
                valid = valid && (!data.HisTransaction.TREATMENT_ID.HasValue || treatmentChecker.VerifyId(data.HisTransaction.TREATMENT_ID.Value, ref treatment));
                valid = valid && checker.IsGetNumOrderFromOldSystem(data.HisTransaction, hisAccountBook);
                valid = valid && commonChecker.IsValidNumOrder(data.HisTransaction, hisAccountBook, false);
                valid = valid && checker.CheckIsForOtherSale(hisAccountBook);
                valid = valid && checker.IsValidCardTransaction(data.HisTransaction);
                if (valid)
                {
                    data.HisTransaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                    this.ProcessTransactionBill(data, hisAccountBook, treatment);
                    this.ProcessBillGoods(data);
                    this.ProcessPatient(data, treatment);
                    this.PassResult(ref resultData);
                    result = true;
                    string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : "";
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichThanhToanKhac, this.recentTransaction.AMOUNT).TreatmentCode(treatmentCode).TransactionCode(this.recentTransaction.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }


        private void ProcessTransactionBill(HisTransactionOtherBillSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook, HIS_TREATMENT treatment)
        {
            decimal totalGoodPrice = data.HisBillGoods.Sum(s => ((s.PRICE * s.AMOUNT) - (s.DISCOUNT ?? 0)));
            if (totalGoodPrice != data.HisTransaction.AMOUNT)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Tong tien trong HisTransaction Khac voi so luong trong Goods: TranPrice: " + data.HisTransaction.AMOUNT + "; GoodsPrice: " + totalGoodPrice);
            }
            data.HisTransaction.BILL_TYPE_ID = hisAccountBook.BILL_TYPE_ID;
            data.HisTransaction.SALE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_OTHER;
            data.HisTransaction.SERE_SERV_AMOUNT = 0;
            HisTransactionUtil.SetTreatmentFeeInfo(data.HisTransaction);

            if (!this.hisTransactionCreate.Create(data.HisTransaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentTransaction = data.HisTransaction;
        }


        private void ProcessBillGoods(HisTransactionOtherBillSDO data)
        {
            if (IsNotNullOrEmpty(data.HisBillGoods))
            {
                data.HisBillGoods.ForEach(o => o.BILL_ID = this.recentTransaction.ID);
                if (!this.hisBillGoodsCreate.CreateList(data.HisBillGoods))
                {
                    throw new Exception("Khong tao duoc HisBillGoods cho giao dich thanh toan. Du lieu se bi Rollback.");
                }
            }
        }

        private void ProcessPatient(HisTransactionOtherBillSDO data, HIS_TREATMENT treatment)
        {
            if (data.HisTransaction != null && treatment != null &&
                (!string.IsNullOrWhiteSpace(data.HisTransaction.BUYER_TAX_CODE)
                || !string.IsNullOrWhiteSpace(data.HisTransaction.BUYER_ACCOUNT_NUMBER)
                || !string.IsNullOrWhiteSpace(data.HisTransaction.BUYER_ORGANIZATION)))
            {
                HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();

                HIS_PATIENT old = Mapper.Map<HIS_PATIENT>(patient);
                patient.TAX_CODE = data.HisTransaction.BUYER_TAX_CODE;
                patient.ACCOUNT_NUMBER = data.HisTransaction.BUYER_ACCOUNT_NUMBER;
                patient.WORK_PLACE = data.HisTransaction.BUYER_ORGANIZATION;

                if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_PATIENT>(old, patient))
                {
                    HisPatientUpdateSDO sdo = new HisPatientUpdateSDO();
                    sdo.HisPatient = patient;
                    sdo.IsNotUpdateImage = true;
                    sdo.TreatmentId = treatment.ID;//update lai ca treatment co chua TDL
                    HIS_PATIENT resultData = null;
                    if (!this.hisPatientUpdate.Run(sdo, ref resultData))
                    {
                        LogSystem.Warn("Cap nhat thong tin patient (thong tin ma so thue, tai khoan, to chuc) that bai");
                    }
                }
            }
        }

        private void PassResult(ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet().GetViewById(this.recentTransaction.ID);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisTransactionOtherBillSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                if (data.HisTransaction != null)
                {
                    data.HisTransaction.TRANSACTION_TIME = now;
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisBillGoodsCreate.RollbackData();
                this.hisTransactionCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
