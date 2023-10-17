using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System.Globalization;
using MOS.UTILITY;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisBillGoods;
using System.Text;

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    //Xuat ban
    partial class HisExpMestSaleCreate : BusinessBase
    {
        private HisExpMestAutoProcess hisExpMestAutoProcess;
        private HisExpMestProcessor hisExpMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisTransactionCreate hisTransactionCreate;

        private HisExpMestResultSDO recentResultSDO;

        internal HisExpMestSaleCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestSaleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisTransactionCreate = new HisTransactionCreate(param);
        }

        internal bool Create(HisExpMestSaleSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_ACCOUNT_BOOK accountBook = null;

                HisExpMestSaleCheck checker = new HisExpMestSaleCheck(param);
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsValidTransactionInfo(data, ref accountBook);
                if (valid)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                    HIS_EXP_MEST expMest = null;
                    List<string> sqls = new List<string>();
                    List<object> listParams = new List<object>();

                    //Tao exp_mest_material
                    if (!this.hisExpMestProcessor.Run(data, ref expMest))
                    {
                        throw new Exception("hisExpMestProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_material
                    if (!this.materialProcessor.Run(data.ClientSessionKey, data.PatientTypeId, data.MaterialBeanIds, data.Materials, expMest, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("ExpMestMaterialMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_medicine
                    if (!this.medicineProcessor.Run(data.ClientSessionKey, data.PatientTypeId, data.MedicineBeanIds, data.Medicines, expMest, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("ExpMestMedicineMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tinh tong tin cua phieu xuat ban
                    decimal totalPrice = this.GetTotalPrice(expMestMaterials, expMestMedicines);
                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(expMest, totalPrice, ref sqls);
                    
                    HIS_TRANSACTION transaction = null;
                    List<HIS_BILL_GOODS> billGoods = null;
                    if (accountBook != null && data.CreateBill)
                    {
                        this.ProcessTransactionBill(expMest, data, accountBook, totalPrice, ref transaction);
                        List<V_HIS_EXP_MEST_MEDICINE> vExpMestMedicines = expMestMedicines != null ? new HisExpMestMedicineGet().GetViewByIds(expMestMedicines.Select(o => o.ID).ToList()) : null;
                        List<V_HIS_EXP_MEST_MATERIAL> vExpMestMaterials = expMestMaterials != null ? new HisExpMestMaterialGet().GetViewByIds(expMestMaterials.Select(o => o.ID).ToList()) : null;
                        this.ProcessBillGoods(transaction, vExpMestMedicines, vExpMestMaterials, ref billGoods);
                    }
                    if (transaction != null)
                    {
                        this.ProcessUpdateExpMest(expMest, data, transaction, loginname, username, ref sqls, ref listParams);
                    }
                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && IsNotNullOrEmpty(listParams) && !DAOWorker.SqlDAO.Execute(sqls, listParams.ToArray()))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    else if (IsNotNullOrEmpty(sqls) && !IsNotNullOrEmpty(listParams) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    //Set sql Execute Update HIS_EXP_MEST
                    expMest.TDL_TOTAL_PRICE = totalPrice;
                    if (transaction != null)
                    {
                        expMest.BILL_ID = transaction.ID;
                        expMest.CASHIER_ROOM_ID = data.CashierRoomId;
                        expMest.CASHIER_LOGINNAME = loginname;
                        expMest.CASHIER_USERNAME = username;
                        expMest.PAY_FORM_ID = data.PayFormId;
                    }

                    this.ProcessAuto(expMest, expMestMedicines, expMestMaterials);

                    this.PassResult(expMest, expMestMaterials, expMestMedicines, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                    if (transaction != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisTransaction_TaoGiaoDichThanhToan, transaction.AMOUNT).TransactionCode(transaction.TRANSACTION_CODE).Run();
                    }
                    result = true;
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

        private void ProcessUpdateExpMest(HIS_EXP_MEST exp, HisExpMestSaleSDO data, HIS_TRANSACTION transaction, string loginName, string userName, ref List<string> sqls, ref List<object> listParams)
        {
            if (IsNotNull(exp))
            {
                string updateSql = null;
                StringBuilder str = new StringBuilder().Append("UPDATE HIS_EXP_MEST SET ");
                List<string> listStr = new List<string>();
                if (transaction != null)
                {
                    listStr.Add(string.Format("BILL_ID = {0}", transaction.ID));
                }
                if (data.CashierRoomId.HasValue)
                {
                    listStr.Add(string.Format("CASHIER_ROOM_ID = {0}", data.CashierRoomId.Value));
                }
                if (!String.IsNullOrEmpty(loginName))
	            {
                    listStr.Add(string.Format("CASHIER_LOGINNAME = :param{0}", listParams.Count + 1));
                    listParams.Add(loginName);
	            }
                if (!String.IsNullOrEmpty(userName))
                {
                    listStr.Add(string.Format("CASHIER_USERNAME = :param{0}", listParams.Count + 1));
                    listParams.Add(userName);
                }
                if (data.PayFormId.HasValue)
                {
                    listStr.Add(string.Format("PAY_FORM_ID = {0}", data.PayFormId.Value));
                }
                str.Append(String.Join(", ", listStr));
                str.Append(string.Format(" WHERE ID = {0}", exp.ID));
                updateSql = str.ToString();

                if (!string.IsNullOrWhiteSpace(updateSql))
                {
                    sqls.Add(updateSql);
                }
            }
        }

        private void ProcessTdlTotalPrice(HIS_EXP_MEST expMest, decimal? totalPrice, ref List<string> sqls)
        {
            if (totalPrice.HasValue)
            {
                string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0}, TRANSFER_AMOUNT = NULL WHERE ID = {1}", totalPrice.Value.ToString("G27", CultureInfo.InvariantCulture), expMest.ID);
                sqls.Add(updateSql);
            }
        }

        private void ProcessTransactionBill(HIS_EXP_MEST expMest, HisExpMestSaleSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook, decimal totalPrice, ref HIS_TRANSACTION transaction)
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
                transaction.TDL_PATIENT_ADDRESS = expMest.TDL_PATIENT_ADDRESS;
                transaction.TDL_PATIENT_CODE = expMest.TDL_PATIENT_CODE;
                transaction.TDL_PATIENT_COMMUNE_CODE = expMest.TDL_PATIENT_COMMUNE_CODE;
                transaction.TDL_PATIENT_DISTRICT_CODE = expMest.TDL_PATIENT_DISTRICT_CODE;
                transaction.TDL_PATIENT_DOB = expMest.TDL_PATIENT_DOB;
                transaction.TDL_PATIENT_FIRST_NAME = expMest.TDL_PATIENT_FIRST_NAME;
                transaction.TDL_PATIENT_GENDER_ID = expMest.TDL_PATIENT_GENDER_ID;
                transaction.TDL_PATIENT_GENDER_NAME = expMest.TDL_PATIENT_GENDER_NAME;
                transaction.TDL_PATIENT_ID = expMest.TDL_PATIENT_ID;
                transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                transaction.TDL_PATIENT_LAST_NAME = expMest.TDL_PATIENT_LAST_NAME;
                transaction.TDL_PATIENT_NAME = expMest.TDL_PATIENT_NAME;
                transaction.TDL_PATIENT_NATIONAL_NAME = expMest.TDL_PATIENT_NATIONAL_NAME;
                transaction.TDL_PATIENT_PROVINCE_CODE = expMest.TDL_PATIENT_PROVINCE_CODE;
                transaction.TDL_PATIENT_WORK_PLACE = expMest.TDL_PATIENT_WORK_PLACE;
                transaction.TDL_TREATMENT_CODE = expMest.TDL_TREATMENT_CODE;
                transaction.TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                transaction.BUYER_PHONE = string.IsNullOrWhiteSpace(expMest.TDL_PATIENT_PHONE) ? expMest.TDL_PATIENT_MOBILE : expMest.TDL_PATIENT_PHONE;
                transaction.BUYER_ADDRESS = expMest.TDL_PATIENT_ADDRESS;
                transaction.BUYER_NAME = expMest.TDL_PATIENT_NAME;
                transaction.BUYER_ACCOUNT_NUMBER = expMest.TDL_PATIENT_ACCOUNT_NUMBER;
                transaction.BUYER_ORGANIZATION = expMest.TDL_PATIENT_WORK_PLACE;
                transaction.BUYER_TAX_CODE = expMest.TDL_PATIENT_TAX_CODE;

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

        private void ProcessBillGoods(HIS_TRANSACTION transaction, List<V_HIS_EXP_MEST_MEDICINE> medicines, List<V_HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_BILL_GOODS> billGoods)
        {
            if (transaction != null && (IsNotNullOrEmpty(materials) || IsNotNullOrEmpty(medicines)))
            {
                billGoods = new List<HIS_BILL_GOODS>();

                if (IsNotNullOrEmpty(materials))
                {
                    List<HIS_BILL_GOODS> bgs = materials
                        .GroupBy(o => new
                        {
                            o.DESCRIPTION,
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

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials)
        {
            try
            {
                this.hisExpMestAutoProcess.Run(expMest, medicines, materials, ref this.recentResultSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, List<HIS_EXP_MEST_MEDICINE> expMedicines, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO != null)
            {
                resultData = this.recentResultSDO;
            }
            else
            {
                resultData = new HisExpMestResultSDO();
                resultData.ExpMest = expMest;
                resultData.ExpMaterials = expMaterials;
                resultData.ExpMedicines = expMedicines;
            }
        }

        internal void RollBack()
        {
            this.hisBillGoodsCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
        }
    }
}
