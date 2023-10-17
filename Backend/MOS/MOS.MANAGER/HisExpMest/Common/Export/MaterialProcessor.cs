using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialBean.Update;
using MOS.MANAGER.HisMediStockMaty;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    partial class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanExport hisMaterialBeanExport;

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
            this.hisMaterialBeanExport = new HisMaterialBeanExport(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, long mediStockId, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            try
            {
                this.ProcessMaterialBean(hisExpMestMaterials, mediStockId, ref sqls);
                this.ProcessExpMestMaterial(hisExpMestMaterials, loginName, userName, expTime, ref sqls);
                
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
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

        private void ProcessExpMestMaterial(List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisExpMestMaterials))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(hisExpMestMaterials.Select(s => s.ID).ToList(), String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET IS_EXPORT = 1, EXP_LOGINNAME = '{0}', EXP_USERNAME = '{1}', EXP_TIME = {2} WHERE %IN_CLAUSE%", loginName ?? "", userName ?? "", expTime), "ID");

                sqls.Add(sql);
            }
        }
    }
}
