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
    class HisMaterialBeanUnimport : BusinessBase
    {
        List<long> materialBeanIds = null;
        long hisMediStockId;
        internal HisMaterialBeanUnimport()
            : base()
        {

        }

        internal HisMaterialBeanUnimport(CommonParam param)
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
                    string sql = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMaterialBean_CapNhatThatBai);
                        throw new Exception("Update MaterialBean that bai sql: " + sql);
                    }
                    this.materialBeanIds = beanIds;
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
                if (IsNotNullOrEmpty(this.materialBeanIds) && this.hisMediStockId > 0)
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(this.materialBeanIds, new StringBuilder().Append("UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, MEDI_STOCK_ID = ").Append(this.hisMediStockId).Append(" WHERE %IN_CLAUSE% ").ToString(), "ID");
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Rollback MaterialBean that bai sql: " + sql);
                    }
                    this.materialBeanIds = null;//Tranh truong hop rollback nhieu lan
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
