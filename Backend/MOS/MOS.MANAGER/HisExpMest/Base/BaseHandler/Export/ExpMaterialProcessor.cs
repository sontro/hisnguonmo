using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialBean.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class ExpMaterialProcessor : BusinessBase
    {
        private HisMaterialBeanExport hisMaterialBeanExport;

        internal ExpMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMaterialBeanExport = new HisMaterialBeanExport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, string loginname, string username, long expTime, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    this.ProcessMaterialBean(expMestMaterials, expMest.MEDI_STOCK_ID, ref sqls);
                    this.ProcessExpMestMaterial(expMestMaterials, loginname, username, expTime, ref sqls);
                }
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
