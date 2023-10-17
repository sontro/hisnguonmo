using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Lock;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.Pay
{
    class SereServTransactionProcessor : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisSereServBillCreate hisSereServBillCreate;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisTreatmentLock hisTreatmentLock;

        internal SereServTransactionProcessor()
            : base()
        {
            this.Init();
        }

        internal SereServTransactionProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSereServBillCreate = new HisSereServBillCreate(param);
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisTreatmentLock = new HisTreatmentLock(param);
        }

        internal bool Run(PharmacyCashierSDO sdo, HIS_BRANCH branch, HIS_TREATMENT treatment, V_HIS_ACCOUNT_BOOK recieptBook, V_HIS_ACCOUNT_BOOK invoiceBook, List<HIS_SERE_SERV_BILL> recieptSereServBills, List<HIS_BILL_GOODS> recieptBillGoods, List<HIS_SERE_SERV_BILL> invoiceSereServBills, List<HIS_BILL_GOODS> invoiceBillGoods, ref List<HIS_TRANSACTION> reciepts, ref List<HIS_TRANSACTION> invoices)
        {
            try
            {
                if ((IsNotNullOrEmpty(recieptSereServBills) || IsNotNullOrEmpty(recieptBillGoods) || IsNotNullOrEmpty(invoiceSereServBills) || IsNotNullOrEmpty(invoiceBillGoods)) && treatment != null)
                {
                    Dictionary<TranWithBook, TranNumsData> dicTrans = PrepareData(sdo, recieptBook, invoiceBook, recieptSereServBills, recieptBillGoods, invoiceSereServBills, invoiceBillGoods);
                    this.ProcessTransaction(dicTrans, sdo, branch, treatment, invoiceBook, ref reciepts, ref invoices);
                    this.ProcessSereServBill(sdo, dicTrans, recieptSereServBills, invoiceSereServBills);
                    this.ProcessBillGoods(dicTrans, recieptBillGoods, invoiceBillGoods);
                    this.ProcessTreatment(sdo, treatment);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private void ProcessTransaction(Dictionary<TranWithBook, TranNumsData> dicTrans, PharmacyCashierSDO sdo, HIS_BRANCH branch, HIS_TREATMENT treatment, V_HIS_ACCOUNT_BOOK invoiceBook, ref List<HIS_TRANSACTION> reciepts, ref List<HIS_TRANSACTION> invoices)
        {
            long time = Inventec.Common.DateTime.Get.Now().Value;
            List<HIS_TRANSACTION> list = new List<HIS_TRANSACTION>();
            List<HIS_TRANSACTION> listReciept = new List<HIS_TRANSACTION>();
            List<HIS_TRANSACTION> listInvoice = new List<HIS_TRANSACTION>();
            foreach (var dic in dicTrans)
            {
                decimal ssAmount = 0;
                decimal goodAmount = 0;
                if (IsNotNullOrEmpty(dic.Value.SereServBills))
                {
                    ssAmount = dic.Value.SereServBills.Sum(o => o.PRICE);
                }
                if (IsNotNullOrEmpty(dic.Value.BillGoods))
                {
                    goodAmount = dic.Value.BillGoods.Sum(o => (o.AMOUNT * o.PRICE * (1 + (o.VAT_RATIO ?? 0))));
                }

                HIS_TRANSACTION hisTransaction = dic.Key.Transaction;
                hisTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                hisTransaction.SALE_TYPE_ID = null;
                hisTransaction.AMOUNT = ssAmount + goodAmount;
                hisTransaction.SERE_SERV_AMOUNT = ssAmount;
                hisTransaction.TREATMENT_ID = treatment.ID;
                hisTransaction.ACCOUNT_BOOK_ID = dic.Key.AccountBook.ID;
                hisTransaction.BILL_TYPE_ID = dic.Key.AccountBook.BILL_TYPE_ID;
                hisTransaction.CASHIER_ROOM_ID = sdo.CashierRoomId;
                hisTransaction.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                hisTransaction.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                //hisTransaction.NUM_ORDER = sdo.InvoiceNumOrder.HasValue ? sdo.InvoiceNumOrder.Value : 0;
                hisTransaction.PAY_FORM_ID = sdo.PayFormId;
                hisTransaction.SELLER_ACCOUNT_NUMBER = branch.ACCOUNT_NUMBER;
                hisTransaction.SELLER_ADDRESS = branch.ADDRESS;
                hisTransaction.SELLER_NAME = branch.BRANCH_NAME;
                hisTransaction.SELLER_PHONE = branch.PHONE;
                hisTransaction.SELLER_TAX_CODE = branch.TAX_CODE;
                hisTransaction.KC_AMOUNT = null;
                hisTransaction.TRANSACTION_TIME = time;
                list.Add(hisTransaction);
                if (dic.Key.AccountBook == invoiceBook)
                {
                    listInvoice.Add(hisTransaction);
                }
                else
                {
                    listReciept.Add(hisTransaction);
                }
            }
            if (!this.hisTransactionCreate.CreateList(list, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            invoices = listInvoice;
            reciepts = listReciept;
        }

        private void ProcessSereServBill(PharmacyCashierSDO sdo, Dictionary<TranWithBook, TranNumsData> dicTrans, List<HIS_SERE_SERV_BILL> recieptSereServBills, List<HIS_SERE_SERV_BILL> invoiceSereServBills)
        {
            if (IsNotNullOrEmpty(recieptSereServBills) || IsNotNullOrEmpty(invoiceSereServBills))
            {
                foreach (var dic in dicTrans)
                {
                    if (IsNotNullOrEmpty(dic.Value.SereServBills))
                    {
                        dic.Value.SereServBills.ForEach(o =>
                            {
                                o.BILL_ID = dic.Key.Transaction.ID;
                                o.TDL_BILL_TYPE_ID = dic.Key.AccountBook.BILL_TYPE_ID;
                                o.TDL_TREATMENT_ID = dic.Key.Transaction.TREATMENT_ID.Value;
                            });
                    }
                }
                List<HIS_SERE_SERV_BILL> createds = new List<HIS_SERE_SERV_BILL>();
                if (IsNotNullOrEmpty(recieptSereServBills))
                {
                    createds.AddRange(recieptSereServBills);
                }
                if (IsNotNullOrEmpty(invoiceSereServBills))
                {
                    createds.AddRange(invoiceSereServBills);
                }
                if (!this.hisSereServBillCreate.CreateList(createds))
                {
                    throw new Exception("Tao sere_serv_bill that bai. Du lieu se bi rollback");
                }
            }
        }

        private void ProcessBillGoods(Dictionary<TranWithBook, TranNumsData> dicTrans, List<HIS_BILL_GOODS> recieptBillGoods, List<HIS_BILL_GOODS> invoiceBillGoods)
        {
            if (IsNotNullOrEmpty(recieptBillGoods) || IsNotNullOrEmpty(invoiceBillGoods))
            {
                foreach (var dic in dicTrans)
                {
                    if (IsNotNullOrEmpty(dic.Value.BillGoods))
                    {
                        dic.Value.BillGoods.ForEach(o =>
                        {
                            o.BILL_ID = dic.Key.Transaction.ID;
                        });
                    }
                }
                List<HIS_BILL_GOODS> createds = new List<HIS_BILL_GOODS>();
                if (IsNotNullOrEmpty(recieptBillGoods))
                {
                    createds.AddRange(recieptBillGoods);
                }
                if (IsNotNullOrEmpty(invoiceBillGoods))
                {
                    createds.AddRange(invoiceBillGoods);
                }
                if (!this.hisBillGoodsCreate.CreateList(createds))
                {
                    throw new Exception("Tao bill_goods that bai. Du lieu se bi rollback");
                }
            }
        }

        private void ProcessTreatment(PharmacyCashierSDO sdo, HIS_TREATMENT treatment)
        {
            if (HisTreatmentCFG.AUTO_LOCK_AFTER_BILL && treatment != null)
            {
                V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet(param).GetFeeView1ById(treatment.ID);

                //Neu benh nhan da ket thuc dieu tri va chua duyet khoa tai chinh
                if (treatmentFee != null && treatmentFee.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE
                    && treatmentFee.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    //So tien can thu them
                    decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);

                    //Neu so tien can thu them = 0 thi tu dong duyet khoa
                    if (unpaid <= Constant.PRICE_DIFFERENCE)
                    {
                        HisTreatmentLockSDO treatmentLockSDO = new HisTreatmentLockSDO();
                        treatmentLockSDO.RequestRoomId = sdo.WorkingRoomId;

                        //Trong truong hop co cau hinh lay gio duyet khoa vien phi theo gio ra vien trong truong hop tu dong
                        if (HisTreatmentCFG.IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO
                            && treatmentFee.OUT_TIME.HasValue)
                        {
                            treatmentLockSDO.FeeLockTime = treatmentFee.OUT_TIME.Value;
                        }
                        else
                        {
                            treatmentLockSDO.FeeLockTime = Inventec.Common.DateTime.Get.Now().Value;
                        }

                        treatmentLockSDO.TreatmentId = treatmentFee.ID;

                        if (!new HisTreatmentLock(param).Run(treatmentLockSDO, treatment, sdo.CashierRoomId, ref treatment))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_KhongTuDongDuyetHoSoVienPhi);
                            LogSystem.Warn("Tu dong duyet khoa vien phi that bai: " + param.GetMessage());
                        }
                    }
                }
            }
        }

        private Dictionary<TranWithBook, TranNumsData> PrepareData(PharmacyCashierSDO sdo, V_HIS_ACCOUNT_BOOK recieptBook, V_HIS_ACCOUNT_BOOK invoiceBook, List<HIS_SERE_SERV_BILL> recieptSereServBills, List<HIS_BILL_GOODS> recieptBillGoods, List<HIS_SERE_SERV_BILL> invoiceSereServBills, List<HIS_BILL_GOODS> invoiceBillGoods)
        {
            Dictionary<TranWithBook, TranNumsData> dicTrans = new Dictionary<TranWithBook, TranNumsData>();
            if (IsNotNullOrEmpty(invoiceSereServBills) || IsNotNullOrEmpty(invoiceBillGoods))
            {
                long max = 0;
                long num = this.GetNumTransByMaxItemPerTrans(invoiceSereServBills, invoiceBillGoods, invoiceBook, ref max);
                if (num == 1)
                {
                    TranWithBook tb = new TranWithBook();
                    tb.Transaction = new HIS_TRANSACTION();
                    tb.Transaction.NUM_ORDER = sdo.InvoiceNumOrder ?? 0;
                    tb.AccountBook = invoiceBook;
                    dicTrans[tb] = new TranNumsData()
                    {
                        SereServBills = invoiceSereServBills,
                        BillGoods = invoiceBillGoods,
                    };
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        List<HIS_SERE_SERV_BILL> ssBills = null;
                        List<HIS_BILL_GOODS> goods = null;
                        int take = 0;
                        int countSS = 0;

                        TranWithBook tb = new TranWithBook();
                        tb.Transaction = new HIS_TRANSACTION();
                        tb.Transaction.NUM_ORDER = sdo.InvoiceNumOrder ?? 0;
                        tb.AccountBook = invoiceBook;

                        int start = i * (int)max;
                        int limit = (int)max;
                        if (IsNotNullOrEmpty(invoiceSereServBills))
                        {
                            countSS = invoiceSereServBills.Count;
                            if (countSS > start)
                            {
                                ssBills = invoiceSereServBills.Skip(start).Take(limit).ToList();
                                take = ssBills.Count;
                            }
                        }

                        if (IsNotNullOrEmpty(invoiceBillGoods) && take < max)
                        {
                            start = start - countSS;
                            limit = (int)max - take;
                            int count = invoiceBillGoods.Count;
                            if (count > start)
                            {
                                goods = invoiceBillGoods.Skip(start).Take(limit).ToList();
                            }
                        }
                        dicTrans[tb] = new TranNumsData()
                        {
                            SereServBills = ssBills,
                            BillGoods = goods
                        };
                    }
                }
            }

            if (IsNotNullOrEmpty(recieptSereServBills) || IsNotNullOrEmpty(recieptBillGoods))
            {
                long max = 0;
                long num = this.GetNumTransByMaxItemPerTrans(recieptSereServBills, recieptBillGoods, recieptBook, ref max);
                if (num == 1)
                {
                    TranWithBook tb = new TranWithBook();
                    tb.Transaction = new HIS_TRANSACTION();
                    tb.Transaction.NUM_ORDER = sdo.RecieptNumOrder ?? 0;
                    tb.AccountBook = recieptBook;
                    dicTrans[tb] = new TranNumsData()
                    {
                        SereServBills = recieptSereServBills,
                        BillGoods = recieptBillGoods,
                    };
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        List<HIS_SERE_SERV_BILL> ssBills = null;
                        List<HIS_BILL_GOODS> goods = null;
                        int take = 0;
                        int countSS = 0;

                        TranWithBook tb = new TranWithBook();
                        tb.Transaction = new HIS_TRANSACTION();
                        tb.Transaction.NUM_ORDER = sdo.RecieptNumOrder ?? 0;
                        tb.AccountBook = recieptBook;

                        int start = i * (int)max;
                        int limit = (int)max;
                        if (IsNotNullOrEmpty(recieptSereServBills))
                        {
                            countSS = recieptSereServBills.Count;
                            if (countSS > start)
                            {
                                ssBills = recieptSereServBills.Skip(start).Take(limit).ToList();
                                take = ssBills.Count;
                            }
                        }

                        if (IsNotNullOrEmpty(recieptBillGoods) && take < max)
                        {
                            start = start - countSS;
                            limit = (int)max - take;
                            int count = recieptBillGoods.Count;
                            if (count > start)
                            {
                                goods = recieptBillGoods.Skip(start).Take(limit).ToList();
                            }
                        }
                        dicTrans[tb] = new TranNumsData()
                        {
                            SereServBills = ssBills,
                            BillGoods = goods
                        };
                    }
                }
            }
            return dicTrans;
        }

        private long GetNumTransByMaxItemPerTrans(List<HIS_SERE_SERV_BILL> sereServBills, List<HIS_BILL_GOODS> billGoods, V_HIS_ACCOUNT_BOOK book, ref long max)
        {
            long num = 1;
            if (book.MAX_ITEM_NUM_PER_TRANS.HasValue && book.MAX_ITEM_NUM_PER_TRANS.Value > 0)
            {
                max = book.MAX_ITEM_NUM_PER_TRANS.Value;
                long count = 0;
                count += (sereServBills != null ? sereServBills.Count : 0);
                count += (billGoods != null ? billGoods.Count : 0);
                long mod = count % max;
                if (mod > 0)
                {
                    num = (count / max) + 1;
                }
                else
                {
                    num = (count / max);
                }
            }
            return num;
        }

        internal void Rollback()
        {
            this.hisBillGoodsCreate.RollbackData();
            this.hisSereServBillCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }

    class TranNumsData
    {
        public List<HIS_SERE_SERV_BILL> SereServBills { get; set; }
        public List<HIS_BILL_GOODS> BillGoods { get; set; }
    }

    class TranWithBook
    {
        public HIS_TRANSACTION Transaction { get; set; }
        public V_HIS_ACCOUNT_BOOK AccountBook { get; set; }
    }
}
