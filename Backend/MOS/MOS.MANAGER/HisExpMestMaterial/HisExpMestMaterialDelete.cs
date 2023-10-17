using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMaterial
{
    partial class HisExpMestMaterialDelete : BusinessBase
    {
        private List<long> recentExpMestMaterialIds = new List<long>();

        internal HisExpMestMaterialDelete()
            : base()
        {

        }

        internal HisExpMestMaterialDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        /// <summary>
        /// Xoa cac thong tin duyet
        /// </summary>
        /// <param name="expMestMaterials"></param>
        /// <returns></returns>
        internal bool Delete(List<long> expMestMaterialIds)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(expMestMaterialIds))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = :param1 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sql, MOS.UTILITY.Constant.IS_TRUE))
                    {
                        throw new Exception("Xoa exp_mest_material that bai. sql: " + sql);
                    }
                    this.recentExpMestMaterialIds.AddRange(expMestMaterialIds);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentExpMestMaterialIds))
                {
                    string sqlRollback = DAOWorker.SqlDAO.AddInClause(this.recentExpMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 0 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sqlRollback))
                    {
                        LogSystem.Error("Rollback viec xoa exp_mest_material that bai. " + sqlRollback);
                    }
                    this.recentExpMestMaterialIds = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

        }
    }
}
