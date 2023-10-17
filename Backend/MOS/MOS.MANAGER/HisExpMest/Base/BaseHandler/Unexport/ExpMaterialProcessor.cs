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

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class ExpMaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanUnexport hisMaterialBeanUnexport;


        internal ExpMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisMaterialBeanUnexport = new HisMaterialBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            bool result = false;
            try
            {
                this.ProcessExpMestMaterial(expMaterials);
                this.ProcessMaterialBean(expMaterials, expMest.MEDI_STOCK_ID);

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
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMaterials"></param>
        private void ProcessMaterialBean(List<HIS_EXP_MEST_MATERIAL> expMaterials, long mediStockId)
        {
            if (IsNotNullOrEmpty(expMaterials) && !this.hisMaterialBeanUnexport.Run(expMaterials, mediStockId))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        /// <summary>
        /// Huy thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            if (IsNotNullOrEmpty(expMaterials))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> befores = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(expMaterials);
                expMaterials.ForEach(o =>
                {
                    o.IS_EXPORT = null;
                    o.EXP_LOGINNAME = null;
                    o.EXP_USERNAME = null;
                    o.EXP_TIME = null;
                });

                if (!this.hisExpMestMaterialUpdate.UpdateList(expMaterials, befores))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialUpdate.RollbackData();
            this.hisMaterialBeanUnexport.Rollback();
        }
    }
}
