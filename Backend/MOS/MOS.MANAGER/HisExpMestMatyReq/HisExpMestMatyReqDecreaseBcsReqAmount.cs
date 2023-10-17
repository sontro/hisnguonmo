using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqDecreaseBcsReqAmount : BusinessBase
    {
        private Dictionary<long, decimal> recentDecreaseDic;

        internal HisExpMestMatyReqDecreaseBcsReqAmount()
            : base()
        {

        }

        internal HisExpMestMatyReqDecreaseBcsReqAmount(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        /// <summary>
        /// Tang so luong da yc bu co so (BCS_REQ_AMOUNT) theo tung ID
        /// </summary>
        /// <param name="decreaseDic"></param>
        /// <returns></returns>
        internal bool Run(Dictionary<long, decimal> decreaseDic)
        {
            bool result = true;
            try
            {
                if (decreaseDic != null && decreaseDic.Count > 0)
                {
                    List<string> sqls = new List<string>();
                    foreach (long k in decreaseDic.Keys)
                    {
                        string sql = string.Format("UPDATE HIS_EXP_MEST_MATY_REQ SET BCS_REQ_AMOUNT = NVL(BCS_REQ_AMOUNT, 0) - {0} WHERE ID = {1}", CommonUtil.ToString(decreaseDic[k]), k);
                        sqls.Add(sql);
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Giam so luong BCS_REQ_AMOUNT that bai.");
                        return false;
                    }
                    this.recentDecreaseDic = decreaseDic;
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
                if (this.recentDecreaseDic != null && this.recentDecreaseDic.Count > 0)
                {
                    StringBuilder sqlBuilder = new StringBuilder();
                    foreach (long k in this.recentDecreaseDic.Keys)
                    {
                        sqlBuilder.Append(string.Format("UPDATE HIS_EXP_MEST_MATY_REQ SET BCS_REQ_AMOUNT = NVL(BCS_REQ_AMOUNT, 0) + {0} WHERE ID = {1};", CommonUtil.ToString(this.recentDecreaseDic[k]), k));
                    }
                    string sql = string.Format("BEGIN {0} END;", sqlBuilder.ToString());
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Rollback giam so luong BCS_REQ_AMOUNT that bai. Sql:" + sql.ToString());
                    }
                    this.recentDecreaseDic = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
