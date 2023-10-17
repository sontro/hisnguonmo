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
using System.Collections.Generic;
using System.Globalization;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update.SaleExpMest
{
    /// <summary>
    /// Tu dong tao phieu xuat ban khi ke ngoai kho
    /// </summary>
    partial class SaleExpMestUpdate : BusinessBase
    {
        private HisExpMestAutoProcess hisExpMestAutoProcess;
        private HisExpMestProcessor hisExpMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal SaleExpMestUpdate()
            : base()
        {
            this.Init();
        }

        internal SaleExpMestUpdate(CommonParam paramCreate)
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

        internal bool Run(HIS_SERVICE_REQ data, long mediStockId, long patientTypeId, List<PresOutStockMatySDO> serviceReqMaties, List<PresOutStockMetySDO> serviceReqMeties, HIS_EXP_MEST saleExpMest, ref List<string> sqls)
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
                    if (!this.hisExpMestProcessor.Run(data, serviceReqMaties, serviceReqMeties, mediStockId, patientTypeId, saleExpMest))
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
                    this.ProcessTdlTotalPrice(saleExpMest, expMestMaterials, expMestMedicines, ref sqls);

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

        private void ProcessTdlTotalPrice(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            try
            {
                decimal? totalPrice = null;
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    decimal matePrice = 0;
                    foreach (HIS_EXP_MEST_MATERIAL mate in expMestMaterials)
                    {
                        if (!mate.PRICE.HasValue)
                        {
                            continue;
                        }
                        matePrice += (mate.AMOUNT * mate.PRICE.Value * (1 + (mate.VAT_RATIO ?? 0)));
                    }
                    if (matePrice > 0)
                    {
                        totalPrice = matePrice;
                    }
                }
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    decimal mediPrice = 0;
                    foreach (HIS_EXP_MEST_MEDICINE medi in expMestMedicines)
                    {
                        if (!medi.PRICE.HasValue)
                        {
                            continue;
                        }
                        mediPrice += (medi.AMOUNT * medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                    }
                    if (mediPrice > 0)
                    {
                        totalPrice = (totalPrice ?? 0) + mediPrice;
                    }
                }
                if (totalPrice.HasValue)
                {
                    string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0} WHERE ID = {1}", totalPrice.Value.ToString("G27", CultureInfo.InvariantCulture), expMest.ID);
                    sqls.Add(updateSql);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void RollBack()
        {
            this.hisExpMestProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
        }
    }
}
