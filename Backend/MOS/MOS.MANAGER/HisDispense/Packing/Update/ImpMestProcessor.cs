using AutoMapper;
using Inventec.Common.ObjectChecker;
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

namespace MOS.MANAGER.HisDispense.Packing.Update
{
    class ImpMestProcessor : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_MATERIAL recentHisMaterial;

        private HisImpMestUpdate hisImpMestUpdate;

        private HisMaterialUpdate hisMaterialUpdate;
        private HisMaterialPatyCreate hisMaterialPatyCreate;
        private HisMaterialPatyUpdate hisMaterialPatyUpdate;
        private HisImpMestMaterialUpdate hisImpMestMaterialUpdate;

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
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.hisImpMestMaterialUpdate = new HisImpMestMaterialUpdate(param);
            this.hisMaterialPatyCreate = new HisMaterialPatyCreate(param);
            this.hisMaterialPatyUpdate = new HisMaterialPatyUpdate(param);
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
        }

        internal bool Run(HisPackingUpdateSDO data, HIS_DISPENSE dispense, HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref HIS_IMP_MEST_MATERIAL impMestMaterial, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                HIS_IMP_MEST_MATERIAL hisImpMestMaterial = null;
                this.ProcessHisImpMest(data, dispense, impMest);
                this.ProcessHisMaterial(data, expMestMaterials, ref hisImpMestMaterial, ref sqls);
                this.ProcessHisImpMestMaterial(hisImpMestMaterial);
                impMestMaterial = hisImpMestMaterial;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisPackingUpdateSDO data, HIS_DISPENSE dispense, HIS_IMP_MEST impMest)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
            impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT;
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.DISPENSE_ID = dispense.ID;
            impMest.TDL_DISPENSE_CODE = dispense.DISPENSE_CODE;

            if (ValueChecker.IsPrimitiveDiff<HIS_IMP_MEST>(before, impMest))
            {
                if (!this.hisImpMestUpdate.Update(impMest, before))
                {
                    throw new Exception("hisImpMestUpdate. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
            this.recentHisImpMest = impMest;
        }

        private void ProcessHisMaterial(HisPackingUpdateSDO data, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref HIS_IMP_MEST_MATERIAL impMestMaterial, ref List<string> sqls)
        {
            List<HIS_IMP_MEST_MATERIAL> olds = new HisImpMestMaterialGet().GetByImpMestId(this.recentHisImpMest.ID);
            if (olds == null || olds.Count != 1)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("So luong HIS_IMP_MEST_MATERIAL theo ImpMestId khac 1");
            }

            impMestMaterial = olds[0];
            HIS_MATERIAL material = new HisMaterialGet().GetById(impMestMaterial.MATERIAL_ID);
            HIS_MATERIAL_TYPE materialType = new HisMaterialTypeGet().GetById(material.MATERIAL_TYPE_ID);
            Mapper.CreateMap<HIS_MATERIAL, HIS_MATERIAL>();
            HIS_MATERIAL before = Mapper.Map<HIS_MATERIAL>(material);
            material.AMOUNT = data.Amount;
            material.EXPIRED_DATE = data.ExpiredDate;
            material.TDL_BID_NUMBER = data.HeinDocumentNumber;
            material.PACKAGE_NUMBER = data.PackageNumber;
            material.IS_SALE_EQUAL_IMP_PRICE = null;
            material.TDL_SERVICE_ID = materialType.SERVICE_ID;
            material.INTERNAL_PRICE = materialType.INTERNAL_PRICE;
            material.IS_PREGNANT = Constant.IS_TRUE;
            this.ProcessImpPrice(material, expMestMaterials);
            HisMaterialUtil.SetTdl(material, materialType);
            HisMaterialUtil.SetTdl(material, this.recentHisImpMest);

            if (HisMaterialUtil.CheckIsDiff(material, before))
            {
                if (!this.hisMaterialUpdate.Update(material, before))
                {
                    throw new Exception("hisMaterialCreate. Ket thuc nghiep vu");
                }
            }
            this.recentHisMaterial = material;

            #region Xy ly thong tin chinh sach gia

            if (IsNotNullOrEmpty(data.MaterialPaties))
            {
                data.MaterialPaties.ForEach(o => o.MATERIAL_ID = material.ID);
            }

            List<HIS_MATERIAL_PATY> materialPatys = data.MaterialPaties;
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
                if (sqls == null)
                {
                    sqls = new List<string>();
                }
                string query = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.MATERIAL_ID).ToList(), "DELETE HIS_MATERIAL_PATY WHERE %IN_CLAUSE%", "ID");
                sqls.Add(query);
            }
            #endregion

        }

        private void ProcessHisImpMestMaterial(HIS_IMP_MEST_MATERIAL impMestMaterial)
        {
            Mapper.CreateMap<HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();
            HIS_IMP_MEST_MATERIAL before = Mapper.Map<HIS_IMP_MEST_MATERIAL>(impMestMaterial);
            impMestMaterial.AMOUNT = this.recentHisMaterial.AMOUNT;
            impMestMaterial.IMP_MEST_ID = this.recentHisImpMest.ID;
            impMestMaterial.MATERIAL_ID = this.recentHisMaterial.ID;
            impMestMaterial.PRICE = this.recentHisMaterial.IMP_PRICE;
            impMestMaterial.VAT_RATIO = this.recentHisMaterial.IMP_VAT_RATIO;

            if (!this.hisImpMestMaterialUpdate.Update(impMestMaterial))
            {
                throw new Exception("hisImpMestMaterialUpdate. Ket thuc nghiep vu");
            }
        }

        private void ProcessImpPrice(HIS_MATERIAL material, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            decimal totalPrice = 0;
            if (IsNotNullOrEmpty(expMestMaterials))
            {
                totalPrice += expMestMaterials.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
            }

            material.IMP_PRICE = totalPrice / material.AMOUNT;
            material.IMP_VAT_RATIO = 0;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisImpMestMaterialUpdate.RollbackData();
                this.hisMaterialPatyUpdate.RollbackData();
                this.hisMaterialPatyCreate.RollbackData();
                this.hisMaterialUpdate.RollbackData();
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
