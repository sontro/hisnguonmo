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
    class HisMedicineBeanUnimport : BusinessBase
    {
        List<long> medicineBeanIds = null;
        long hisMediStockId;
        internal HisMedicineBeanUnimport()
            : base()
        {

        }

        internal HisMedicineBeanUnimport(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> beanIds, long mediStockId)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(beanIds))
                {
                    if (mediStockId <= 0)
                        throw new Exception("mediStockId invalid: " + mediStockId);
                    string sql = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMedicineBean_CapNhatThatBai);
                        throw new Exception("Update MedicineBean that bai sql: " + sql);
                    }
                    this.medicineBeanIds = beanIds;
                    this.hisMediStockId = mediStockId;
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.medicineBeanIds) && this.hisMediStockId > 0)
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(this.medicineBeanIds, new StringBuilder().Append("UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, MEDI_STOCK_ID = ").Append(this.hisMediStockId).Append(" WHERE %IN_CLAUSE% ").ToString(), "ID");
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Rollback MedicineBean that bai sql: " + sql);
                    }
                    this.medicineBeanIds = null;//Tranh truong hop rollback nhieu lan
                    this.hisMediStockId = 0;//Tranh truong hop rollback nhieu lan
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
