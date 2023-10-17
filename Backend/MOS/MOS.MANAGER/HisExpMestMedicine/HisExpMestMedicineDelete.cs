using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMedicine
{
    partial class HisExpMestMedicineDelete : BusinessBase
    {
        private List<long> recentExpMestMedicineIds = new List<long>();

        internal HisExpMestMedicineDelete()
            : base()
        {

        }

        internal HisExpMestMedicineDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        /// <summary>
        /// Xoa cac thong tin duyet chua duoc thuc xuat
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <returns></returns>
        internal bool Delete(List<long> expMestMedicineIds)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(expMestMedicineIds))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = :param1 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sql, MOS.UTILITY.Constant.IS_TRUE))
                    {
                        throw new Exception("Xoa exp_mest_medicine that bai. sql: " + sql);
                    }
                    this.recentExpMestMedicineIds.AddRange(expMestMedicineIds);
                    result = true;
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
                if (IsNotNullOrEmpty(this.recentExpMestMedicineIds))
                {
                    string sqlRollback = DAOWorker.SqlDAO.AddInClause(this.recentExpMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 0 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sqlRollback))
                    {
                        LogSystem.Error("Rollback viec xoa exp_mest_medicine that bai. " + sqlRollback);
                    }
                    this.recentExpMestMedicineIds = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

        }
    }
}
