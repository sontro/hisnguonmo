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
using AutoMapper;
using MOS.MANAGER.EventLogUtil;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.ExpInvoice
{
    public class PharmacyCashierExpInvoice: BusinessBase
    {
        ExpMestTransactionProcessor expMestTransactionProcessor;

        internal PharmacyCashierExpInvoice()
            : base()
        {
            this.Init();
        }

        internal PharmacyCashierExpInvoice(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestTransactionProcessor = new ExpMestTransactionProcessor(param);
        }

        internal bool Run(PharmacyCashierExpInvoiceSDO sdo, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                PharmacyCashierExpInvoiceCheck checker = new PharmacyCashierExpInvoiceCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);

                WorkPlaceSDO workPlaceSdo = null;
                V_HIS_CASHIER_ROOM cashierRoom = null;
                V_HIS_ACCOUNT_BOOK invoiceBook = null;
                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                
                valid = valid && this.HasWorkPlaceInfo(sdo.WorkingRoomId, ref workPlaceSdo);
                valid = valid && checker.IsAllowing(workPlaceSdo, sdo, ref cashierRoom);
                valid = valid && checker.IsValidAccountBook(sdo, ref invoiceBook);
                valid = valid && financePeriodChecker.HasNotFinancePeriod(workPlaceSdo.BranchId, Inventec.Common.DateTime.Get.Now().Value);
                valid = valid && checker.IsValidExpMest(sdo, ref expMest, ref expMestMedicines, ref expMestMaterials);

                if (valid)
                {
                    HIS_TRANSACTION expMestInvoice = null;
                    List<HIS_BILL_GOODS> billGoods = null;

                    if (!this.expMestTransactionProcessor.Run(sdo, invoiceBook, expMest, expMestMedicines, expMestMaterials, ref expMestInvoice, ref billGoods))
                    {
                        throw new Exception("Ket thuc xu ly, rollback du lieu");
                    }

                    result = true;
                    resultData = new HisTransactionGet().GetById(expMestInvoice.ID);//truy van lai de lay thong tin num_order trong truong hop num_order tu sinh
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_XuatHoaDonTheoBienLai, resultData.TRANSACTION_CODE, expMestInvoice.TRANSACTION_CODE).Run();
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

    }
}
