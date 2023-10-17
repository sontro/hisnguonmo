using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    class HisMaterialBeanExport : BusinessBase
    {
        internal HisMaterialBeanExport()
            : base()
        {
        }

        internal HisMaterialBeanExport(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal string GenSql(List<long> expMestMaterialIds, long mediStockId)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestMaterialIds))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL, EXP_MEST_MATERIAL_ID = NULL WHERE MEDI_STOCK_ID = {0} AND %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                    return string.Format(query, mediStockId);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
