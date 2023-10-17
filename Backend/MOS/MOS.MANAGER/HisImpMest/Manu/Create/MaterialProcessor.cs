using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMediContractMaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Manu.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisMediContractMatyUpdate hisMediContractMatyUpdate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
            this.hisMediContractMatyUpdate = new HisMediContractMatyUpdate(param);
        }

        internal bool Run(List<HisMaterialWithPatySDO> ManuMaterials, HIS_IMP_MEST impMest, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref  List<HisMaterialWithPatySDO> impMaterialSDOs, HisImpMestManuSDO data)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(ManuMaterials))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && checker.IsValidImpMaterials(ManuMaterials);
                    valid = valid && checker.IsNotExistStopImpMaterialType(ManuMaterials);
                    if (!valid)
                    {
                        return false;
                    }

                    List<V_HIS_MEDI_CONTRACT_MATY_1> mediContractMatys = this.GetMediContractMaty(ManuMaterials);

                    List<HIS_MEDI_CONTRACT_MATY> updates = new List<HIS_MEDI_CONTRACT_MATY>();
                    List<HIS_MEDI_CONTRACT_MATY> befores = new List<HIS_MEDI_CONTRACT_MATY>();

                    List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                    List<HisMaterialWithPatySDO> output = new List<HisMaterialWithPatySDO>();

                    Mapper.CreateMap<V_HIS_MEDI_CONTRACT_MATY_1, HIS_MEDI_CONTRACT_MATY>();

                    foreach (HisMaterialWithPatySDO impMaterial in ManuMaterials)
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

                        if (material.MEDICAL_CONTRACT_ID.HasValue)
                        {
                            V_HIS_MEDI_CONTRACT_MATY_1 contractMaty = mediContractMatys.FirstOrDefault(o => o.MEDICAL_CONTRACT_ID == material.MEDICAL_CONTRACT_ID.Value && o.MATERIAL_TYPE_ID == material.MATERIAL_TYPE_ID && (o.CONTRACT_PRICE ?? 0) == material.CONTRACT_PRICE && (o.BID_GROUP_CODE ?? "") == (material.TDL_BID_GROUP_CODE ?? ""));
                            if (contractMaty == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception(String.Format("Khong ton tai HIS_MEDI_CONTRACT_MATY tuong ung voi MEDICAL_CONTRACT_ID: {0} va MATERIAL_TYPE_ID: {0} cua lo vat tu.", material.MEDICAL_CONTRACT_ID.Value, material.MATERIAL_TYPE_ID));
                            }

                            if (material.IMP_VAT_RATIO > 0
                                && (!contractMaty.IMP_VAT_RATIO.HasValue || contractMaty.IMP_VAT_RATIO.Value == 0))
                            {
                                HIS_MEDI_CONTRACT_MATY before = Mapper.Map<HIS_MEDI_CONTRACT_MATY>(contractMaty);
                                HIS_MEDI_CONTRACT_MATY up = Mapper.Map<HIS_MEDI_CONTRACT_MATY>(contractMaty);
                                up.IMP_VAT_RATIO = material.IMP_VAT_RATIO;
                                if (up.CONTRACT_PRICE.HasValue)
                                    up.IMP_PRICE = up.CONTRACT_PRICE.Value / (decimal)(1 + up.IMP_VAT_RATIO.Value);
                                if (!updates.Exists(o => o.ID == up.ID))
                                    updates.Add(up);
                                befores.Add(before);
                            }
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
                        impMestMaterial.DOCUMENT_PRICE = material.DOCUMENT_PRICE;
                        impMestMaterial.IMP_UNIT_AMOUNT = material.IMP_UNIT_AMOUNT;
                        impMestMaterial.IMP_UNIT_PRICE = material.IMP_UNIT_PRICE;
                        impMestMaterial.TDL_IMP_UNIT_CONVERT_RATIO = material.TDL_IMP_UNIT_CONVERT_RATIO;
                        impMestMaterial.TDL_IMP_UNIT_ID = material.TDL_IMP_UNIT_ID;
                        impMestMaterial.CONTRACT_PRICE = material.CONTRACT_PRICE;
                        hisImpMestMaterials.Add(impMestMaterial);
                        output.Add(inserted);
                    }

                    if (!this.hisImpMestMaterialCreate.CreateList(hisImpMestMaterials))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisMediContractMatyUpdate.UpdateList(updates, befores))
                    {
                        throw new Exception("Update HIS_MEDI_CONTRACT_MATY that bai");
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

        private List<V_HIS_MEDI_CONTRACT_MATY_1> GetMediContractMaty(List<HisMaterialWithPatySDO> manuMaterials)
        {
            if (!manuMaterials.Any(a => a.Material.MEDICAL_CONTRACT_ID.HasValue))
            {
                return null;
            }

            List<long> medicalContractIds = manuMaterials.Where(o => o.Material.MEDICAL_CONTRACT_ID.HasValue).Select(s => s.Material.MEDICAL_CONTRACT_ID.Value).Distinct().ToList();

            return new HisMediContractMatyGet().GetView1ByMedicalContractIds(medicalContractIds);
        }

        internal void Rollback()
        {
            try
            {
                this.hisMediContractMatyUpdate.RollbackData();
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
