using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialPaty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Init.Update
{
    class MaterialReusableProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisMaterialUpdate hisMaterialUpdate;
        private HisMaterialCreate hisMaterialCreate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisMaterialPatyUpdate hisMaterialPatyUpdate;

        private List<HisMaterialWithPatySDO> outputs = new List<HisMaterialWithPatySDO>();

        internal MaterialReusableProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisMaterialCreate = new HisMaterialCreate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
            this.hisMaterialPatyUpdate = new HisMaterialPatyUpdate(param);
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMaterialWithPatySDO> reusableMaterials, List<HIS_MATERIAL> hisMaterials, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref List<HisMaterialWithPatySDO> impMaterialSDOs, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.ProcessMaterial(impMest, reusableMaterials, hisMaterials, hisMaterialTypes, ref sqls);

                this.ProcessImpMestMaterial(impMest, ref sqls);

                if (IsNotNullOrEmpty(impMaterialSDOs))
                    this.outputs.AddRange(impMaterialSDOs);

                this.ProcessTruncateMaterial(hisMaterials, ref sqls);

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

        private void ProcessMaterial(HIS_IMP_MEST impMest, List<HisMaterialWithPatySDO> reusableMaterials, List<HIS_MATERIAL> hisMaterials, List<HIS_MATERIAL_TYPE> hisMaterialTypes, ref List<string> sqls)
        {
            if (!IsNotNullOrEmpty(reusableMaterials))
            {
                return;
            }
            foreach (var imp in reusableMaterials)
            {
                HisMaterialWithPatySDO updated = new HisMaterialWithPatySDO();

                HIS_MATERIAL material = imp.Material;

                HIS_MATERIAL_TYPE materialType = hisMaterialTypes
                        .Where(o => o.ID == material.MATERIAL_TYPE_ID).SingleOrDefault();

                material.INTERNAL_PRICE = materialType.INTERNAL_PRICE;
                material.SUPPLIER_ID = impMest.SUPPLIER_ID;
                material.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                material.MAX_REUSE_COUNT = materialType.MAX_REUSE_COUNT;
                HisMaterialUtil.SetTdl(material, materialType);

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

                List<HIS_MATERIAL_PATY> materialPatys = imp.MaterialPaties;
                if (IsNotNullOrEmpty(materialPatys))
                {
                    //Cap nhat material_id cho cac material_paty
                    materialPatys.ForEach(o => o.MATERIAL_ID = material.ID);
                }

                List<HIS_MATERIAL_PATY> listToInserted = IsNotNullOrEmpty(materialPatys) ? materialPatys.Where(o => o.ID <= 0).ToList() : null;
                List<HIS_MATERIAL_PATY> listToUpdate = null;
                List<HIS_MATERIAL_PATY> listToDelete = null;

                List<HIS_MATERIAL_PATY> existedMaterialPatys = new HisMaterialPatyGet().GetByMaterialId(material.ID);
                List<long> materialPatyIds = IsNotNullOrEmpty(materialPatys) ? materialPatys.Select(o => o.ID).ToList() : null;

                if (existedMaterialPatys != null)
                {
                    List<long> existedMaterialPatyIds = existedMaterialPatys.Select(o => o.ID).ToList();
                    //D/s can update la d/s da ton tai tren he thong va co trong d/s gui tu client
                    listToUpdate = IsNotNullOrEmpty(materialPatys) ? materialPatys.Where(o => existedMaterialPatyIds.Contains(o.ID)).ToList() : null;
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
                }
                #endregion

                updated.MaterialPaties = materialPatys;
                outputs.Add(updated);
            }
        }

        private void ProcessImpMestMaterial(HIS_IMP_MEST impMest, ref List<string> sqls)
        {
            List<HIS_IMP_MEST_MATERIAL> listToCreate = new List<HIS_IMP_MEST_MATERIAL>();

            if (IsNotNullOrEmpty(this.outputs))
            {
                foreach (HisMaterialWithPatySDO sdo in this.outputs)
                {
                    foreach (var seri in sdo.SerialNumbers)
                    {
                        HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMestMaterial.IMP_MEST_ID = impMest.ID;
                        impMestMaterial.MATERIAL_ID = sdo.Material.ID;
                        impMestMaterial.AMOUNT = 1;
                        impMestMaterial.PRICE = sdo.Material.IMP_PRICE;
                        impMestMaterial.VAT_RATIO = sdo.Material.IMP_VAT_RATIO;
                        impMestMaterial.SERIAL_NUMBER = seri.SerialNumber;
                        impMestMaterial.REMAIN_REUSE_COUNT = seri.ReusCount;
                        listToCreate.Add(impMestMaterial);
                    }
                }
            }

            if (IsNotNullOrEmpty(listToCreate))
            {
                if (!this.hisImpMestMaterialCreate.CreateList(listToCreate))
                {
                    throw new Exception("Tao HIS_IMP_MEST_MATERIAL that bai");
                }
            }
        }

        private void ProcessTruncateMaterial(List<HIS_MATERIAL> hisMaterials, ref List<string> sqls)
        {
            List<HIS_MATERIAL_BEAN> needToDeleteMaterialBeans = new List<HIS_MATERIAL_BEAN>();
            List<HIS_MATERIAL_PATY> needToDeleteMaterialPatys = new List<HIS_MATERIAL_PATY>();
            List<HIS_MATERIAL> needToDeleteMaterials = hisMaterials != null ? hisMaterials.Where(o => !this.outputs.Exists(e => e.Material.ID == o.ID)).ToList() : null;

            if (needToDeleteMaterials != null && needToDeleteMaterials.Count > 0)
            {
                //Lay ra danh sach material_bean va material_paty can xoa tuong ung
                List<long> needToDeleteMaterialIds = needToDeleteMaterials.Select(o => o.ID).ToList();
                needToDeleteMaterialBeans = new HisMaterialBeanGet().GetByMaterialIds(needToDeleteMaterialIds);
                needToDeleteMaterialPatys = new HisMaterialPatyGet().GetByMaterialIds(needToDeleteMaterialIds);
            }

            if (IsNotNullOrEmpty(needToDeleteMaterialBeans))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteMaterialBeans.Select(s => s.ID).ToList(), "DELETE HIS_MATERIAL_BEAN WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

            if (IsNotNullOrEmpty(needToDeleteMaterialPatys))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteMaterialPatys.Select(s => s.ID).ToList(), "DELETE HIS_MATERIAL_PATY WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

            if (IsNotNullOrEmpty(needToDeleteMaterials))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteMaterials.Select(s => s.ID).ToList(), "DELETE HIS_MATERIAL WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMaterialCreate.RollbackData();
                this.hisMaterialPatyUpdate.RollbackData();
                this.hisMaterialPatyCreate.RollbackData();
                this.hisMaterialCreate.RollbackData();
                this.hisMaterialUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
