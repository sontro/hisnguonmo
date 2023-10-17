using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Unconfirm
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;
        private HisMaterialUpdate hisMaterialUpdate;

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
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, HIS_IMP_MEST_MATERIAL impMestMaterial, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.CheckExistsImpExpMest(impMest, impMestMaterial);
                this.ProcessHisImpMest(impMest);
                this.ProcessHisMaterial(impMestMaterial);
                this.ProcessHisMaterialBean(impMestMaterial, ref sqls);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HIS_IMP_MEST impMest)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.APPROVAL_LOGINNAME = null;
            impMest.APPROVAL_USERNAME = null;
            impMest.APPROVAL_TIME = null;
            impMest.IMP_LOGINNAME = null;
            impMest.IMP_USERNAME = null;
            impMest.IMP_TIME = null;
            if (!this.hisImpMestUpdate.Update(impMest, before))
            {
                throw new Exception("hisImpMestUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessHisMaterial(HIS_IMP_MEST_MATERIAL impMestMaterial)
        {
            HIS_MATERIAL material = new HisMaterialGet().GetById(impMestMaterial.MATERIAL_ID);
            Mapper.CreateMap<HIS_MATERIAL, HIS_MATERIAL>();
            HIS_MATERIAL before = Mapper.Map<HIS_MATERIAL>(material);
            material.IMP_TIME = null;
            material.IS_PREGNANT = Constant.IS_TRUE;
            if (!this.hisMaterialUpdate.Update(material, before))
            {
                throw new Exception("hisMaterialUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessHisMaterialBean(HIS_IMP_MEST_MATERIAL impMestMaterial, ref List<string> sqls)
        {
            string sqlDelete = String.Format("DELETE HIS_MATERIAL_BEAN WHERE MATERIAL_ID = {0}", impMestMaterial.MATERIAL_ID);
            sqls.Add(sqlDelete);
        }

        private void CheckExistsImpExpMest(HIS_IMP_MEST impMest, HIS_IMP_MEST_MATERIAL impMestMaterial)
        {
            List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().GetByMaterialId(impMestMaterial.MATERIAL_ID);
            if (IsNotNullOrEmpty(expMestMaterials))
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                throw new Exception("Thuoc thanh pham da co phieu xuat");
            }

            List<HIS_IMP_MEST_MATERIAL> impMestMaterials = new HisImpMestMaterialGet().GetByMaterialId(impMestMaterial.MATERIAL_ID);
            impMestMaterials = impMestMaterials != null ? impMestMaterials.Where(o => o.IMP_MEST_ID != impMest.ID).ToList() : null;
            if (IsNotNullOrEmpty(impMestMaterials))
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                throw new Exception("Thuoc thanh pham da co phieu nhap khac voi phieu nhap thanh pham. Kiem tra lai du lieu");
            }
        }

        internal void RollbackData()
        {
            try
            {
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
