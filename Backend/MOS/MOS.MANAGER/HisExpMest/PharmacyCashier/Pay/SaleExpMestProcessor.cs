using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Sale.Create;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.Pay
{
    class SaleExpMestProcessor : BusinessBase
    {
        private HisExpMestSaleCreate hisExpMestSaleCreate;

        internal SaleExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal SaleExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestSaleCreate = new HisExpMestSaleCreate(param);
        }

        public bool Run(PharmacyCashierSDO sdo, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(sdo.Materials) || IsNotNullOrEmpty(sdo.Medicines))
                {
                    HisExpMestSaleSDO data = new HisExpMestSaleSDO();
                    data.ClientSessionKey = sdo.ClientSessionKey;
                    data.InstructionTime = serviceReq != null ? (long?)serviceReq.INTRUCTION_TIME : null;
                    data.IsHasNotDayDob = sdo.IsHasNotDayDob;
                    data.MaterialBeanIds = sdo.MaterialBeanIds;
                    data.Materials = sdo.Materials;
                    data.MedicineBeanIds = sdo.MedicineBeanIds;
                    data.Medicines = sdo.Medicines;
                    data.MediStockId = workPlace.MediStockId.Value;
                    data.PatientAddress = sdo.PatientAddress;
                    data.PatientDob = sdo.PatientDob;
                    data.PatientGenderId = sdo.PatientGenderId;
                    data.PatientName = sdo.PatientName;
                    data.PatientTypeId = sdo.PatientTypeId;
                    data.PrescriptionId = sdo.PrescriptionId;
                    data.PrescriptionReqLoginname = sdo.PrescriptionReqLoginname;
                    data.PrescriptionReqUsername = sdo.PrescriptionReqUsername;
                    data.ReqRoomId = sdo.WorkingRoomId;
                    data.TotalServiceAttachPrice = this.GetServiceAttachPrice(sdo);
                    HisExpMestResultSDO resultData = null;
                    if (!this.hisExpMestSaleCreate.Create(data, ref resultData))
                    {
                        throw new Exception("hisExpMestSaleCreate. Ket thuc nghiep vu");
                    }
                    expMest = resultData.ExpMest;
                    expMedicines = resultData.ExpMedicines;
                    expMaterials = resultData.ExpMaterials;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private decimal? GetServiceAttachPrice(PharmacyCashierSDO sdo)
        {
            decimal? result = null;
            if (IsNotNullOrEmpty(sdo.InvoiceAssignServices) || IsNotNullOrEmpty(sdo.InvoiceSereServs) || IsNotNullOrEmpty(sdo.RecieptAssignServices) || IsNotNullOrEmpty(sdo.RecieptSereServs))
            {
                decimal ssAmount = 0;
                decimal goodAmount = 0;
                if (IsNotNullOrEmpty(sdo.InvoiceSereServs))
                {
                    ssAmount = sdo.InvoiceSereServs.Sum(o => o.Price);
                }
                if (IsNotNullOrEmpty(sdo.InvoiceAssignServices))
                {
                    goodAmount = sdo.InvoiceAssignServices.Sum(o => (o.Amount * o.Price * (1 + o.VatRatio)));
                }
                if (IsNotNullOrEmpty(sdo.RecieptSereServs))
                {
                    ssAmount = sdo.RecieptSereServs.Sum(o => o.Price);
                }
                if (IsNotNullOrEmpty(sdo.RecieptAssignServices))
                {
                    goodAmount = sdo.RecieptAssignServices.Sum(o => (o.Amount * o.Price * (1 + o.VatRatio)));
                }
                result = (ssAmount + goodAmount);
            }
            return result;
        }

        public void Rollback()
        {
            this.hisExpMestSaleCreate.RollBack();
        }
    }
}
