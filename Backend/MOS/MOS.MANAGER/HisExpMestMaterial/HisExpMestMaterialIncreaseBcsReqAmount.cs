using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestMaterial
{
    class HisExpMestMaterialIncreaseBcsReqAmount : BusinessBase
    {
        Dictionary<long, decimal> dicIncrease;
        internal HisExpMestMaterialIncreaseBcsReqAmount()
            : base()
        {

        }

        internal HisExpMestMaterialIncreaseBcsReqAmount(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(Dictionary<long, decimal> dicData)
        {
            bool result = false;
            try
            {
                if (dicData != null && dicData.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var dic in dicData)
                    {
                        sb.Append(String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET BCS_REQ_AMOUNT = NVL(BCS_REQ_AMOUNT,0) + {0} WHERE ID = {1};", CommonUtil.ToString(dic.Value), dic.Key));
                    }
                    string sql = String.Format("BEGIN {0} END;", sb.ToString());
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Tang so luong BCS_REQ_AMOUNT that bai. Sql:" + sql.ToString());
                        return false;
                    }
                    result = true;
                    this.dicIncrease = dicData;
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

        internal void RollbackData()
        {
            if (this.dicIncrease != null && this.dicIncrease.Count > 0)
            {
                StringBuilder sbRollback = new StringBuilder();
                foreach (var dic in dicIncrease)
                {
                    sbRollback.Append(String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET BCS_REQ_AMOUNT = NVL(BCS_REQ_AMOUNT,0) - {0} WHERE ID = {1};", CommonUtil.ToString(dic.Value), dic.Key));
                }
                string sql = String.Format("BEGIN {0} END;", sbRollback.ToString());
                if (!DAOWorker.SqlDAO.Execute(sql))
                {
                    LogSystem.Warn("Rollback tang so luong TH_AMOUNT that bai. Sql:" + sql.ToString());
                }
                this.dicIncrease = null; //tranh truong hop goi rollback 2 lan
            }
        }
    }
}
