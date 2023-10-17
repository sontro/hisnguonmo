using Inventec.Common.Logging;
using MOS.DynamicDTO;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction
{
    partial class HisTransactionGet : GetBase
    {
        internal HisTransactionTotalPriceSDO GetTotalPriceSdo(HisTransactionViewFilterQuery filter)
        {
            HisTransactionTotalPriceSDO result = null;
            try
            {
                filter.ORDER_DIRECTION = null;
                filter.ORDER_FIELD = null;
                filter.ColumnParams = new List<string>()
                {
                    "ID",
                    "AMOUNT",
                    "IS_CANCEL",
                    "TRANSACTION_TYPE_ID"
                };

                List<HisTransactionViewDTO> dtos = this.GetViewDynamic(filter);

                if (dtos != null)
                {
                    result = new HisTransactionTotalPriceSDO();
                    result.TotalPrice = dtos.Sum(s => s.AMOUNT);
                    result.CancelPrice = dtos.Where(o => o.IS_CANCEL == Constant.IS_TRUE).Sum(s => s.AMOUNT);
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

        internal HisTransactionGeneralInfoSDO GetGeneralInfo(HisTransactionGeneralInfoFilter filter)
        {
            HisTransactionGeneralInfoSDO result = null;
            try
            {
                if (filter != null && !String.IsNullOrWhiteSpace(filter.CASHIER_LOGINNAME) && filter.TRANSACTION_DATE.HasValue)
                {
                    string sql = new StringBuilder().Append("SELECT ")
                        .Append("NVL(SUM(CASE WHEN IS_DIRECTLY_BILLING = 1 THEN AMOUNT ELSE 0 END), 0) AS TOTAL_DIRECTLY, ")
                        .Append("NVL(SUM(CASE WHEN IS_DIRECTLY_BILLING IS NULL OR IS_DIRECTLY_BILLING <> 1 THEN AMOUNT ELSE 0 END), 0) AS TOTAL_NOT_DIRECTLY ")
                        .Append("FROM HIS_TRANSACTION ")
                        .Append("WHERE (IS_CANCEL IS NULL OR IS_CANCEL <> 1) ")
                        .Append("AND (IS_DELETE IS NULL OR IS_DELETE <> 1) ")
                        .Append("AND TRANSACTION_TYPE_ID = :param1 ")
                        .Append("AND CASHIER_LOGINNAME = :param2 ")
                        .Append("AND TRANSACTION_DATE = :param3").ToString();

                    LogSystem.Debug("Sql Get general transaction: " + sql);

                    GeneralInfo info = DAOWorker.SqlDAO.GetSqlSingle<GeneralInfo>(sql, IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT, filter.CASHIER_LOGINNAME, filter.TRANSACTION_DATE.Value);

                    result = new HisTransactionGeneralInfoSDO();
                    result.CashierLoginname = filter.CASHIER_LOGINNAME;
                    result.TransactionDate = filter.TRANSACTION_DATE;
                    if (info != null)
                    {
                        result.TotalBillDirectly = info.TOTAL_DIRECTLY;
                        result.TotalBillNotDirectly = info.TOTAL_NOT_DIRECTLY;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }
    }

    class GeneralInfo
    {
        public decimal TOTAL_DIRECTLY { get; set; }
        public decimal TOTAL_NOT_DIRECTLY { get; set; }
    }
}
