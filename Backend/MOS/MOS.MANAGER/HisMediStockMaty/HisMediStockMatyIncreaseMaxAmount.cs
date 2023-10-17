using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisMediStockMaty
{
    partial class HisMediStockMatyIncreaseMaxAmount : BusinessBase
    {
        private Dictionary<long, decimal> recentIncreaseDic;

        internal HisMediStockMatyIncreaseMaxAmount()
            : base()
        {

        }

        internal HisMediStockMatyIncreaseMaxAmount(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        /// <summary>
        /// Tang so luong co so (ALERT_MAX_IN_STOCK) theo tung ID
        /// </summary>
        /// <param name="increaseDic"></param>
        /// <returns></returns>
        internal bool Run(Dictionary<long, decimal> increaseDic)
        {
            bool result = true;
            try
            {
                if (increaseDic != null && increaseDic.Count > 0)
                {
                    List<string> sqls = new List<string>();
                    foreach (long k in increaseDic.Keys)
                    {
                        string sql = string.Format("UPDATE HIS_MEDI_STOCK_MATY SET ALERT_MAX_IN_STOCK = NVL(ALERT_MAX_IN_STOCK, 0) + {0} WHERE ID = {1}", CommonUtil.ToString(increaseDic[k]), k);
                        sqls.Add(sql);
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Tang so luong ALERT_MAX_IN_STOCK that bai.");
                        return false;
                    }
                    this.recentIncreaseDic = increaseDic;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                if (this.recentIncreaseDic != null && this.recentIncreaseDic.Count > 0)
                {
                    StringBuilder sqlBuilder = new StringBuilder();
                    foreach (long k in this.recentIncreaseDic.Keys)
                    {
                        sqlBuilder.Append(string.Format("UPDATE HIS_MEDI_STOCK_MATY SET ALERT_MAX_IN_STOCK = NVL(ALERT_MAX_IN_STOCK, 0) - {0} WHERE ID = {1};", CommonUtil.ToString(this.recentIncreaseDic[k]), k));
                    }
                    string sql = string.Format("BEGIN {0} END;", sqlBuilder.ToString());
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Tang rollback so luong ALERT_MAX_IN_STOCK that bai. Sql:" + sql.ToString());
                    }
                    this.recentIncreaseDic = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
