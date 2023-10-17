using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Other.Create
{
    class MaterialReusableProcessor : BusinessBase
    {
        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;

        internal MaterialReusableProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
        }

        internal bool Run(List<HisMaterialWithPatySDO> materialReusSDOs, HIS_IMP_MEST impMest, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref  List<HisMaterialWithPatySDO> impMaterialSDOs)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(materialReusSDOs))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && checker.IsValidImpMaterials(materialReusSDOs);
                    valid = valid && checker.IsNotExistStopImpMaterialType(materialReusSDOs);
                    if (!valid)
                    {
                        return false;
                    }

                    List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                    List<HisMaterialWithPatySDO> output = new List<HisMaterialWithPatySDO>();

                    foreach (HisMaterialWithPatySDO impMaterial in materialReusSDOs)
                    {
                        HisMaterialWithPatySDO inserted = new HisMaterialWithPatySDO();

                        //insert thong tin material
                        HIS_MATERIAL material = impMaterial.Material;
                        HIS_MATERIAL_TYPE materialType = hisMaterialTypes
                            .Where(o => o.ID == material.MATERIAL_TYPE_ID).SingleOrDefault();
                        //tu dong insert cac truong sau dua vao thong tin co trong danh muc
                        material.INTERNAL_PRICE = materialType.INTERNAL_PRICE;
                        material.SUPPLIER_ID = impMest.SUPPLIER_ID;
                        material.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                        material.MAX_REUSE_COUNT = materialType.MAX_REUSE_COUNT;
                        HisMaterialUtil.SetTdl(material, materialType);

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

                        foreach (var seri in impMaterial.SerialNumbers)
                        {
                            HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                            impMestMaterial.IMP_MEST_ID = impMest.ID;
                            impMestMaterial.MATERIAL_ID = material.ID;
                            impMestMaterial.AMOUNT = 1;
                            impMestMaterial.PRICE = material.IMP_PRICE;
                            impMestMaterial.VAT_RATIO = material.IMP_VAT_RATIO;
                            impMestMaterial.SERIAL_NUMBER = seri.SerialNumber;
                            impMestMaterial.REMAIN_REUSE_COUNT = seri.ReusCount;
                            hisImpMestMaterials.Add(impMestMaterial);
                        }
                        output.Add(inserted);
                    }

                    if (!this.hisImpMestMaterialCreate.CreateList(hisImpMestMaterials))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                    }
                    if (impMaterialSDOs != null)
                        impMaterialSDOs.AddRange(output);
                    else
                        impMaterialSDOs = output;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
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
