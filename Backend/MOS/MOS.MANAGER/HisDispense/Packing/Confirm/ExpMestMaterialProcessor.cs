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

namespace MOS.MANAGER.HisDispense.Packing.Confirm
{
    class ExpMestMaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanExport hisMaterialBeanExport;

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
            this.hisMaterialBeanExport = new HisMaterialBeanExport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                this.ProcessMaterialBean(hisExpMestMaterials, expMest.MEDI_STOCK_ID, ref sqls);
                this.ProcessExpMestMaterial(expMest, hisExpMestMaterials);
                expMestMaterials = hisExpMestMaterials;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Cap nhat his_material_bean
        /// </summary>
        /// <param name="hisExpMestMaterials"></param>
        private void ProcessMaterialBean(List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, long mediStockId, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisExpMestMaterials))
            {
                List<long> expMestMaterialIds = hisExpMestMaterials.Select(o => o.ID).ToList();
                sqls.Add(this.hisMaterialBeanExport.GenSql(expMestMaterialIds, mediStockId));
            }
        }

        private void ProcessExpMestMaterial(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials)
        {
            if (IsNotNullOrEmpty(hisExpMestMaterials))
            {
                hisExpMestMaterials.ForEach(o =>
                {
                    o.IS_EXPORT = MOS.UTILITY.Constant.IS_TRUE;
                    o.EXP_LOGINNAME = expMest.LAST_EXP_LOGINNAME;
                    o.EXP_USERNAME = expMest.LAST_EXP_USERNAME;
                    o.EXP_TIME = expMest.LAST_EXP_TIME;
                    o.APPROVAL_LOGINNAME = expMest.LAST_EXP_LOGINNAME;
                    o.APPROVAL_USERNAME = expMest.LAST_EXP_USERNAME;
                    o.APPROVAL_TIME = expMest.LAST_EXP_TIME;
                });

                if (!this.hisExpMestMaterialUpdate.UpdateList(hisExpMestMaterials))
                {
                    throw new Exception("hisExpMestMaterialUpdate. Ket thuc nghiep vu");
                }
            }
        }

        internal void RollbackData()
        {
            this.hisExpMestMaterialUpdate.RollbackData();
        }
    }
}
