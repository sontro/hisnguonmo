using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.MANAGER.HisSereServ
{
    partial class HisSereServGet : GetBase
    {
        internal List<DHisSereServ2> GetDHisSereServ2(DHisSereServ2Filter filter)
        {
            try
            {
                StringBuilder query = new StringBuilder("SELECT * FROM D_HIS_SERE_SERV_2 WHERE 1 = 1 ");
                List<object> listParam = new List<object>();
                if (filter.TREATMENT_ID.HasValue)
                {
                    query.Append(" AND TDL_TREATMENT_ID = :param1");
                    listParam.Add(filter.TREATMENT_ID.Value);
                }
                if (filter.INTRUCTION_DATE.HasValue)
                {
                    query.Append(" AND TDL_INTRUCTION_DATE = :param2");
                    listParam.Add(filter.INTRUCTION_DATE.Value);
                }
                if (filter.IS_NO_EXECUTE.HasValue && filter.IS_NO_EXECUTE.Value)
                {
                    query.Append(" AND IS_NO_EXECUTE IS NOT NULL AND IS_NO_EXECUTE = :param3");
                    listParam.Add(Constant.IS_TRUE);
                }
                if (filter.IS_NO_EXECUTE.HasValue && !filter.IS_NO_EXECUTE.Value)
                {
                    query.Append(" AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> :param4)");
                    listParam.Add(Constant.IS_TRUE);
                }
                object[] prs = listParam.ToArray();
                return DAOWorker.SqlDAO.GetSql<DHisSereServ2>(query.ToString(), prs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV> GetExceedMinDuration(HisSereServMinDurationFilter filter)
        {
            List<HIS_SERE_SERV> result = null;
            try
            {
                if (filter != null && filter.PatientId > 0 && IsNotNullOrEmpty(filter.ServiceDurations) && filter.InstructionTime > 0)
                {
                    List<object> listParam = new List<object>();


                    int index = 0;
                    StringBuilder durationCondition = new StringBuilder("(1 = 0)");
                    foreach (ServiceDuration s in filter.ServiceDurations)
                    {
                        DateTime intructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(filter.InstructionTime).Value;
                        DateTime dateSub = intructionTime.AddDays(-1 * s.MinDuration);
                        DateTime datePlus = intructionTime.AddDays(s.MinDuration);
                        string tmp = string.Format(" OR ((TDL_INTRUCTION_DATE BETWEEN :param{0} AND :param{1}) AND SERVICE_ID = :param{2})", index, index + 1, index + 2);
                        
                        durationCondition.Append(tmp);

                        listParam.Add(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateSub).Value);
                        listParam.Add(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(datePlus).Value);
                        listParam.Add(s.ServiceId);

                        index = index + 3;
                    }
                    string sql = string.Format("SELECT * FROM HIS_SERE_SERV WHERE IS_DELETE = 0 AND ({0}) AND SERVICE_REQ_ID IS NOT NULL AND IS_NO_EXECUTE IS NULL AND TDL_PATIENT_ID = :param{1} AND SERVICE_REQ_ID <> :param{2}", durationCondition.ToString(), index, index + 1);

                    listParam.Add(filter.PatientId);
                    listParam.Add(filter.ServiceReqId);

                    result = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sql, listParam.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Chi lay cac sere_serv la dich vu kham, voi doi tuong la "kham" va la BHYT trong ngay
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HIS_SERE_SERV> GetSereServBhytOutpatientExam(HisSereServBhytOutpatientExamFilter filter)
        {
            try
            {
                if (filter != null && IsNotNullOrEmpty(filter.ROOM_IDs))
                {

                    string query = "SELECT * FROM HIS_SERE_SERV "
                        + " WHERE TDL_INTRUCTION_DATE = :param1 "
                        + " AND PATIENT_TYPE_ID = :param2 "
                        + " AND TDL_SERVICE_TYPE_ID = :param3 "
                        + " AND SERVICE_REQ_ID IS NOT NULL "
                        + " AND IS_NO_EXECUTE IS NULL "
                        + " AND JSON_PATIENT_TYPE_ALTER LIKE '%\"TREATMENT_TYPE_ID\":{0}%' AND %IN_CLAUSE% ";
                    query = string.Format(query,
                        IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM);
                    query = DAOWorker.SqlDAO.AddInClause(filter.ROOM_IDs, query, "TDL_EXECUTE_ROOM_ID");
                    return DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(query, filter.INTRUCTION_DATE, HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV> GetRecentPttt(HisSereServRecentPtttFilter filter)
        {
            try
            {
                if (filter != null)
                {
                    List<object> listParam = new List<object>();

                    StringBuilder query = new StringBuilder("SELECT SS.* FROM HIS_SERE_SERV SS JOIN HIS_SERVICE_REQ SR ON SS.SERVICE_REQ_ID = SR.ID WHERE SS.TDL_PATIENT_ID = :param1 AND SS.IS_DELETE = 0 AND SS.SERVICE_REQ_ID IS NOT NULL AND SS.IS_NO_EXECUTE IS NULL AND SS.IS_EXPEND IS NULL AND SR.SERVICE_REQ_STT_ID = :param2 AND SR.SERVICE_REQ_TYPE_ID IN (:param3, :param4) ");

                    listParam.Add(filter.PATIENT_ID);
                    listParam.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT);
                    listParam.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT);
                    listParam.Add(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT);

                    if (filter.INTRUCTION_TIME.HasValue)
                    {
                        query.Append(" AND SR.INTRUCTION_TIME <= :param5 ");
                        listParam.Add(filter.INTRUCTION_TIME.Value);
                    }
                    if (filter.SERVICE_ID.HasValue)
                    {
                        query.Append(" AND SS.SERVICE_ID = :param6 ");
                        listParam.Add(filter.SERVICE_ID.Value);
                    }

                    // add order by
                    query.Append(string.Format(" ORDER BY SR.INTRUCTION_TIME DESC FETCH FIRST {0} ROWS ONLY", filter.COUNT));

                    return DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(query.ToString(), listParam.ToArray());
                }
                return null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HisSereServCountSDO GetCount(HisSereServCountFilter filter)
        {
            HisSereServCountSDO result = null;
            try
            {
                string sql = new StringBuilder()
                .Append("SELECT ")
                .Append("(SELECT NVL(COUNT(*), 0) FROM HIS_SERE_SERV SS JOIN HIS_SERVICE_REQ SR ON SS.SERVICE_REQ_ID = SR.ID ")
                .Append("WHERE SR.EXECUTE_ROOM_ID = :param1 AND SR.INTRUCTION_DATE = :param2 AND SR.SERVICE_REQ_STT_ID IN (2,3) ")
                .Append("AND (SS.IS_DELETE IS NULL OR SS.IS_DELETE <> 1) AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1)) AS TotalProcess, ")
                .Append("(SELECT NVL(COUNT(*), 0) FROM HIS_SERE_SERV SS ")
                .Append("WHERE SS.TDL_EXECUTE_ROOM_ID = :param1 AND SS.TDL_INTRUCTION_DATE = :param2 ")
                .Append("AND (SS.IS_DELETE IS NULL OR SS.IS_DELETE <> 1) AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1)) AS TotalAssign ")
                .Append("FROM DUAL").ToString();

                result = DAOWorker.SqlDAO.GetSqlSingle<HisSereServCountSDO>(sql, filter.EXECUTE_ROOM_ID, filter.INTRUCTION_DATE);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }

    }
}
