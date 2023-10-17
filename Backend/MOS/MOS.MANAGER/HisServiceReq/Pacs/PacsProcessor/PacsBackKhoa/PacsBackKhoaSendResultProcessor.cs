using Hl7.Fhir.Model;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.PACS.Fhir;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsBackKhoa
{
    class PacsBackKhoaSendResultProcessor : BusinessBase
    {
        private FhirProcessor fhirProcessor;

        internal PacsBackKhoaSendResultProcessor()
            : base()
        {
        }

        internal PacsBackKhoaSendResultProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool SendResult()
        {
            bool result = false;
            try
            {
                if (PacsCFG.PACS_INTEGRATE_OPTION != (int)PacsIntegrateOption.PACS_BACH_KHOA)
                    return result;

                if (String.IsNullOrWhiteSpace(PacsCFG.FHIR_CONNECT_INFO))
                    throw new NullReferenceException("Cau hinh FHIR OPTION thieu thong tin");

                List<string> cfgs = PacsCFG.FHIR_CONNECT_INFO.Split('|').ToList();
                if (cfgs == null || cfgs.Count < 3)
                {
                    throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                }

                string uri = cfgs[0];
                string loginname = cfgs[1];
                string password = cfgs[2];

                if (String.IsNullOrWhiteSpace(uri) || String.IsNullOrWhiteSpace(loginname) || String.IsNullOrWhiteSpace(password))
                    throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");

                fhirProcessor = new FhirProcessor(uri, loginname, password, Config.HisEmployeeCFG.DATA, Config.HisServiceCFG.DATA_VIEW);

                List<HIS_SERE_SERV_EXT> sereServExts = null;
                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_TREATMENT> treatments = null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                GetData(ref sereServs, ref sereServExts, ref treatments, ref serviceReqs);

                if (IsNotNullOrEmpty(sereServExts) && IsNotNullOrEmpty(sereServs) && IsNotNullOrEmpty(treatments))
                {
                    List<string> sqls = new List<string>();
                    foreach (var ext in sereServExts)
                    {
                        HIS_SERE_SERV ss = sereServs.FirstOrDefault(o => o.ID == ext.SERE_SERV_ID);
                        if (!IsNotNull(ss))
                            continue;

                        HIS_TREATMENT treatment = treatments.FirstOrDefault(o => o.ID == ss.TDL_TREATMENT_ID);
                        if (!IsNotNull(treatment))
                            continue;

                        HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == ss.SERVICE_REQ_ID);
                        if (!IsNotNull(serviceReq))
                            continue;

                        V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == serviceReq.EXECUTE_ROOM_ID);

                        string studyID = "";
                        if (fhirProcessor.SendResult(treatment, serviceReq, ss, exeRoom, ext, ref studyID))
                        {
                            string sql = "UPDATE HIS_SERE_SERV_EXT SET JSON_PRINT_ID = 'studyID:{0}' WHERE ID = {1}";
                            sqls.Add(string.Format(sql, studyID, ext.ID));
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Warn(string.Format("Khong gui dc ket qua SERE_SERV_EXT_ID: {0} SERE_SERV_ID: {1} ", ext.ID, ss.ID));
                            Inventec.Common.Logging.LogAction.Warn(string.Format("Khong gui dc ket qua SERE_SERV_EXT_ID: {0} SERE_SERV_ID: {1} ", ext.ID, ss.ID));
                        }
                    }

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Update Status Pacs that bai sql: " + sqls.ToString());
                        LogAction.Warn("Update Status Pacs that bai sql: " + sqls.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        private void GetData(ref List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV_EXT> sereServExts, ref List<HIS_TREATMENT> treatments, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                string sql = "SELECT * FROM HIS_SERE_SERV_EXT EXT WHERE EXT.JSON_PRINT_ID IS NULL AND CREATE_TIME >= {0} AND EXISTS (SELECT 1 FROM HIS_SERE_SERV SS WHERE EXT.SERE_SERV_ID = SS.ID AND SS.IS_DELETE = 0 AND SS.IS_SENT_EXT IS NULL AND SS.TDL_TREATMENT_ID IS NOT NULL AND EXISTS (SELECT 1 FROM HIS_SERVICE_REQ WHERE SS.SERVICE_REQ_ID = ID AND IS_SENT_EXT IS NULL AND PACS_STT_ID IS NULL AND PACS_STT_ID IS NULL AND ALLOW_SEND_PACS = 1))";
                sql = string.Format(sql, Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000"));

                sereServExts = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sql);
                if (IsNotNullOrEmpty(sereServExts))
                {
                    string ssSql = "SELECT * FROM HIS_SERE_SERV WHERE %IN_CLAUSE%";
                    ssSql = DAOWorker.SqlDAO.AddInClause(sereServExts.Select(s => s.SERE_SERV_ID).ToList(), ssSql, "ID");
                    sereServs = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(ssSql);

                    if (IsNotNullOrEmpty(sereServs))
                    {
                        string treatSql = "SELECT * FROM HIS_TREATMENT WHERE %IN_CLAUSE%";
                        treatSql = DAOWorker.SqlDAO.AddInClause(sereServs.Select(s => s.TDL_TREATMENT_ID ?? 0).Distinct().ToList(), treatSql, "ID");
                        treatments = DAOWorker.SqlDAO.GetSql<HIS_TREATMENT>(treatSql);

                        string reqSql = "SELECT * FROM HIS_SERVICE_REQ WHERE %IN_CLAUSE%";
                        reqSql = DAOWorker.SqlDAO.AddInClause(sereServs.Select(s => s.SERVICE_REQ_ID ?? 0).Distinct().ToList(), reqSql, "ID");
                        serviceReqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(reqSql);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
        }
    }
}
