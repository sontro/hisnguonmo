using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestBlood
{
    partial class HisExpMestBloodDelete : BusinessBase
    {
        private List<long> recentExpMestBloodIds = new List<long>();

        internal HisExpMestBloodDelete()
            : base()
        {

        }

        internal HisExpMestBloodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        /// <summary>
        /// Xoa cac thong tin duyet chua duoc thuc xuat
        /// </summary>
        /// <param name="expMestBloods"></param>
        /// <returns></returns>
        internal bool Delete(List<long> expMestBloodIds)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(expMestBloodIds))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(expMestBloodIds, "UPDATE HIS_EXP_MEST_BLOOD SET IS_DELETE = :param1 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sql, MOS.UTILITY.Constant.IS_TRUE))
                    {
                        throw new Exception("Xoa exp_mest_blood that bai. sql: " + sql);
                    }
                    this.recentExpMestBloodIds.AddRange(expMestBloodIds);
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
                if (IsNotNullOrEmpty(this.recentExpMestBloodIds))
                {
                    string sqlRollback = DAOWorker.SqlDAO.AddInClause(this.recentExpMestBloodIds, "UPDATE HIS_EXP_MEST_BLOOD SET IS_DELETE = 0 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sqlRollback))
                    {
                        LogSystem.Error("Rollback viec xoa exp_mest_blood that bai. " + sqlRollback);
                    }
                    this.recentExpMestBloodIds = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

        }
    }
}
