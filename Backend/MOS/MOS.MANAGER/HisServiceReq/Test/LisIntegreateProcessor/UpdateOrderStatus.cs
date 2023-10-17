using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor
{
    class UpdateOrderStatus : BusinessBase
    {
        internal UpdateOrderStatus()
            : base()
        {

        }

        internal UpdateOrderStatus(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        /// <summary>
        /// Cap nhat lai trang thai de danh dau cac chi dinh XN da duoc gui sang he thong LIS thanh cong
        /// - Cac chi dinh chua gui (is_sent_ext = null) thi cap nhat is_sent_ext = 1
        /// - Cac chi dinh da gui (is_sent_ext = 1), va lan nay gui lai do co nghiep vu cap nhat (IS_UPDATED_ext = 1), 
        /// thi cap nhat lai is_update_ext trong service_req va sere_serv ve null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateSentOrder(List<OrderData> data)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    List<long> newIds = data.Where(o => !o.ServiceReq.IS_SENT_EXT.HasValue).Select(o => o.ServiceReq.ID).ToList();
                    List<long> updateIds = data
                        .Where(o => o.ServiceReq.IS_SENT_EXT == Constant.IS_TRUE
                            && o.ServiceReq.IS_UPDATED_EXT == Constant.IS_TRUE)
                        .Select(o => o.ServiceReq.ID)
                        .ToList();

                    List<string> sqls = new List<string>();
                    List<long> sereServIds = new List<long>();
                    if (IsNotNullOrEmpty(newIds))
                    {
                        string sql = "UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1, LIS_STT_ID = 1, APP_MODIFIER = 'MOS_THREAD' WHERE IS_SENT_EXT IS NULL AND %IN_CLAUSE%";
                        sql = DAOWorker.SqlDAO.AddInClause(newIds, sql, "ID");
                        sqls.Add(sql);
                    }
                    if (IsNotNullOrEmpty(updateIds))
                    {
                        string sql = "UPDATE HIS_SERVICE_REQ SET IS_UPDATED_EXT = NULL, LIS_STT_ID = 1, APP_MODIFIER = 'MOS_THREAD' WHERE IS_UPDATED_EXT IS NOT NULL AND %IN_CLAUSE%";
                        sql = DAOWorker.SqlDAO.AddInClause(updateIds, sql, "ID");
                        sqls.Add(sql);
                    }

                    foreach (OrderData d in data)
                    {
                        if (IsNotNullOrEmpty(d.Inserts))
                        {
                            List<long> ids = d.Inserts.Select(o => o.ID).ToList();
                            sereServIds.AddRange(ids);
                        }

                        if (IsNotNullOrEmpty(d.Deletes))
                        {
                            List<long> ids = d.Deletes.Select(o => o.ID).ToList();
                            sereServIds.AddRange(ids);
                        }
                    }

                    if (IsNotNullOrEmpty(sereServIds))
                    {
                        string sqlSereServ = "UPDATE HIS_SERE_SERV SET IS_SENT_EXT = 1, APP_MODIFIER = 'MOS_THREAD' WHERE IS_SENT_EXT IS NULL AND %IN_CLAUSE%";
                        sqlSereServ = DAOWorker.SqlDAO.AddInClause(sereServIds, sqlSereServ, "ID");
                        sqls.Add(sqlSereServ);
                    }

                    result = IsNotNullOrEmpty(sqls) && DAOWorker.SqlDAO.Execute(sqls);
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
    }
}
