﻿using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialBean.Update;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.AggrExam.Unexport
{
    partial class MaterialProcessor : BusinessBase
    {
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
            this.hisMaterialBeanUnexport = new HisMaterialBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials)
        {
            try
            {
                this.ProcessExpMestMaterial(hisExpMestMaterials);
                this.ProcessMaterialBean(hisExpMestMaterials, aggrExpMest.MEDI_STOCK_ID);
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
        /// Huy thong tin xuat vao exp_mest_material
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (IsNotNullOrEmpty(expMestMaterials))
            {
                List<long> expMestMaterialIds = expMestMaterials.Select(o => o.ID).ToList();

                string sql = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_EXPORT = NULL, EXP_LOGINNAME = NULL, EXP_USERNAME = NULL, EXP_TIME = NULL WHERE %IN_CLAUSE%", "ID");

                if (!DAOWorker.SqlDAO.Execute(sql))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisMaterialBeanUnexport.Rollback();
        }
    }
}
