using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create.SaleExpMest
{
    /// <summary>
    /// Tu dong tao phieu xuat ban khi ke ngoai kho
    /// </summary>
    partial class SaleExpMestCreate : BusinessBase
    {
        private HisExpMestAutoProcess hisExpMestAutoProcess;
        private HisExpMestProcessor hisExpMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal SaleExpMestCreate()
            : base()
        {
            this.Init();
        }

        internal SaleExpMestCreate(CommonParam paramCreate)
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
        }

        internal bool Run(HIS_SERVICE_REQ data, long mediStockId, long patientTypeId, List<PresOutStockMatySDO> serviceReqMaties, List<PresOutStockMetySDO> serviceReqMeties, ref HIS_EXP_MEST saleExpMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                bool valid = true;
                if (valid)
                {
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    //Tao exp_mest
                    if (!this.hisExpMestProcessor.Run(data, serviceReqMaties, serviceReqMeties, mediStockId, patientTypeId, ref saleExpMest))
                    {
                        throw new Exception("hisExpMestProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_material
                    if (!this.materialProcessor.Run(serviceReqMaties, saleExpMest, patientTypeId, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("ExpMestMaterialMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_medicine
                    if (!this.medicineProcessor.Run(serviceReqMeties, saleExpMest, patientTypeId, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("ExpMestMedicineMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Set TDL_TOTAL_PRICE
                    HisServiceReqPresUtil.SqlUpdateExpMest(saleExpMest, expMestMaterials, expMestMedicines, ref sqls);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollBack();
                result = false;
            }
            return result;
        }

        internal void RollBack()
        {
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
        }
    }
}
