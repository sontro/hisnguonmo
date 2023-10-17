using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMaterialType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Create
{
    class ImpMestProcessor : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_MATERIAL recentHisMaterial;

        private HisImpMestCreate hisImpMestCreate;

        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;

        internal ImpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
        }

        internal bool Run(HisPackingCreateSDO data, HIS_DISPENSE hisDispense, List<HIS_EXP_MEST_MATERIAL> materials, ref HIS_IMP_MEST impMest, ref HIS_IMP_MEST_MATERIAL impMestMaterial)
        {
            bool result = false;
            try
            {
                this.ProcessHisImpMest(data, hisDispense);
                this.ProcessHisMaterial(data, materials);
                this.ProcessHisImpMestMaterial(ref impMestMaterial);
                impMest = this.recentHisImpMest;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisPackingCreateSDO data, HIS_DISPENSE hisDispense)
        {
            long time = Inventec.Common.DateTime.Get.Now().Value;
            HIS_IMP_MEST impMest = new HIS_IMP_MEST();
            impMest.DISPENSE_ID = hisDispense.ID;
            impMest.TDL_DISPENSE_CODE = hisDispense.DISPENSE_CODE;
            impMest.MEDI_STOCK_ID = data.MediStockId;
            impMest.REQ_ROOM_ID = data.RequestRoomId;
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT;
            if (!this.hisImpMestCreate.Create(impMest))
            {
                throw new Exception("hisImpMestCreate. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = impMest;
        }

        private void ProcessHisMaterial(HisPackingCreateSDO data, List<HIS_EXP_MEST_MATERIAL> materials)
        {
            HIS_MATERIAL_TYPE materialType = new HisMaterialTypeGet().GetById(data.MaterialTypeId);

            HIS_MATERIAL material = new HIS_MATERIAL();
            material.AMOUNT = data.Amount;
            material.MATERIAL_TYPE_ID = data.MaterialTypeId;
            material.EXPIRED_DATE = data.ExpiredDate;
            material.TDL_BID_NUMBER = data.HeinDocumentNumber;
            material.PACKAGE_NUMBER = data.PackageNumber;
            material.IS_SALE_EQUAL_IMP_PRICE = null;
            material.TDL_SERVICE_ID = materialType.SERVICE_ID;
            material.INTERNAL_PRICE = materialType.INTERNAL_PRICE;
            material.IS_PREGNANT = Constant.IS_TRUE;
            this.ProcessImpPrice(material, materials);
            HisMaterialUtil.SetTdl(material, materialType);
            HisMaterialUtil.SetTdl(material, this.recentHisImpMest);

            if (!this.hisMaterialCreate.Create(material))
            {
                throw new Exception("hisMaterialCreate. Ket thuc nghiep vu");
            }
            this.recentHisMaterial = material;

            if (IsNotNullOrEmpty(data.MaterialPaties))
            {
                data.MaterialPaties.ForEach(o => o.MATERIAL_ID = material.ID);
                if (!this.hisMaterialPatyCreate.CreateList(data.MaterialPaties))
                {
                    throw new Exception("hisMaterialPatyCreate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessHisImpMestMaterial(ref HIS_IMP_MEST_MATERIAL material)
        {
            HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
            impMestMaterial.AMOUNT = this.recentHisMaterial.AMOUNT;
            impMestMaterial.IMP_MEST_ID = this.recentHisImpMest.ID;
            impMestMaterial.MATERIAL_ID = this.recentHisMaterial.ID;
            impMestMaterial.PRICE = this.recentHisMaterial.IMP_PRICE;
            impMestMaterial.VAT_RATIO = this.recentHisMaterial.IMP_VAT_RATIO;

            if (!this.hisImpMestMaterialCreate.Create(impMestMaterial))
            {
                throw new Exception("hisImpMestMaterialCreate. Ket thuc nghiep vu");
            }
            material = impMestMaterial;
        }

        private void ProcessImpPrice(HIS_MATERIAL material, List<HIS_EXP_MEST_MATERIAL> materials)
        {
            decimal totalPrice = 0;
            if (IsNotNullOrEmpty(materials))
            {
                totalPrice += materials.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
            }

            material.IMP_PRICE = totalPrice / material.AMOUNT;
            material.IMP_VAT_RATIO = 0;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisImpMestMaterialCreate.RollbackData();
                this.hisMaterialPatyCreate.RollbackData();
                this.hisMaterialCreate.RollbackData();
                this.hisImpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
