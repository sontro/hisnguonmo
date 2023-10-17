using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialBean.Update;
using MOS.MANAGER.HisMediStockMaty;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    partial class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanUnexport hisMaterialBeanUnexport;

        internal MaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal MaterialProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisMaterialBeanUnexport = new HisMaterialBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials)
        {
            try
            {
                this.ProcessExpMestMaterial(hisExpMestMaterials);
                this.ProcessMaterialBean(hisExpMestMaterials, expMest.MEDI_STOCK_ID);

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMaterials"></param>
        private void ProcessMaterialBean(List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestMaterials) && !this.hisMaterialBeanUnexport.Run(hisExpMestMaterials, mediStockId))
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
                });

                if (!this.hisExpMestMaterialUpdate.UpdateList(expMestMaterials, befores))
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
