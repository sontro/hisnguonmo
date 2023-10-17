using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    class HisMedicineBeanExport : BusinessBase
    {
        internal HisMedicineBeanExport()
            : base()
        {
        }

        internal HisMedicineBeanExport(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal string GenSql(List<long> expMestMedicineIds, long mediStockId)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestMedicineIds))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL, EXP_MEST_MEDICINE_ID = NULL WHERE MEDI_STOCK_ID = {0} AND %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
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
