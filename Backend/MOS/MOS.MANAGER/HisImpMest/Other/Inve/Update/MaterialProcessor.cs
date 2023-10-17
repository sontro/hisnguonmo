using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMaterialType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Inve.Update
{
    class MaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisImpMestMaterialUpdate hisImpMestMaterialUpdate;
        private HisMaterialUpdate hisMaterialUpdate;
        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisMaterialPatyUpdate hisMaterialPatyUpdate;

        private List<HisMaterialWithPatySDO> outputs = new List<HisMaterialWithPatySDO>();

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisImpMestMaterialUpdate = new HisImpMestMaterialUpdate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
            this.hisMaterialPatyUpdate = new HisMaterialPatyUpdate(param);
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMaterialWithPatySDO> materialSDOs, List<HIS_MATERIAL> hisMaterials, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref List<HisMaterialWithPatySDO> impMaterialSDOs, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.ProcessMaterial(impMest, materialSDOs, hisMaterials, hisMaterialTypes, ref sqls);

                this.ProcessImpMestMaterial(impMest, impMestMaterials, ref sqls);

                if (IsNotNullOrEmpty(outputs))
                {
                    impMaterialSDOs = outputs;
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

        private void ProcessMaterial(HIS_IMP_MEST impMest, List<HisMaterialWithPatySDO> inveMaterials, List<HIS_MATERIAL> hisMaterials, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref List<string> sqls)
        {
            if (!IsNotNullOrEmpty(inveMaterials))
            {
                return;
            }

            foreach (var imp in inveMaterials)
            {
                HisMaterialWithPatySDO updated = new HisMaterialWithPatySDO();

                HIS_MATERIAL material = imp.Material;

                HIS_MATERIAL_TYPE materialType = hisMaterialTypes
                        .Where(o => o.ID == material.MATERIAL_TYPE_ID).SingleOrDefault();

                material.INTERNAL_PRICE = materialType.INTERNAL_PRICE;
                material.SUPPLIER_ID = impMest.SUPPLIER_ID;
                material.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                material.MAX_REUSE_COUNT = null;

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

                if (material.ID > 0)
                {
                    HIS_MATERIAL oldMaterial = hisMaterials != null ? hisMaterials.FirstOrDefault(o => o.ID == material.ID) : null;
                    if (oldMaterial == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc Material theo Id: " + material.ID);
                    }
                    if (material.MATERIAL_TYPE_ID != oldMaterial.MATERIAL_TYPE_ID)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong cho sua loai cua lo thuoc " + LogUtil.TraceData("oldMaterial", oldMaterial));
                    }
                    if (HisMaterialUtil.CheckIsDiff(material, oldMaterial))
                    {
                        if (!this.hisMaterialUpdate.Update(material))
                        {
                            throw new Exception("Update HIS_MATERIAL that bai");
                        }
                    }
                }
                else
                {
                    if (!this.hisMaterialCreate.Create(material))
                    {
                        throw new Exception("Tao HIS_MATERIAL that bai");
                    }
                }
                updated.Material = material;

                #region Xy ly thong tin chinh sach gia

                List<HIS_MATERIAL_PATY> listToInserted = new List<HIS_MATERIAL_PATY>();
                List<HIS_MATERIAL_PATY> listToUpdate = new List<HIS_MATERIAL_PATY>();
                List<HIS_MATERIAL_PATY> listToDelete = null;
                List<HIS_MATERIAL_PATY> existedMaterialPatys = new HisMaterialPatyGet().GetByMaterialId(material.ID);

                List<HIS_MATERIAL_PATY> materialPatys = imp.MaterialPaties;
                if (IsNotNullOrEmpty(materialPatys))
                {
                    if (materialType.IMP_UNIT_ID.HasValue)
                    {
                        materialPatys.ForEach(o =>
                        {
                            o.IMP_UNIT_EXP_PRICE = o.EXP_PRICE;
                            o.EXP_PRICE = o.EXP_PRICE / materialType.IMP_UNIT_CONVERT_RATIO.Value;
                        });
                    }
                    else
                    {
                        materialPatys.ForEach(o =>
                        {
                            o.IMP_UNIT_EXP_PRICE = null;
                        });
                    }

                    //Cap nhat material_id cho cac material_paty
                    foreach (var mp in materialPatys)
                    {
                        mp.MATERIAL_ID = material.ID;
                        var old = existedMaterialPatys != null ? existedMaterialPatys.FirstOrDefault(o => o.PATIENT_TYPE_ID == mp.PATIENT_TYPE_ID) : null;
                        if (old != null)
                        {
                            mp.ID = old.ID;
                            listToUpdate.Add(mp);
                        }
                        else
                        {
                            listToInserted.Add(mp);
                        }
                    }
                }

                List<long> materialPatyIds = IsNotNullOrEmpty(materialPatys) ? materialPatys.Select(o => o.ID).ToList() : null;

                if (existedMaterialPatys != null)
                {
                    List<long> existedMaterialPatyIds = existedMaterialPatys.Select(o => o.ID).ToList();
                    //D/s can delete la d/s ton tai tren he thong nhung ko co trong d/s gui tu client
                    listToDelete = existedMaterialPatys.Where(o => materialPatyIds == null || !materialPatyIds.Contains(o.ID)).ToList();
                }

                if (this.IsNotNullOrEmpty(listToInserted))
                {
                    if (!this.hisMaterialPatyCreate.CreateList(listToInserted))
                    {
                        throw new Exception("Tao HIS_MATERIAL_PATY that bai");
                    }
                }
                if (this.IsNotNullOrEmpty(listToUpdate))
                {
                    if (!this.hisMaterialPatyUpdate.UpdateList(listToUpdate))
                    {
                        throw new Exception("Update HIS_MATERIAL_PATY that bai");
                    }
                }
                if (IsNotNullOrEmpty(listToDelete))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.ID).ToList(), "DELETE HIS_MATERIAL_PATY WHERE %IN_CLAUSE% ", "ID");
                    sqls.Add(sql);
                    //if (!this.hisMaterialPatyTruncate.TruncateList(listToDelete))
                    //{
                    //    throw new Exception("Xoa HIS_MATERIAL_PATY that bai");
                    //}
                }
                #endregion

                updated.MaterialPaties = materialPatys;
                outputs.Add(updated);
            }
        }

        private void ProcessImpMestMaterial(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<string> sqls)
        {
            List<HIS_IMP_MEST_MATERIAL> listNew = new List<HIS_IMP_MEST_MATERIAL>();
            List<HIS_IMP_MEST_MATERIAL> listToDelete = new List<HIS_IMP_MEST_MATERIAL>();
            List<HIS_IMP_MEST_MATERIAL> listToCreate = new List<HIS_IMP_MEST_MATERIAL>();
            List<HIS_IMP_MEST_MATERIAL> listToUpdate = new List<HIS_IMP_MEST_MATERIAL>();

            if (IsNotNullOrEmpty(this.outputs))
            {
                foreach (HisMaterialWithPatySDO sdo in this.outputs)
                {
                    List<HIS_IMP_MEST_MATERIAL> oldMestMaterials = impMestMaterials != null ? impMestMaterials.Where(o => o.MATERIAL_ID == sdo.Material.ID).ToList() : null;
                    if (IsNotNullOrEmpty(oldMestMaterials) && oldMestMaterials.Count == 1)
                    {
                        var impMestMaterial = oldMestMaterials.FirstOrDefault();
                        if (impMestMaterial.AMOUNT != sdo.Material.AMOUNT 
                            || impMestMaterial.PRICE != sdo.Material.IMP_PRICE 
                            || impMestMaterial.VAT_RATIO != sdo.Material.IMP_VAT_RATIO 
                            || !String.IsNullOrWhiteSpace(impMestMaterial.SERIAL_NUMBER) 
                            || (impMestMaterial.REMAIN_REUSE_COUNT.HasValue && impMestMaterial.REMAIN_REUSE_COUNT > 0)
                            || impMestMaterial.IMP_UNIT_AMOUNT != sdo.Material.IMP_UNIT_AMOUNT
                            || impMestMaterial.IMP_UNIT_PRICE != sdo.Material.IMP_UNIT_PRICE
                            || impMestMaterial.TDL_IMP_UNIT_CONVERT_RATIO != sdo.Material.TDL_IMP_UNIT_CONVERT_RATIO
                            || impMestMaterial.TDL_IMP_UNIT_ID != sdo.Material.TDL_IMP_UNIT_ID)
                        {
                            impMestMaterial.AMOUNT = sdo.Material.AMOUNT;
                            impMestMaterial.PRICE = sdo.Material.IMP_PRICE;
                            impMestMaterial.VAT_RATIO = sdo.Material.IMP_VAT_RATIO;
                            impMestMaterial.SERIAL_NUMBER = null;
                            impMestMaterial.REMAIN_REUSE_COUNT = null;
                            impMestMaterial.IMP_UNIT_AMOUNT = sdo.Material.IMP_UNIT_AMOUNT;
                            impMestMaterial.IMP_UNIT_PRICE = sdo.Material.IMP_UNIT_PRICE;
                            impMestMaterial.TDL_IMP_UNIT_CONVERT_RATIO = sdo.Material.TDL_IMP_UNIT_CONVERT_RATIO;
                            impMestMaterial.TDL_IMP_UNIT_ID = sdo.Material.TDL_IMP_UNIT_ID;
                            listToUpdate.Add(impMestMaterial);
                        }
                        listNew.Add(impMestMaterial);
                    }
                    else
                    {
                        HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMestMaterial.IMP_MEST_ID = impMest.ID;
                        impMestMaterial.MATERIAL_ID = sdo.Material.ID;
                        impMestMaterial.AMOUNT = sdo.Material.AMOUNT;
                        impMestMaterial.PRICE = sdo.Material.IMP_PRICE;
                        impMestMaterial.VAT_RATIO = sdo.Material.IMP_VAT_RATIO;
                        impMestMaterial.IMP_UNIT_AMOUNT = sdo.Material.IMP_UNIT_AMOUNT;
                        impMestMaterial.IMP_UNIT_PRICE = sdo.Material.IMP_UNIT_PRICE;
                        impMestMaterial.TDL_IMP_UNIT_CONVERT_RATIO = sdo.Material.TDL_IMP_UNIT_CONVERT_RATIO;
                        impMestMaterial.TDL_IMP_UNIT_ID = sdo.Material.TDL_IMP_UNIT_ID;
                        listToCreate.Add(impMestMaterial);
                        listNew.Add(impMestMaterial);
                    }
                }
            }

            List<long> updateIds = listNew.Select(o => o.ID).Distinct().ToList();

            //lay ra danh sach can xoa la danh sach co trong he thong ma ko co trong danh sach y/c tu client
            listToDelete = impMestMaterials != null ? impMestMaterials.Where(o => updateIds == null || !updateIds.Contains(o.ID)).ToList() : null;
            if (IsNotNullOrEmpty(listToCreate))
            {
                if (!this.hisImpMestMaterialCreate.CreateList(listToCreate))
                {
                    throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                }
            }

            if (IsNotNullOrEmpty(listToUpdate))
            {
                if (!this.hisImpMestMaterialUpdate.UpdateList(listToUpdate))
                {
                    throw new Exception("Update HIS_IMP_MEST_MATERIAL that bai");
                }
            }

            if (IsNotNullOrEmpty(listToDelete))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.ID).ToList(), "DELETE HIS_IMP_MEST_MATERIAL WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMaterialUpdate.RollbackData();
                this.hisImpMestMaterialCreate.RollbackData();
                this.hisMaterialPatyUpdate.RollbackData();
                this.hisMaterialPatyCreate.RollbackData();
                this.hisMaterialUpdate.RollbackData();
                this.hisMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
