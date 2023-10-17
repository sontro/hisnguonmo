using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTransactionExp;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.ExpInvoice
{
    class ExpMestTransactionProcessor : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisExpMestUpdate hisExpMestUpdate;
        private HisTransactionExpCreate hisTransactionExpCreate;

        internal ExpMestTransactionProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestTransactionProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisTransactionExpCreate = new HisTransactionExpCreate(param);
        }

        internal bool Run(PharmacyCashierExpInvoiceSDO sdo, V_HIS_ACCOUNT_BOOK invoiceBook, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref HIS_TRANSACTION bill, ref List<HIS_BILL_GOODS> billGoods)
        {
            try
            {
                if (expMest != null && (IsNotNullOrEmpty(expMedicines) || IsNotNullOrEmpty(expMaterials)))
                {
                    this.ProcessTransaction(sdo, invoiceBook, expMest, expMedicines, expMaterials, ref bill);
                    this.ProcessBillGoods(bill, expMedicines, expMaterials, ref billGoods);
                    this.ProcessExpMest(expMest, bill);
                    this.ProcessHisTransactionExp(expMest, bill);
                    this.AutoExportExpMest(expMest);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private void ProcessTransaction(PharmacyCashierExpInvoiceSDO sdo, V_HIS_ACCOUNT_BOOK billBook, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref HIS_TRANSACTION invoice)
        {
            HIS_BRANCH branch = new TokenManager().GetBranch();
            HIS_TREATMENT treatment = expMest.TDL_TREATMENT_ID.HasValue ? new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value) : null;
            decimal totalPrice = 0;
            totalPrice += IsNotNullOrEmpty(expMedicines) ? expMedicines.Sum(o => o.PRICE.Value * (1 + o.VAT_RATIO.Value) * o.AMOUNT - (o.DISCOUNT ?? 0)) : 0;
            totalPrice += IsNotNullOrEmpty(expMaterials) ? expMaterials.Sum(o => o.PRICE.Value * (1 + o.VAT_RATIO.Value) * o.AMOUNT - (o.DISCOUNT ?? 0)) : 0;

            HIS_TRANSACTION hisTransaction = new HIS_TRANSACTION();
            hisTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
            hisTransaction.SALE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP;
            hisTransaction.AMOUNT = totalPrice;
            hisTransaction.SERE_SERV_AMOUNT = 0;
            hisTransaction.TREATMENT_ID = expMest.TDL_TREATMENT_ID;
            hisTransaction.ACCOUNT_BOOK_ID = billBook.ID;
            hisTransaction.BILL_TYPE_ID = billBook.BILL_TYPE_ID;
            hisTransaction.CASHIER_ROOM_ID = sdo.CashierRoomId;
            hisTransaction.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            hisTransaction.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            hisTransaction.NUM_ORDER = sdo.InvoiceNumOrder.HasValue ? sdo.InvoiceNumOrder.Value : 0;
            hisTransaction.PAY_FORM_ID = sdo.PayFormId;
            hisTransaction.SELLER_ACCOUNT_NUMBER = branch.ACCOUNT_NUMBER;
            hisTransaction.SELLER_ADDRESS = branch.ADDRESS;
            hisTransaction.SELLER_NAME = branch.BRANCH_NAME;
            hisTransaction.SELLER_PHONE = branch.PHONE;
            hisTransaction.SELLER_TAX_CODE = branch.TAX_CODE;
            hisTransaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;

            HisTransactionUtil.SetTdl(hisTransaction, expMest);
            if (!this.hisTransactionCreate.Create(hisTransaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            invoice = hisTransaction;
        }

        private void ProcessHisTransactionExp(HIS_EXP_MEST expMest, HIS_TRANSACTION invoice)
        {
            if (expMest != null)
            {
                HIS_TRANSACTION_EXP transactionExp = new HIS_TRANSACTION_EXP();
                transactionExp.EXP_MEST_ID = expMest.ID;
                transactionExp.TDL_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                transactionExp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                transactionExp.TRANSACTION_ID = invoice.ID;

                if (!this.hisTransactionExpCreate.Create(transactionExp))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void ProcessBillGoods(HIS_TRANSACTION bill, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref List<HIS_BILL_GOODS> billGoods)
        {
            List<HIS_BILL_GOODS> goods = new List<HIS_BILL_GOODS>();
            if (IsNotNullOrEmpty(expMedicines))
            {
                List<long> ids = expMedicines.Select(o => o.ID).ToList();
                List<V_HIS_EXP_MEST_MEDICINE> vExpMedicines = new HisExpMestMedicineGet().GetViewByIds(ids);
                List<HIS_BILL_GOODS> medicines = vExpMedicines.Select(o => new HIS_BILL_GOODS
                {
                    AMOUNT = o.AMOUNT,
                    BILL_ID = bill.ID,
                    DESCRIPTION = o.DESCRIPTION,
                    DISCOUNT = o.DISCOUNT,
                    GOODS_NAME = o.MEDICINE_TYPE_NAME,
                    GOODS_UNIT_NAME = o.SERVICE_UNIT_NAME,
                    PRICE = o.VIR_PRICE.Value
                }).ToList();
                goods.AddRange(medicines);
            }

            if (IsNotNullOrEmpty(expMaterials))
            {
                List<long> ids = expMaterials.Select(o => o.ID).ToList();
                List<V_HIS_EXP_MEST_MATERIAL> vExpMaterials = new HisExpMestMaterialGet().GetViewByIds(ids);
                List<HIS_BILL_GOODS> materials = vExpMaterials.Select(o => new HIS_BILL_GOODS
                {
                    AMOUNT = o.AMOUNT,
                    BILL_ID = bill.ID,
                    DESCRIPTION = o.DESCRIPTION,
                    DISCOUNT = o.DISCOUNT,
                    GOODS_NAME = o.MATERIAL_TYPE_NAME,
                    GOODS_UNIT_NAME = o.SERVICE_UNIT_NAME,
                    PRICE = o.VIR_PRICE.Value
                }).ToList();
                goods.AddRange(materials);
            }

            if (IsNotNullOrEmpty(goods) && !this.hisBillGoodsCreate.CreateList(goods))
            {
                throw new Exception("Khong tao duoc HisBillGoods cho giao dich thanh toan. Du lieu se bi Rollback.");
            }
            billGoods = goods;
        }

        private void ProcessExpMest(HIS_EXP_MEST expMest, HIS_TRANSACTION bill)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);

            expMest.BILL_ID = bill.ID;
            expMest.CASHIER_LOGINNAME = bill.CASHIER_LOGINNAME;
            expMest.CASHIER_USERNAME = bill.CASHIER_USERNAME;

            if (!this.hisExpMestUpdate.UpdateTransactionId(expMest, before))
            {
                throw new Exception("Update BILL_ID cho SaleExpMest that bai. Du lieu se bi Rollback.");
            }
        }

        private void AutoExportExpMest(HIS_EXP_MEST expMest)
        {
            try
            {
                if (!HisExpMestCFG.IS_AUTO_EXPORT_EXP_MEST_SALE)
                {
                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                    {
                        HisExpMestResultSDO resultData = null;
                        bool approve = new HisExpMestAutoProcess().Run(expMest, ref resultData, true);
                        if (!approve)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongDuyetThatBai);
                            return;
                        }
                        expMest.EXP_MEST_STT_ID = resultData.ExpMest.EXP_MEST_STT_ID;
                    }

                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                    {
                        HIS_EXP_MEST resultData = null;
                        HisExpMestExportSDO sdo = new HisExpMestExportSDO();
                        sdo.ExpMestId = expMest.ID;
                        sdo.ReqRoomId = expMest.REQ_ROOM_ID;
                        sdo.IsFinish = true;
                        if (!new HisExpMestExport().Export(sdo, true, ref resultData))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongXuatThatBai);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
            this.hisBillGoodsCreate.RollbackData();
            this.hisTransactionExpCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}
