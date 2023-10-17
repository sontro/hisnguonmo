using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Confirm
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;

        private HisMaterialUpdate hisMaterialUpdate;
        private HisMaterialBeanCreate hisMaterialBeanCreate;

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
            this.hisMaterialBeanCreate = new HisMaterialBeanCreate(param);
            this.hisMaterialUpdate = new HisMaterialUpdate(param);
        }

        internal bool Run(HIS_DISPENSE dispense, HIS_IMP_MEST impMest, string loginname, string username, ref HIS_MATERIAL material, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                HIS_MATERIAL hisMaterial = null;
                this.ProcessHisImpMest(dispense, impMest, loginname, username);
                this.ProcessMaterial(impMest, ref hisMaterial);
                this.ProcessMaterialBean(impMest, hisMaterial, ref sqls);
                material = hisMaterial;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HIS_DISPENSE dispense, HIS_IMP_MEST impMest, string loginname, string username)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT;
            impMest.APPROVAL_LOGINNAME = loginname;
            impMest.APPROVAL_USERNAME = username;
            impMest.APPROVAL_TIME = dispense.DISPENSE_TIME;
            impMest.IMP_LOGINNAME = loginname;
            impMest.IMP_USERNAME = username;
            impMest.IMP_TIME = dispense.DISPENSE_TIME;
            if (!this.hisImpMestUpdate.Update(impMest, before))
            {
                throw new Exception("hisImpMestUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessMaterial(HIS_IMP_MEST impMest, ref HIS_MATERIAL material)
        {
            List<HIS_IMP_MEST_MATERIAL> impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(impMest.ID);
            if (impMestMaterials == null || impMestMaterials.Count != 1)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("So luong HIS_IMP_MEST_MATERIAL theo ImpMestId khac 1");
            }
            HIS_IMP_MEST_MATERIAL impMestMaterial = impMestMaterials[0];
            HIS_MATERIAL hisMaterial = new HisMaterialGet().GetById(impMestMaterial.MATERIAL_ID);
            Mapper.CreateMap<HIS_MATERIAL, HIS_MATERIAL>();
            HIS_MATERIAL before = Mapper.Map<HIS_MATERIAL>(hisMaterial);
            hisMaterial.IS_PREGNANT = null;
            hisMaterial.IMP_TIME = impMest.IMP_TIME;
            if (!this.hisMaterialUpdate.Update(hisMaterial, before))
            {
                throw new Exception("hisMaterialUpdate. Ket thuc nghiep vu");
            }
            material = hisMaterial;
        }

        private void ProcessMaterialBean(HIS_IMP_MEST impMest, HIS_MATERIAL material, ref List<string> sqls)
        {
            HIS_MATERIAL_BEAN bean = new HIS_MATERIAL_BEAN();
            bean.MATERIAL_ID = material.ID;
            bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
            bean.AMOUNT = material.AMOUNT;
            bean.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
            HisMaterialBeanUtil.SetTdl(bean, material );
            if (!this.hisMaterialBeanCreate.Create(bean))
            {
                throw new Exception("hisMaterialBeanCreate. Ket thuc nghiep vu");
            }

            if (sqls == null) sqls = new List<string>();
            sqls.Add(String.Format("UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1 WHERE ID = {0}", bean.ID));
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMaterialBeanCreate.RollbackData();
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
