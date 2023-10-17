using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisMaterialBean.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unapprove
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMatyReqDecreaseDdAmount decreaseDdAmount;
        private HisMaterialBeanUnlockByExpMest beanUnlock;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.beanUnlock = new HisMaterialBeanUnlockByExpMest(param);
            this.decreaseDdAmount = new HisExpMestMatyReqDecreaseDdAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref List<string> sqls)
        {
            try
            {
                this.ProcessMatyReq(expMest, expMaterials);
                this.ProcessMaterialBean(expMaterials, expMest.MEDI_STOCK_ID, ref sqls);
                this.ProcessExpMestMaterial(expMest, expMaterials, ref sqls);
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessMatyReq(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            if (IsNotNullOrEmpty(expMaterials))
            {
                Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();
                var Groups = expMaterials.GroupBy(g => g.EXP_MEST_MATY_REQ_ID ?? 0).ToList();
                foreach (var group in Groups)
                {
                    dicDecrease[group.Key] = group.Sum(s => s.AMOUNT);
                }

                if (!this.decreaseDdAmount.Run(dicDecrease))
                {
                    throw new Exception("decreaseDdAmount. Ket thuc nghiep vu");
                }
            }
        }

        /// <summary>
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMaterials"></param>
        private void ProcessMaterialBean(List<HIS_EXP_MEST_MATERIAL> expMaterials, long mediStockId, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(expMaterials))
            {
                string sqlUpdate = this.beanUnlock.GenSql(expMaterials.Select(s => s.ID).ToList());
                sqls.Add(sqlUpdate);
            }
        }

        /// <summary>
        /// Huy thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMaterial(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(expMaterials))
            {
                string sqlDelete = String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE EXP_MEST_ID = {0}", expMest.ID);

                sqls.Add(sqlDelete);
            }
        }

        internal void Rollback()
        {
            this.decreaseDdAmount.Rollback();
        }
    }
}
