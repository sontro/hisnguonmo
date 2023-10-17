using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Unconfirm
{
    class ExpMestMaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanUnexport hisMaterialBeanUnexport;

        internal ExpMestMaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisMaterialBeanUnexport = new HisMaterialBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            bool result = false;
            try
            {
                this.ProcessMaterialBean(expMestMaterials, expMest.MEDI_STOCK_ID);
                this.ProcessExpMestMaterial(expMestMaterials);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessMaterialBean(List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestMaterials))
            {
                if (!this.hisMaterialBeanUnexport.Run(hisExpMestMaterials, mediStockId))
                {
                    throw new Exception("hisMaterialBeanUnexport. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (IsNotNullOrEmpty(expMestMaterials))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> befores = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(expMestMaterials);

                expMestMaterials.ForEach(o =>
                {
                    o.IS_EXPORT = null;
                    o.EXP_LOGINNAME = null;
                    o.EXP_USERNAME = null;
                    o.EXP_TIME = null;
                    o.APPROVAL_LOGINNAME = null;
                    o.APPROVAL_USERNAME = null;
                    o.APPROVAL_TIME = null;
                });

                if (!this.hisExpMestMaterialUpdate.UpdateList(expMestMaterials, befores))
                {
                    throw new Exception("hisExpMestMaterialUpdate. Ket thuc nghiep vu");
                }
            }
        }

        internal void RollbackData()
        {
            this.hisExpMestMaterialUpdate.RollbackData();
            this.hisMaterialBeanUnexport.Rollback();
        }
    }
}
