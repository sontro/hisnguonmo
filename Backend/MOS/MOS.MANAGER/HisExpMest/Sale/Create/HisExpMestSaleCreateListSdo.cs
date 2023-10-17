using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    class HisExpMestSaleCreateListSdo : BusinessBase
    {
        private List<HisExpMestProcessor> expMestProcessors;
        private List<MaterialProcessor> materialProcessors;
        private List<MedicineProcessor> medicineProcessors;

        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisTransactionCreate hisTransactionCreate;

        internal HisExpMestSaleCreateListSdo()
            : base()
        {
            this.Init();
        }

        internal HisExpMestSaleCreateListSdo(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessors = new List<HisExpMestProcessor>();
            this.materialProcessors = new List<MaterialProcessor>();
            this.medicineProcessors = new List<MedicineProcessor>();
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisTransactionCreate = new HisTransactionCreate(param);
        }

        internal bool Run(HisExpMestSaleListSDO data, ref HisExpMestSaleListResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AutoEnum en = AutoEnum.NONE;

                V_HIS_ACCOUNT_BOOK accountBook = null;
                long? transactionTime = null;

                HisExpMestSaleCheck checker = new HisExpMestSaleCheck(param);
                valid = valid && checker.IsAllowed(data.SaleData);
                valid = valid && checker.VerifyRequireField(data.SaleData);
                valid = valid && checker.IsValidGroup(data.SaleData, null);
                valid = valid && checker.IsValidTransactionInfo(data, ref accountBook, ref transactionTime);
                valid = valid && checker.CheckAuto(data.SaleData.FirstOrDefault().MediStockId, ref en);
                if (valid)
                {
                    long time = Inventec.Common.DateTime.Get.Now().Value;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    decimal totalPrice = 0;

                    List<long> expMestMaterialIds = new List<long>();
                    List<long> expMestMedicineIds = new List<long>();
                    List<long> expMestIds = new List<long>();
                    List<string> sqls = new List<string>();

                    List<HIS_EXP_MEST> expMests = new List<HIS_EXP_MEST>();

                    foreach (var sdo in data.SaleData)
                    {
                        List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                        List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                        HIS_EXP_MEST expMest = null;

                        HisExpMestProcessor expMestProcesser = new HisExpMestProcessor(param);
                        this.expMestProcessors.Add(expMestProcesser);
                        //Tao exp_mest
                        if (!expMestProcesser.Run(sdo, ref expMest, en, time, loginname, username))
                        {
                            throw new Exception("expMestProcesser Rollback du lieu. Ket thuc nghiep vu");
                        }

                        MaterialProcessor materialProcessor = new MaterialProcessor(param);
                        this.materialProcessors.Add(materialProcessor);
                        //Tao exp_mest_material
                        if (!materialProcessor.Run(sdo.ClientSessionKey, sdo.PatientTypeId, sdo.MaterialBeanIds, sdo.Materials, expMest, ref expMestMaterials, ref sqls, en, time, loginname, username))
                        {
                            throw new Exception("materialProcessor. Ket thuc nghiep vu");
                        }

                        MedicineProcessor medicineProcessor = new MedicineProcessor(param);
                        this.medicineProcessors.Add(medicineProcessor);
                        //Tao exp_mest_medicine
                        if (!medicineProcessor.Run(sdo.ClientSessionKey, sdo.PatientTypeId, sdo.MedicineBeanIds, sdo.Medicines, expMest, ref expMestMedicines, ref sqls, en, time, loginname, username))
                        {
                            throw new Exception("medicineProcessor Rollback du lieu. Ket thuc nghiep vu");
                        }

                        //Tinh tong tin cua phieu xuat ban
                        decimal? price = this.GetTotalPrice(expMestMaterials, expMestMedicines);

                        expMest.TDL_TOTAL_PRICE = price;
                        expMest.PAY_FORM_ID = data.PayFormId;

                        //Tong tien cua tat ca cac phieu xuat ban
                        totalPrice += price.HasValue ? price.Value : 0;

                        if (IsNotNullOrEmpty(expMestMaterials))
                        {
                            expMestMaterialIds.AddRange(expMestMaterials.Select(o => o.ID).ToList());
                        }
                        if (IsNotNullOrEmpty(expMestMedicines))
                        {
                            expMestMedicineIds.AddRange(expMestMedicines.Select(o => o.ID).ToList());
                        }

                        expMests.Add(expMest);
                    }

                    List<V_HIS_EXP_MEST_MEDICINE> vExpMestMedicines = new HisExpMestMedicineGet().GetViewByIds(expMestMedicineIds);
                    List<V_HIS_EXP_MEST_MATERIAL> vExpMestMaterials = new HisExpMestMaterialGet().GetViewByIds(expMestMaterialIds);
                    List<HIS_BILL_GOODS> billGoods = null;
                    HIS_TRANSACTION transaction = null;

                    this.ProcessTransactionBill(expMests, data, accountBook, totalPrice, ref transaction);

                    this.ProcessBillGoods(data, transaction, vExpMestMedicines, vExpMestMaterials, ref billGoods);

                    this.ProcessUpdateExpMest(expMests, transaction, ref sqls);

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    result = true;

                    this.PassResult(expMests, vExpMestMedicines, vExpMestMaterials, billGoods, transaction, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuatBan, this.GenerateEventLog(expMests)).Run();
                    if (transaction != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisTransaction_TaoGiaoDichThanhToan, transaction.AMOUNT).TransactionCode(transaction.TRANSACTION_CODE).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void ProcessUpdateExpMest(List<HIS_EXP_MEST> expMests, HIS_TRANSACTION transaction, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(expMests))
            {
                foreach (HIS_EXP_MEST exp in expMests)
                {
                    string updateSql = null;
                    if (exp.TDL_TOTAL_PRICE.HasValue || exp.PAY_FORM_ID.HasValue)
                    {
                        StringBuilder str = new StringBuilder().Append("UPDATE HIS_EXP_MEST SET ");
                        List<string> listStr = new List<string>();
                        if (transaction != null && exp.TDL_TOTAL_PRICE.HasValue)
                        {
                            listStr.Add(string.Format("TDL_TOTAL_PRICE = {0}, BILL_ID = {1}", exp.TDL_TOTAL_PRICE.Value.ToString("G27", CultureInfo.InvariantCulture), transaction.ID));
                        }
                        else if (transaction == null && exp.TDL_TOTAL_PRICE.HasValue)
                        {
                            listStr.Add(string.Format("TDL_TOTAL_PRICE = {0}", exp.TDL_TOTAL_PRICE.Value.ToString("G27", CultureInfo.InvariantCulture)));
                        }
                        if (exp.PAY_FORM_ID.HasValue)
                        {
                            listStr.Add(string.Format("PAY_FORM_ID = {0}", exp.PAY_FORM_ID.Value));
                        }
                        str.Append(String.Join(", ", listStr));
                        str.Append(string.Format(" WHERE ID = {0}",exp.ID));
                        updateSql = str.ToString();
                    }
                    
                    if (!string.IsNullOrWhiteSpace(updateSql))
                    {
                        sqls.Add(updateSql);
                    }
                }
            }
        }

        private void PassResult(List<HIS_EXP_MEST> expMests, List<V_HIS_EXP_MEST_MEDICINE> vExpMestMedicines, List<V_HIS_EXP_MEST_MATERIAL> vExpMestMaterials, List<HIS_BILL_GOODS> billGoods, HIS_TRANSACTION transaction, ref HisExpMestSaleListResultSDO resultData)
        {
            if (IsNotNullOrEmpty(expMests))
            {
                resultData = new HisExpMestSaleListResultSDO();
                resultData.BillGoods = billGoods;
                resultData.Transaction = transaction;
                List<HisExpMestSaleResultSDO> sdos = new List<HisExpMestSaleResultSDO>();
                List<V_HIS_EXP_MEST> vExpMests = new HisExpMestGet().GetViewByIds(expMests.Select(o => o.ID).ToList());

                foreach (V_HIS_EXP_MEST exp in vExpMests)
                {
                    HisExpMestSaleResultSDO sdo = new HisExpMestSaleResultSDO();
                    sdo.ExpMest = exp;
                    sdo.ExpMedicines = vExpMestMedicines != null ? vExpMestMedicines.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                    sdo.ExpMaterials = vExpMestMaterials != null ? vExpMestMaterials.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                    sdos.Add(sdo);
                }
                resultData.ExpMestSdos = sdos;
            }
        }

        private void ProcessTransactionBill(List<HIS_EXP_MEST> expMests, HisExpMestSaleListSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook, decimal totalPrice, ref HIS_TRANSACTION transaction)
        {
            if (hisAccountBook != null && data.CreateBill)
            {
                transaction = new HIS_TRANSACTION();
                transaction.ACCOUNT_BOOK_ID = hisAccountBook.ID;
                transaction.BILL_TYPE_ID = hisAccountBook.BILL_TYPE_ID;
                transaction.SALE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP;
                transaction.AMOUNT = totalPrice;
                transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                transaction.PAY_FORM_ID = data.PayFormId.Value;
                transaction.CASHIER_ROOM_ID = data.CashierRoomId.Value;
                transaction.SERE_SERV_AMOUNT = 0;
                transaction.TRANSACTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
                transaction.ROUND_PRICE_BASE = data.RoundedPriceBase;
                transaction.ROUNDED_TOTAL_PRICE = data.RoundedTotalPrice;
                transaction.TDL_PATIENT_ADDRESS = expMests[0].TDL_PATIENT_ADDRESS;
                transaction.TDL_PATIENT_CODE = expMests[0].TDL_PATIENT_CODE;
                transaction.TDL_PATIENT_COMMUNE_CODE = expMests[0].TDL_PATIENT_COMMUNE_CODE;
                transaction.TDL_PATIENT_DISTRICT_CODE = expMests[0].TDL_PATIENT_DISTRICT_CODE;
                transaction.TDL_PATIENT_DOB = expMests[0].TDL_PATIENT_DOB;
                transaction.TDL_PATIENT_FIRST_NAME = expMests[0].TDL_PATIENT_FIRST_NAME;
                transaction.TDL_PATIENT_GENDER_ID = expMests[0].TDL_PATIENT_GENDER_ID;
                transaction.TDL_PATIENT_GENDER_NAME = expMests[0].TDL_PATIENT_GENDER_NAME;
                transaction.TDL_PATIENT_ID = expMests[0].TDL_PATIENT_ID;
                transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = expMests[0].TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                transaction.TDL_PATIENT_LAST_NAME = expMests[0].TDL_PATIENT_LAST_NAME;
                transaction.TDL_PATIENT_NAME = expMests[0].TDL_PATIENT_NAME;
                transaction.TDL_PATIENT_NATIONAL_NAME = expMests[0].TDL_PATIENT_NATIONAL_NAME;
                transaction.TDL_PATIENT_PROVINCE_CODE = expMests[0].TDL_PATIENT_PROVINCE_CODE;
                transaction.TDL_PATIENT_WORK_PLACE = expMests[0].TDL_PATIENT_WORK_PLACE;
                transaction.TDL_TREATMENT_CODE = expMests[0].TDL_TREATMENT_CODE;
                transaction.TREATMENT_ID = expMests[0].TDL_TREATMENT_ID;
                transaction.BUYER_PHONE = string.IsNullOrWhiteSpace(expMests[0].TDL_PATIENT_PHONE) ? expMests[0].TDL_PATIENT_MOBILE : expMests[0].TDL_PATIENT_PHONE;
                transaction.BUYER_ADDRESS = expMests[0].TDL_PATIENT_ADDRESS;
                transaction.POS_CARD_HOLDER = data.PosCardHoder;
                transaction.POS_INVOICE = data.PosInvoice;
                transaction.POS_PAN = data.PosPan;
                transaction.POS_RESULT_JSON = data.PosResultJson;

                if (hisAccountBook.IS_NOT_GEN_TRANSACTION_ORDER == Constant.IS_TRUE)
                {
                    transaction.NUM_ORDER = data.TransactionNumOrder.Value;
                }

                if (!this.hisTransactionCreate.Create(transaction, null))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessBillGoods(HisExpMestSaleListSDO data, HIS_TRANSACTION transaction, List<V_HIS_EXP_MEST_MEDICINE> medicines, List<V_HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_BILL_GOODS> billGoods)
        {
            if (transaction != null && (IsNotNullOrEmpty(materials) || IsNotNullOrEmpty(medicines)))
            {
                billGoods = new List<HIS_BILL_GOODS>();

                if (IsNotNullOrEmpty(materials))
                {
                    List<HIS_BILL_GOODS> bgs = materials
                        .GroupBy(o => new {o.DESCRIPTION,
                            o.DISCOUNT,
                            o.EXPIRED_DATE,
                            o.MATERIAL_TYPE_NAME,
                            o.SERVICE_UNIT_NAME,
                            o.PACKAGE_NUMBER,
                            o.MANUFACTURER_NAME,
                            o.MATERIAL_TYPE_ID,
                            o.NATIONAL_NAME,
                            o.PRICE,
                            o.SERVICE_UNIT_ID,
                            o.VAT_RATIO
                        }).Select(o => new HIS_BILL_GOODS
                        {
                            AMOUNT = o.Sum(x => x.AMOUNT),
                            DESCRIPTION = o.Key.DESCRIPTION,
                            DISCOUNT = o.Key.DISCOUNT,
                            EXPIRED_DATE = o.Key.EXPIRED_DATE,
                            GOODS_NAME = o.Key.MATERIAL_TYPE_NAME,
                            GOODS_UNIT_NAME = o.Key.SERVICE_UNIT_NAME,
                            MANUFACTURER_NAME = o.Key.MANUFACTURER_NAME,
                            MATERIAL_TYPE_ID = o.Key.MATERIAL_TYPE_ID,
                            NATIONAL_NAME = o.Key.NATIONAL_NAME,
                            PACKAGE_NUMBER = o.Key.PACKAGE_NUMBER,
                            PRICE = o.Key.PRICE ?? 0,
                            SERVICE_UNIT_ID = o.Key.SERVICE_UNIT_ID,
                            VAT_RATIO = o.Key.VAT_RATIO
                        }).ToList();

                    billGoods.AddRange(bgs);
                }

                if (IsNotNullOrEmpty(medicines))
                {
                    List<HIS_BILL_GOODS> bgs = medicines
                        .GroupBy(o => new
                        {
                            o.DESCRIPTION,
                            o.DISCOUNT,
                            o.EXPIRED_DATE,
                            o.CONCENTRA,
                            o.MEDICINE_TYPE_NAME,
                            o.SERVICE_UNIT_NAME,
                            o.PACKAGE_NUMBER,
                            o.MANUFACTURER_NAME,
                            o.MEDICINE_TYPE_ID,
                            o.NATIONAL_NAME,
                            o.PRICE,
                            o.SERVICE_UNIT_ID,
                            o.VAT_RATIO
                        }).Select(o => new HIS_BILL_GOODS
                        {
                            AMOUNT = o.Sum(x => x.AMOUNT),
                            DESCRIPTION = o.Key.DESCRIPTION,
                            DISCOUNT = o.Key.DISCOUNT,
                            EXPIRED_DATE = o.Key.EXPIRED_DATE,
                            GOODS_NAME = o.Key.MEDICINE_TYPE_NAME,
                            GOODS_UNIT_NAME = o.Key.SERVICE_UNIT_NAME,
                            MANUFACTURER_NAME = o.Key.MANUFACTURER_NAME,
                            MEDICINE_TYPE_ID = o.Key.MEDICINE_TYPE_ID,
                            NATIONAL_NAME = o.Key.NATIONAL_NAME,
                            PACKAGE_NUMBER = o.Key.PACKAGE_NUMBER,
                            PRICE = o.Key.PRICE ?? 0,
                            CONCENTRA = o.Key.CONCENTRA,
                            SERVICE_UNIT_ID = o.Key.SERVICE_UNIT_ID,
                            VAT_RATIO = o.Key.VAT_RATIO
                        }).ToList();

                    billGoods.AddRange(bgs);
                }

                billGoods.ForEach(o => o.BILL_ID = transaction.ID);

                if (!this.hisBillGoodsCreate.CreateList(billGoods))
                {
                    throw new Exception("Khong tao duoc HisBillGoods cho giao dich thanh toan. Du lieu se bi Rollback.");
                }
            }
        }

        private decimal GetTotalPrice(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            decimal totalPrice = 0;
            totalPrice += IsNotNullOrEmpty(expMestMaterials) ? expMestMaterials.Sum(o => o.AMOUNT * (o.PRICE ?? 0) * (1 + (o.VAT_RATIO ?? 0))) : 0;
            totalPrice += IsNotNullOrEmpty(expMestMedicines) ? expMestMedicines.Sum(o => o.AMOUNT * (o.PRICE ?? 0) * (1 + (o.VAT_RATIO ?? 0))) : 0;
            return totalPrice;
        }

        private string GenerateEventLog(List<HIS_EXP_MEST> hisExpMests)
        {
            string log = "";
            try
            {
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    List<string> logs = new List<string>();
                    foreach (var item in hisExpMests)
                    {
                        string s = String.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, item.EXP_MEST_CODE);
                        if (item.PRESCRIPTION_ID.HasValue)
                        {
                            s = String.Format("{0}({1}: {2}. {3}: {4})", s, SimpleEventKey.TREATMENT_CODE, item.TDL_TREATMENT_CODE, SimpleEventKey.SERVICE_REQ_CODE, item.TDL_SERVICE_REQ_CODE);
                        }
                        s = String.Format("{0}, {1}: {2} - {3}", s, EventLog.Enum.Bacsi, item.TDL_PRESCRIPTION_REQ_LOGINNAME, item.TDL_PRESCRIPTION_REQ_USERNAME);

                        logs.Add(s);
                    }
                    log = String.Join(". ", logs);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                log = "";
            }
            return log;
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(List<HisExpMestResultSDO> hisExpMests)
        {
            try
            {
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    foreach (var sdo in hisExpMests)
                    {
                        HisExpMestResultSDO rsSdo = null;
                        if (new HisExpMestAutoProcess().Run(sdo.ExpMest, sdo.ExpMedicines, sdo.ExpMaterials, ref rsSdo))
                        {
                            if (rsSdo != null)
                            {
                                sdo.ExpMest.EXP_MEST_STT_ID = rsSdo.ExpMest.EXP_MEST_STT_ID;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollBack()
        {
            this.hisBillGoodsCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
            if (IsNotNullOrEmpty(this.medicineProcessors))
            {
                foreach (var processor in this.medicineProcessors)
                {
                    processor.Rollback();
                }
            }
            if (IsNotNullOrEmpty(this.materialProcessors))
            {
                foreach (var processor in this.materialProcessors)
                {
                    processor.Rollback();
                }
            }
            if (IsNotNullOrEmpty(this.expMestProcessors))
            {
                foreach (var processor in this.expMestProcessors)
                {
                    processor.Rollback();
                }
            }
        }
    }

}
