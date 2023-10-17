using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisServiceReq
{
    enum ServiceReqUpdateExecuteOption
    {
        EXECUTE, //cap nhat ve is_no_execute = null
        NO_EXECUTE, //cap nhat ve is_no_execute = 1
        CHECK_SERE_SERV //cap nhat phu thuoc is_no_execute trong sere_serv
    }

    class HisServiceReqUpdateNoExecuteUtil : BusinessBase
    {
        internal static List<string> GenSql(List<long> ids, ServiceReqUpdateExecuteOption option, bool hasExam, long treatmentId)
        {
            List<string> sqls = new List<string>();
            try
            {
                List<HIS_SERVICE_REQ> raws = new List<HIS_SERVICE_REQ>();

                //Cap nhat is_no_execute cua service_req co kiem tra is_no_execute cua sere_serv trong service_req
                if (option == ServiceReqUpdateExecuteOption.CHECK_SERE_SERV)
                {
                    string sql = "UPDATE HIS_SERVICE_REQ REQ "
                    + " SET REQ.IS_NO_EXECUTE = (CASE "
                    + "  WHEN EXISTS (SELECT 1 FROM HIS_SERE_SERV SS WHERE SS.IS_DELETE = 0 AND SS.SERVICE_REQ_ID = REQ.ID AND SS.IS_NO_EXECUTE IS NULL) THEN NULL "
                    + "  ELSE {0} END) "
                    + ", REQ.TDL_SERVICE_IDS = (SELECT LISTAGG(SERVICE_ID,';') WITHIN GROUP (ORDER BY SS.SERVICE_REQ_ID,SS.SERVICE_ID) FROM HIS_SERE_SERV SS WHERE SS.SERVICE_REQ_ID = REQ.ID AND SS.IS_DELETE = 0 AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1)) "
                    + " WHERE %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(ids, sql, "REQ.ID");
                    sql = string.Format(sql, Constant.IS_TRUE);

                    sqls.Add(sql);

                    //Neu co dich vu kham thi cap nhat de bo "kham chinh" doi voi dich vu kham 
                    //dang duoc danh dau la "kham chinh" nhung bi tich la "khong thuc hien"
                    if (hasExam)
                    {
                        string tmp = "UPDATE HIS_SERVICE_REQ SET IS_MAIN_EXAM = NULL "
                        + " WHERE IS_MAIN_EXAM = {0} AND IS_NO_EXECUTE = {1} AND %IN_CLAUSE% ";
                        string sqlUpdateMainExam = DAOWorker.SqlDAO.AddInClause(ids, tmp, "ID");
                        sqlUpdateMainExam = string.Format(sqlUpdateMainExam, Constant.IS_TRUE, Constant.IS_TRUE);
                        sqls.Add(sqlUpdateMainExam);
                    }
                }
                else if (option == ServiceReqUpdateExecuteOption.EXECUTE)
                {
                    string sql = "UPDATE HIS_SERVICE_REQ REQ SET REQ.IS_NO_EXECUTE = NULL "
                    + ", REQ.TDL_SERVICE_IDS = (SELECT LISTAGG(SERVICE_ID,';') WITHIN GROUP (ORDER BY SS.SERVICE_REQ_ID,SS.SERVICE_ID) FROM HIS_SERE_SERV SS WHERE SS.SERVICE_REQ_ID = REQ.ID AND SS.IS_DELETE = 0 AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1)) "
                    + " WHERE %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(ids, sql, "REQ.ID");
                    sqls.Add(sql);
                }
                else if (option == ServiceReqUpdateExecuteOption.NO_EXECUTE)
                {
                    //Neu ko thuc hien thi bo kham chinh ("is_main_exam")
                    string sql = "UPDATE HIS_SERVICE_REQ REQ SET IS_MAIN_EXAM = NULL, IS_NO_EXECUTE = {0} WHERE %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(ids, sql, "ID");
                    sql = string.Format(sql, Constant.IS_TRUE);
                    sqls.Add(sql);
                }

                //Neu co dich vu kham thi cap nhat "kham chinh" doi voi dich vu kham con lai trong truong hop
                //co dich vu "kham chinh" bi tich "khong thuc hien"
                if (hasExam)
                {
                    string tmp = "UPDATE HIS_SERVICE_REQ REQ SET REQ.IS_MAIN_EXAM = {0} "
                                + " WHERE REQ.ID = (SELECT ID FROM HIS_SERVICE_REQ REQ1 "
                                + " WHERE REQ1.SERVICE_REQ_TYPE_ID = {1} AND REQ1.TREATMENT_ID = {2} "
                                + " AND REQ1.IS_NO_EXECUTE IS NULL"
                                + " ORDER BY REQ1.INTRUCTION_TIME ASC FETCH FIRST ROWS ONLY) "
                                + " AND NOT EXISTS (SELECT 1 FROM HIS_SERVICE_REQ REQ2 WHERE REQ2.IS_MAIN_EXAM = {3} "
                                + " AND REQ2.IS_NO_EXECUTE IS NULL AND REQ2.TREATMENT_ID = {4} ) ";

                    string sqlUpdateMainExam1 = string.Format(tmp, Constant.IS_TRUE, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH, treatmentId, Constant.IS_TRUE, treatmentId);
                    sqls.Add(sqlUpdateMainExam1);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                sqls = null;
            }
            return sqls;
        }
    }
}
