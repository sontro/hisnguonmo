using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToBiin
{
    class PrepareDataProcessor
    {
        internal static List<HIS_SERVICE_REQ> Run()
        {
            List<HIS_SERVICE_REQ> result = null;
            try
            {
                string sql = "SELECT * FROM HIS_SERVICE_REQ R WHERE SERVICE_REQ_TYPE_ID = :PARAM1 AND SERVICE_REQ_STT_ID = :PARAM2 AND BIIN_TEST_RESULT IS NULL " +
                    "AND (EXISTS (SELECT 1 FROM HIS_PATIENT WHERE R.TDL_PATIENT_ID = ID AND REGISTER_CODE IS NOT NULL) OR EXISTS (SELECT 1 FROM HIS_CARD WHERE R.TDL_PATIENT_ID = PATIENT_ID)) " +
                    "ORDER BY MODIFY_TIME DESC FETCH FIRST 100 ROWS ONLY";
                List<HIS_SERVICE_REQ> listServiceReq = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sql, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT);
                if (listServiceReq != null && listServiceReq.Count > 0)
                {
                    List<HIS_SERE_SERV> listSereServ = new List<HIS_SERE_SERV>();
                    List<V_HIS_SERE_SERV_TEIN> listSereServTein = new List<V_HIS_SERE_SERV_TEIN>();
                    List<HIS_CARD> listCard = new List<HIS_CARD>();
                    List<HIS_PATIENT> listPatient = new List<HIS_PATIENT>();

                    string sqlSereServ = "SELECT * FROM HIS_SERE_SERV WHERE IS_DELETE = 0 AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND (IS_EXPEND IS NULL OR IS_EXPEND <> 1) AND %IN_CLAUSE% ";
                    sqlSereServ = DAOWorker.SqlDAO.AddInClause(listServiceReq.Select(s => s.ID).ToList(), sqlSereServ, "SERVICE_REQ_ID");
                    listSereServ = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sqlSereServ);
                    if (listSereServ == null || listSereServ.Count == 0)
                    {
                        return result;
                    }

                    int skip = 0;
                    string sqlSsTein = "SELECT * FROM V_HIS_SERE_SERV_TEIN WHERE %IN_CLAUSE%";
                    while (listSereServ.Count - skip > 0)
                    {
                        var listIds = listSereServ.Skip(skip).Take(100).ToList();
                        skip += 100;

                        var sereServTeins = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV_TEIN>(DAOWorker.SqlDAO.AddInClause(listIds.Select(s => s.ID).ToList(), sqlSsTein, "SERE_SERV_ID"));
                        if (sereServTeins != null && sereServTeins.Count > 0)
                        {
                            listSereServTein.AddRange(sereServTeins);
                        }
                    }

                    if (listSereServTein.Count == 0)
                    {
                        return result;
                    }

                    string sqlPatient = "SELECT * FROM HIS_PATIENT WHERE %IN_CLAUSE%";
                    string sqlCard = "SELECT * FROM HIS_CARD WHERE %IN_CLAUSE%";

                    var cards = DAOWorker.SqlDAO.GetSql<HIS_CARD>(DAOWorker.SqlDAO.AddInClause(listServiceReq.Select(s => s.TDL_PATIENT_ID).Distinct().ToList(), sqlCard, "PATIENT_ID"));
                    if (cards != null && cards.Count > 0)
                    {
                        listCard.AddRange(cards);
                    }
                    var patients = DAOWorker.SqlDAO.GetSql<HIS_PATIENT>(DAOWorker.SqlDAO.AddInClause(listServiceReq.Select(s => s.TDL_PATIENT_ID).Distinct().ToList(), sqlPatient, "ID"));
                    if (patients != null && patients.Count > 0)
                    {
                        listPatient.AddRange(patients);
                    }

                    result = new List<HIS_SERVICE_REQ>();
                    foreach (var data in listServiceReq)
                    {
                        var patient = listPatient.FirstOrDefault(o => o.ID == data.TDL_PATIENT_ID);
                        var sereServs = listSereServ.Where(o => o.SERVICE_REQ_ID == data.ID).ToList();
                        var ssTeins = listSereServTein.Where(o => sereServs.Select(s => s.ID).Contains(o.SERE_SERV_ID)).ToList();

                        string biinResult = "";
                        new HisServiceReqTestEhrResulting().Run(patient, data, sereServs, ssTeins, ref biinResult);

                        if (!String.IsNullOrWhiteSpace(biinResult))
                        {
                            result.Add(new HIS_SERVICE_REQ() { ID = data.ID, BIIN_TEST_RESULT = biinResult });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
