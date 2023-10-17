using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
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

namespace MOS.MANAGER.HisImpMest.Other.Other.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
        }

        internal bool Run(List<HisMaterialWithPatySDO> otherMaterials, HIS_IMP_MEST impMest, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref  List<HisMaterialWithPatySDO> impMaterialSDOs)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(otherMaterials))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && checker.IsValidImpMaterials(otherMaterials);
                    valid = valid && checker.IsNotExistStopImpMaterialType(otherMaterials);
                    if (!valid)
                    {
                        return false;
                    }


                    List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                    List<HisMaterialWithPatySDO> output = new List<HisMaterialWithPatySDO>();
                    foreach (HisMaterialWithPatySDO impMaterial in otherMaterials)
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

                        if (materialType.IMP_UNIT_ID.HasValue && (!materialType.IMP_UNIT_CONVERT_RATIO.HasValue || materialType.IMP_UNIT_CONVERT_RATIO.Value <= 0))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("materialType.IMP_UNIT_CONVERT_RATIO <= 0");
                        }

                        if (materialType.IMP_UNIT_ID.HasValue)
                        {
                            material.IMP_UNIT_AMOUNT = material.AMOUNT;
                            material.IMP_UNIT_PRICE = material.IMP_PRICE;
                            material.AMOUNT = material.IMP_UNIT_AMOUNT.Value * materialType.IMP_UNIT_CONVERT_RATIO.Value;
                            material.IMP_PRICE = material.IMP_UNIT_PRICE.Value / materialType.IMP_UNIT_CONVERT_RATIO.Value;
                        }
                        else
                        {
                            material.IMP_UNIT_AMOUNT = null;
                            material.IMP_UNIT_PRICE = null;

                        }

                        HisMaterialUtil.SetTdl(material, materialType);
                        HisMaterialUtil.SetTdl(material, impMest);

                        if (!this.hisMaterialCreate.Create(material))
                        {
                            throw new Exception("Tao HIS_MATERIAL that bai");
                        }
                        inserted.Material = material;

                        //neu co thong tin chinh sach gia thi insert thong tin gia ban cho tung material
                        if (IsNotNullOrEmpty(impMaterial.MaterialPaties))
                        {
                            List<HIS_MATERIAL_PATY> materialPaties = impMaterial.MaterialPaties;

                            if (materialType.IMP_UNIT_ID.HasValue)
                            {
                                materialPaties.ForEach(o =>
                                {
                                    o.IMP_UNIT_EXP_PRICE = o.EXP_PRICE;
                                    o.EXP_PRICE = o.EXP_PRICE / materialType.IMP_UNIT_CONVERT_RATIO.Value;
                                });
                            }
                            else
                            {
                                materialPaties.ForEach(o =>
                                {
                                    o.IMP_UNIT_EXP_PRICE = null;
                                });
                            }

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
                        impMestMaterial.IMP_UNIT_AMOUNT = material.IMP_UNIT_AMOUNT;
                        impMestMaterial.IMP_UNIT_PRICE = material.IMP_UNIT_PRICE;
                        impMestMaterial.TDL_IMP_UNIT_CONVERT_RATIO = material.TDL_IMP_UNIT_CONVERT_RATIO;
                        impMestMaterial.TDL_IMP_UNIT_ID = material.TDL_IMP_UNIT_ID;
                        hisImpMestMaterials.Add(impMestMaterial);
                        output.Add(inserted);
                    }

                    if (!this.hisImpMestMaterialCreate.CreateList(hisImpMestMaterials))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                    }
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
