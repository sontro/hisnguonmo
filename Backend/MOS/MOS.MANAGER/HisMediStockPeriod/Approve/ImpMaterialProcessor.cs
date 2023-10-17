using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMaterialType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class ImpMaterialProcessor : BusinessBase
    {
        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;

        internal ImpMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
        }

        internal bool Run(List<HisMaterialWithPatySDO> inveMaterials, HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(inveMaterials))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && checker.IsNotExistStopImpMaterialType(inveMaterials);
                    if (!valid)
                    {
                        return false;
                    }

                    List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                    List<HisMaterialWithPatySDO> output = new List<HisMaterialWithPatySDO>();
                    foreach (HisMaterialWithPatySDO impMaterial in inveMaterials)
                    {
                        HisMaterialWithPatySDO inserted = new HisMaterialWithPatySDO();

                        //insert thong tin material
                        HIS_MATERIAL material = impMaterial.Material;

                        material.SUPPLIER_ID = impMest.SUPPLIER_ID;
                        material.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;

                        if (!this.hisMaterialCreate.Create(material))
                        {
                            throw new Exception("Tao HIS_MATERIAL that bai");
                        }
                        inserted.Material = material;

                        //neu co thong tin chinh sach gia thi insert thong tin gia ban cho tung material
                        if (IsNotNullOrEmpty(impMaterial.MaterialPaties))
                        {
                            List<HIS_MATERIAL_PATY> materialPaties = impMaterial.MaterialPaties;
                            materialPaties.ForEach(o => o.MATERIAL_ID = material.ID);

                            if (!this.hisMaterialPatyCreate.CreateList(materialPaties))
                            {
                                throw new Exception("Tao HIS_MATERIAL_PATY that bai");
                            }
                            inserted.MaterialPaties = materialPaties;
                        }

                        HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMestMaterial.IMP_MEST_ID = impMest.ID;
                        impMestMaterial.MATERIAL_ID = material.ID;
                        impMestMaterial.AMOUNT = material.AMOUNT;
                        impMestMaterial.PRICE = material.IMP_PRICE;
                        impMestMaterial.VAT_RATIO = material.IMP_VAT_RATIO;
                        hisImpMestMaterials.Add(impMestMaterial);
                        output.Add(inserted);
                    }

                    if (!this.hisImpMestMaterialCreate.CreateList(hisImpMestMaterials))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                    }
                    //impMaterialSDOs = output;
                }
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

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMaterialCreate.RollbackData();
                this.hisMaterialPatyCreate.RollbackData();
                this.hisMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
