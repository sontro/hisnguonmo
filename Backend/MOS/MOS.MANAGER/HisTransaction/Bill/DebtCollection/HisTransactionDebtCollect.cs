using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisDebtGoods;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTransaction.Bill.SaleExpMest;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransaction.Bill.DebtCollection
{
    partial class HisTransactionDebtCollect : BusinessBase
    {
        private HisTransactionUpdate hisTransactionUpdate;
        private HisTransactionBillCreate hisTransactionBillCreate;
        private HisTransactionBillCreateWithBillGoods hisTransactionBillCreateWithBillGoods;

        internal HisTransactionDebtCollect(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
            this.hisTransactionBillCreate = new HisTransactionBillCreate(param);
            this.hisTransactionBillCreateWithBillGoods = new HisTransactionBillCreateWithBillGoods(param);
        }

        public bool Run(HisTransactionDebtCollecSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                HisTransactionDebtCollectCheck checker = new HisTransactionDebtCollectCheck(param);
                bool valid = true;
                WorkPlaceSDO workPlaceSDO = null;
                List<HIS_TRANSACTION> debts = null;

                valid = valid && checker.IsValidData(data, ref debts);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlaceSDO);
                if (valid)
                {
                    this.ProcessTransactionBill(debts, workPlaceSDO, data, ref resultData);
                    this.ProcessBillGoods(debts, workPlaceSDO, data, ref resultData);

                    if (resultData != null)
                    {
                        this.UpdateDebts(debts, resultData.ID);
                        result = true;
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

        private void ProcessBillGoods(List<HIS_TRANSACTION> debts, WorkPlaceSDO workPlaceSDO, HisTransactionDebtCollecSDO data, ref V_HIS_TRANSACTION resultData)
        {
            if (IsNotNullOrEmpty(debts) && debts.Exists(t => t.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__DRUG_STORE))
            {
                List<long> debtIds = debts.Select(o => o.ID).ToList();
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByDebtIds(debtIds);
                List<HIS_DEBT_GOODS> debtGoods = new HisDebtGoodsGet().GetByDebtIds(debtIds);

                HisTransactionBillGoodsSDO billGoodsSdo = this.ConvertToBillGoodsSdo(workPlaceSDO, debts, debtGoods, data, expMests);
                if (billGoodsSdo != null)
                {
                    this.hisTransactionBillCreateWithBillGoods.Run(billGoodsSdo, ref resultData);
                }
            }
        }

        private void ProcessTransactionBill(List<HIS_TRANSACTION> debts, WorkPlaceSDO workPlaceSDO, HisTransactionDebtCollecSDO data, ref V_HIS_TRANSACTION resultData)
        {
            if (IsNotNullOrEmpty(debts) && debts.Exists(t => t.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__SERVICE || t.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__TREAT))
            {
                List<long> debtIds = debts.Select(o => o.ID).ToList();
                List<HIS_SERE_SERV_DEBT> sereServDebts = new HisSereServDebtGet().GetByDebtIds(debtIds);

                HisTransactionBillSDO billSdo = this.ConvertToBillSdo(workPlaceSDO, debts, sereServDebts, data);
                HisTransactionBillResultSDO resultSDO = null;
                if (billSdo != null && this.hisTransactionBillCreate.CreateBill(billSdo, ref resultSDO))
                {
                    resultData = resultSDO.TransactionBill;
                }
            }
        }

        private void UpdateDebts(List<HIS_TRANSACTION> debts, long billId)
        {
            if (IsNotNullOrEmpty(debts))
            {
                Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                List<HIS_TRANSACTION> befores = Mapper.Map<List<HIS_TRANSACTION>>(debts);

                debts.ForEach(o => o.DEBT_BILL_ID = billId);

                if (!this.hisTransactionUpdate.UpdateList(debts, befores))
                {
                    throw new Exception("Cap nhat DEBT_BILL_ID that bai, rollback du lieu");
                }
            }
        }

        private void Rollback()
        {
            this.hisTransactionUpdate.RollbackData();
            this.hisTransactionBillCreate.RollbackData();
        }

        private HisTransactionBillSDO ConvertToBillSdo(WorkPlaceSDO workPlaceSDO, List<HIS_TRANSACTION> debts, List<HIS_SERE_SERV_DEBT> sereServDebts, HisTransactionDebtCollecSDO data)
        {
            if (IsNotNullOrEmpty(sereServDebts) && data != null && data.TreatmentId.HasValue && IsNotNullOrEmpty(debts) && workPlaceSDO.CashierRoomId.HasValue)
            {
                HisTransactionBillSDO sdo = new HisTransactionBillSDO();
                HIS_TRANSACTION transaction = new HIS_TRANSACTION();
                transaction.CASHIER_ROOM_ID = workPlaceSDO.CashierRoomId.Value;
                transaction.EXEMPTION = data.Exemption;//debts.Sum(o => o.EXEMPTION ?? 0);
                transaction.TRANSACTION_TIME = data.TransactionTime;
                transaction.IS_DEBT_COLLECTION = Constant.IS_TRUE;
                transaction.TREATMENT_ID = data.TreatmentId;
                transaction.ACCOUNT_BOOK_ID = data.AccountBookId;
                transaction.PAY_FORM_ID = data.PayFormId;
                transaction.NUM_ORDER = !data.NumOrder.HasValue ? 0 : data.NumOrder.Value;
                transaction.DESCRIPTION = data.Description;
                transaction.TRANSFER_AMOUNT = data.TransferAmount;
                transaction.SWIPE_AMOUNT = data.SwipeAmount;
                sdo.Transaction = transaction;
                sdo.RequestRoomId = data.RequestRoomId;

                List<HIS_SERE_SERV_BILL> bills = sereServDebts.GroupBy(o => new
                {
                    o.SERE_SERV_ID
                }).Select(o => new HIS_SERE_SERV_BILL
                {
                    SERE_SERV_ID = o.Key.SERE_SERV_ID,
                    PRICE = o.ToList().Sum(t => t.DEBT_PRICE)
                }).ToList();
                sdo.SereServBills = bills;

                decimal kcAmount = 0;
                decimal debtAmount = debts.Sum(o => o.AMOUNT);
                kcAmount = debts.Sum(o => (o.KC_AMOUNT ?? 0));
                //Tong so tien cho phep dung de ket chuyen (so tien Hiện dư)
                decimal? availableAmount = new HisTreatmentGet().GetAvailableAmount(data.TreatmentId.Value);
                if (kcAmount > 0 && (!availableAmount.HasValue || kcAmount > availableAmount.Value))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTrasaction_SoTienHienDuNhoHonTaiThoiDiemChotNo);
                    throw new Exception(String.Format("So tien hien du nho hon tai thoi diem chot no. availableAmount: {0}, kcAmount: {1}", availableAmount, kcAmount));
                }
                if (kcAmount > 0)
                {
                    transaction.KC_AMOUNT = kcAmount;
                }
                //sdo.PayAmount = (debtAmount + kcAmount) - (transaction.EXEMPTION ?? 0);
                transaction.AMOUNT = debtAmount + kcAmount;
                return sdo;
            }
            else
            {
                LogSystem.Warn("Du lieu ko hop le.");
                return null;
            }
        }

        private HisTransactionBillGoodsSDO ConvertToBillGoodsSdo(WorkPlaceSDO workPlaceSDO, List<HIS_TRANSACTION> debts, List<HIS_DEBT_GOODS> debtGoods, HisTransactionDebtCollecSDO data, List<HIS_EXP_MEST> expMests)
        {
            if (IsNotNullOrEmpty(debtGoods) && data != null && IsNotNullOrEmpty(debts) 
                && workPlaceSDO.CashierRoomId.HasValue
                && IsNotNullOrEmpty(expMests))
            {
                HisTransactionBillGoodsSDO sdo = new HisTransactionBillGoodsSDO();
                HIS_TRANSACTION transaction = new HIS_TRANSACTION();
                transaction.CASHIER_ROOM_ID = workPlaceSDO.CashierRoomId.Value;
                transaction.EXEMPTION = data.Exemption;//debts.Sum(o => o.EXEMPTION ?? 0);
                transaction.TRANSACTION_TIME = data.TransactionTime;
                transaction.IS_DEBT_COLLECTION = Constant.IS_TRUE;
                transaction.ACCOUNT_BOOK_ID = data.AccountBookId;
                transaction.PAY_FORM_ID = data.PayFormId;
                transaction.NUM_ORDER = !data.NumOrder.HasValue ? 0 : data.NumOrder.Value;
                transaction.DESCRIPTION = data.Description;
                transaction.TRANSFER_AMOUNT = data.TransferAmount;
                transaction.AMOUNT = debts.Sum(o => o.AMOUNT);
                transaction.SWIPE_AMOUNT = data.SwipeAmount;
                sdo.HisTransaction = transaction;

                List<HIS_BILL_GOODS> goods = debtGoods.Select(o => new HIS_BILL_GOODS
                {
                    AMOUNT = o.AMOUNT,
                    CONCENTRA = o.CONCENTRA,
                    DESCRIPTION = o.DESCRIPTION,
                    DISCOUNT = o.DISCOUNT,
                    EXPIRED_DATE = o.EXPIRED_DATE,
                    GOODS_NAME = o.GOODS_NAME,
                    GOODS_UNIT_NAME = o.GOODS_UNIT_NAME,
                    MATERIAL_TYPE_ID = o.MATERIAL_TYPE_ID,
                    MEDICINE_TYPE_ID = o.MEDICINE_TYPE_ID,
                    NATIONAL_NAME = o.NATIONAL_NAME,
                    NONE_MEDI_SERVICE_ID = o.NONE_MEDI_SERVICE_ID,
                    PACKAGE_NUMBER = o.PACKAGE_NUMBER,
                    PRICE = o.PRICE,
                    SERVICE_UNIT_ID = o.SERVICE_UNIT_ID,
                    VAT_RATIO = o.VAT_RATIO
                }).ToList();

                sdo.HisBillGoods = goods;
                sdo.ExpMestIds = expMests.Select(o => o.ID).ToList();

                
                return sdo;
            }
            else
            {
                LogSystem.Warn("Du lieu ko hop le.");
                return null;
            }
        }
    }
}
