using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAccountBook
{
    internal class HisAccountBookGetSql : GetBase
    {
        internal HisAccountBookGetSql()
            : base()
        {

        }

        internal HisAccountBookGetSql(CommonParam param)
            : base(param)
        {

        }

        internal List<HisAccountBookGeneralInfoSDO> GetGeneralInfo(HisAccountBookGeneralInfoFilter filter)
        {
            List<HisAccountBookGeneralInfoSDO> result = new List<HisAccountBookGeneralInfoSDO>();
            try
            {
                if (filter != null && IsNotNullOrEmpty(filter.ACCOUNT_BOOK_IDs))
                {
                    string sqlTemp = "SELECT ACBO.ID AS AccountBookId, (SELECT SUM(TRAN.AMOUNT) FROM HIS_TRANSACTION TRAN WHERE TRAN.ACCOUNT_BOOK_ID = ACBO.ID AND (TRAN.IS_CANCEL IS NULL OR TRAN.IS_CANCEL <> 1) AND TRAN.TRANSACTION_TYPE_ID = :param1{0}{1}) AS TotalBillAmount FROM HIS_ACCOUNT_BOOK ACBO WHERE %IN_CLAUSE% GROUP BY ACBO.ID";
                    string sqlAccBookId = DAOWorker.SqlDAO.AddInClause(filter.ACCOUNT_BOOK_IDs, sqlTemp, "ACBO.ID");
                    string sqlLoginname = "";
                    string sqlTransactionDate = "";
                    if (!String.IsNullOrWhiteSpace(filter.CASHIER_LOGINNAME))
                    {
                        sqlLoginname = String.Format(" AND TRAN.CASHIER_LOGINNAME = '{0}'", filter.CASHIER_LOGINNAME);
                    }
                    if (filter.TRANSACTON_DATE.HasValue)
                    {
                        sqlTransactionDate = String.Format(" AND TRAN.TRANSACTION_DATE = {0}", filter.TRANSACTON_DATE.Value);
                    }

                    string sql = String.Format(sqlAccBookId, sqlLoginname, sqlTransactionDate);
                    LogSystem.Info("HisAccountBookGetSql.GetGeneralInfo. Sql: " + sql);
                    result = DAOWorker.SqlDAO.GetSql<HisAccountBookGeneralInfoSDO>(sql, IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT);
                    if (IsNotNullOrEmpty(result))
                    {
                        result.ForEach(f =>
                        {
                            f.CashierLoginname = filter.CASHIER_LOGINNAME;
                            f.TransactionDate = filter.TRANSACTON_DATE;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
