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
    class HisExpMestMaterialDecreaseBcsReqAmount : BusinessBase
    {
        Dictionary<long, decimal> dicDecrease;
        internal HisExpMestMaterialDecreaseBcsReqAmount()
            : base()
        {

        }

        internal HisExpMestMaterialDecreaseBcsReqAmount(CommonParam param)
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
                        sb.Append(String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET BCS_REQ_AMOUNT = NVL(BCS_REQ_AMOUNT,0) - {0} WHERE ID = {1};", CommonUtil.ToString(dic.Value), dic.Key));
                    }
                    string sql = String.Format("BEGIN {0} END;", sb.ToString());
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("giam so luong BCS_REQ_AMOUNT that bai. Sql:" + sql.ToString());
                        return false;
                    }
                    result = true;
                    this.dicDecrease = dicData;
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
            if (this.dicDecrease != null && this.dicDecrease.Count > 0)
            {
                StringBuilder sbRollback = new StringBuilder();
                foreach (var dic in dicDecrease)
                {
                    sbRollback.Append(String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET BCS_REQ_AMOUNT = NVL(BCS_REQ_AMOUNT,0) + {0} WHERE ID = {1};", CommonUtil.ToString(dic.Value), dic.Key));
                }
                string sql = String.Format("BEGIN {0} END;", sbRollback.ToString());
                if (!DAOWorker.SqlDAO.Execute(sql))
                {
                    LogSystem.Warn("Rollback giam so luong BCS_REQ_AMOUNT that bai. Sql:" + sql.ToString());
                }
                this.dicDecrease = null;
            }
        }
    }
}
