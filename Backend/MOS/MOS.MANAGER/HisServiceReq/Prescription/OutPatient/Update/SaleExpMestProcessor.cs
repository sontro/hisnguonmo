using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create.SaleExpMest;
using MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update.SaleExpMest;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update
{
    public class SaleExpMestProcessor: BusinessBase
    {
        private SaleExpMestUpdate saleExpMestUpdate;

        internal SaleExpMestProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.saleExpMestUpdate = new SaleExpMestUpdate(param);
        }

        internal bool Run(OutPatientPresSDO data, HIS_SERVICE_REQ outStockServiceReq, HIS_EXP_MEST saleExpMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST
                    && HisServiceReqCFG.DEFAULT_DRUG_STORE_PATIENT_TYPE_ID > 0
                    && outStockServiceReq != null && data.DrugStoreId.HasValue
                    && (IsNotNullOrEmpty(data.ServiceReqMeties) || IsNotNullOrEmpty(data.ServiceReqMaties)))
                {
                    //Khong lay cac thuoc/vat tu ngoai vien
                    List<PresOutStockMetySDO> meties = data.ServiceReqMeties != null ? data.ServiceReqMeties.Where(o => HisMedicineTypeCFG.DATA.Exists(t => t.IS_OUT_HOSPITAL != Constant.IS_TRUE && t.ID == o.MedicineTypeId)).ToList() : null;

                    List<PresOutStockMatySDO> maties = data.ServiceReqMaties != null ? data.ServiceReqMaties.Where(o => HisMaterialTypeCFG.DATA.Exists(t => t.IS_OUT_HOSPITAL != Constant.IS_TRUE && t.ID == o.MaterialTypeId)).ToList() : null;

                    if (IsNotNullOrEmpty(meties) || IsNotNullOrEmpty(maties))
                    {
                        result = this.saleExpMestUpdate.Run(outStockServiceReq, data.DrugStoreId.Value, HisServiceReqCFG.DEFAULT_DRUG_STORE_PATIENT_TYPE_ID, maties, meties, saleExpMest, ref sqls);
                    }
                    else
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
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

        internal void Rollback()
        {
            this.saleExpMestUpdate.RollBack();
        }
    }
}
