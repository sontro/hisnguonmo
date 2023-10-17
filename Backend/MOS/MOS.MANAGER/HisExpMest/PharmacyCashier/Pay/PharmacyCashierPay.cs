using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.Token;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.Pay
{
    public class PharmacyCashierPay : BusinessBase
    {
        private SaleExpMestProcessor saleExpMestProcessor;
        private SereServTransactionProcessor sereServTransactionProcessor;

        internal PharmacyCashierPay()
            : base()
        {
            this.Init();
        }

        internal PharmacyCashierPay(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.saleExpMestProcessor = new SaleExpMestProcessor(param);
            this.sereServTransactionProcessor = new SereServTransactionProcessor(param);
        }

        internal bool Run(PharmacyCashierSDO sdo, ref PharmacyCashierResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                PharmacyCashierPayCheck checker = new PharmacyCashierPayCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);

                WorkPlaceSDO workPlaceSdo = null;
                V_HIS_CASHIER_ROOM cashierRoom = null;
                V_HIS_ACCOUNT_BOOK recieptBook = null;
                V_HIS_ACCOUNT_BOOK invoiceBook = null;
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ prescription = null;
                List<HIS_SERE_SERV> sereServs = null;
                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                List<HIS_SERE_SERV_BILL> recieptSereServBills = null;
                List<HIS_SERE_SERV_BILL> invoiceSereServBills = null;
                List<HIS_BILL_GOODS> recieptBillGoods = null;
                List<HIS_BILL_GOODS> invoiceBillGoods = null;

                valid = valid && this.HasWorkPlaceInfo(sdo.WorkingRoomId, ref workPlaceSdo);
                valid = valid && checker.IsAllowing(workPlaceSdo, sdo, ref cashierRoom);
                valid = valid && checker.IsValidAccountBook(sdo, ref recieptBook, ref invoiceBook);
                valid = valid && checker.IsValidTreatment(sdo, ref treatment, ref prescription, ref sereServs);
                valid = valid && financePeriodChecker.HasNotFinancePeriod(workPlaceSdo.BranchId, Inventec.Common.DateTime.Get.Now().Value);
                //valid = valid && checker.IsNotHeinSereServ(sereServs);
                valid = valid && checker.IsValidInvoiceSereServPrice(sereServs, sdo.InvoiceSereServs, sdo.InvoiceAssignServices, workPlaceSdo, treatment, ref invoiceSereServBills, ref invoiceBillGoods);
                valid = valid && checker.IsValidRecieptSereServPrice(sereServs, sdo.RecieptSereServs, sdo.RecieptAssignServices, workPlaceSdo, treatment, ref recieptSereServBills, ref recieptBillGoods);
                valid = valid && checker.CheckDuplicateExpMest(sdo, prescription);

                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    List<HIS_TRANSACTION> serviceReciepts = null; //bien lai bhyt/vien phi va khong phai vacxin
                    List<HIS_TRANSACTION> serviceInvoices = null; //hoa don dich vu

                    if (!this.saleExpMestProcessor.Run(sdo, prescription, workPlaceSdo, ref expMest, ref expMestMedicines, ref expMestMaterials))
                    {
                        throw new Exception("Ket thuc xu ly, rollback du lieu");
                    }

                    if (!this.sereServTransactionProcessor.Run(sdo, branch, treatment, recieptBook, invoiceBook, recieptSereServBills, recieptBillGoods, invoiceSereServBills, invoiceBillGoods, ref serviceReciepts, ref serviceInvoices))
                    {
                        throw new Exception("Ket thuc xu ly, rollback du lieu");
                    }

                    resultData = new PharmacyCashierResultSDO();
                    resultData.ExpMest = expMest;
                    resultData.ExpMestMaterials = expMestMaterials;
                    resultData.ExpMestMedicines = expMestMedicines;
                    resultData.InvoiceSereServBills = invoiceSereServBills;
                    resultData.RecieptSereServBills = recieptSereServBills;
                    if (IsNotNullOrEmpty(serviceInvoices))
                    {
                        resultData.ServiceInvoices = new HisTransactionGet().GetByIds(serviceInvoices.Select(s => s.ID).ToList());
                    }
                    if (IsNotNullOrEmpty(serviceReciepts))
                    {
                        resultData.ServiceReciepts = new HisTransactionGet().GetByIds(serviceReciepts.Select(s => s.ID).ToList());
                    }
                    resultData.InvoiceBillGoods = invoiceBillGoods;
                    resultData.RecieptBillGoods = recieptBillGoods;
                    resultData.Treatment = treatment;
                    result = true;

                    ///Bo sung ghi log
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        private void Rollback()
        {
            this.sereServTransactionProcessor.Rollback();
            this.saleExpMestProcessor.Rollback();
        }
    }
}
