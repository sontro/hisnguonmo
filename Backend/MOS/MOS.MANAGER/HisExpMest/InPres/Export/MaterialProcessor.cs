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

namespace MOS.MANAGER.HisExpMest.InPres.Export
{
    class MaterialProcessor : BusinessBase
    {
        internal MaterialProcessor()
            : base()
        {
        }

        internal MaterialProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }


        internal bool Run(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            try
            {
                this.ProcessExpMestMaterial(expMest.ID, ref hisExpMestMaterials, loginName, userName, expTime, ref sqls);
                this.ProcessMaterialBean(hisExpMestMaterials, expMest.MEDI_STOCK_ID, ref sqls);
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
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
                sqls.Add(new HisMaterialBeanExport(param).GenSql(expMestMaterialIds, mediStockId));
            }
        }

        /// <summary>
        /// Bo sung thong tin xuat vao exp_mest_material
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMaterial(long expMestId, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            //Lay cac du lieu chua duoc xuat va da duoc duyet
            HisExpMestMaterialFilterQuery filter = new HisExpMestMaterialFilterQuery();
            filter.EXP_MEST_ID = expMestId;
            filter.IS_EXPORT = false;
            filter.IS_APPROVED = true;

            expMestMaterials = new HisExpMestMaterialGet().Get(filter);

            if (IsNotNullOrEmpty(expMestMaterials))
            {
                List<long> expMestMaterialIds = expMestMaterials.Select(o => o.ID).ToList();

                string sql = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_EXPORT = {0}, EXP_LOGINNAME = '{1}', EXP_USERNAME = '{2}', EXP_TIME = {3} WHERE %IN_CLAUSE%", "ID");

                sqls.Add(string.Format(sql, MOS.UTILITY.Constant.IS_TRUE, loginName, userName, expTime));
            }
        }
    }
}
