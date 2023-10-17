using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test
{
    partial class HisServiceReqTestGet : GetBase
    {
        internal List<TestResultI3DrugsTDO> GetResultForI3Drugs(Filter.TestResultI3DrugsFilter data)
        {
            List<TestResultI3DrugsTDO> result = null;
            try
            {
                if (String.IsNullOrWhiteSpace(data.patientId) || data.patientId.Length != 10)
                {
                    throw new Exception("Thông tin mã bệnh nhân không hợp lệ");
                }

                string serviceReqSql = "SELECT * FROM HIS_SERVICE_REQ WHERE TDL_PATIENT_CODE = :param1 AND SERVICE_REQ_TYPE_ID = :param2 AND INTRUCTION_DATE = :param3";
                long intructionDate = Inventec.Common.DateTime.Get.Now() ?? 0;
                if (data.precriptionDate.HasValue)
                {
                    if (!long.TryParse(data.precriptionDate.Value.ToString("yyyyMMdd") + "000000", out intructionDate))
                    {
                        intructionDate = Inventec.Common.DateTime.Get.Now() ?? 0;
                    }
                }

                List<HIS_SERVICE_REQ> serviceReqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(serviceReqSql, data.patientId, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN, intructionDate);
                if (IsNotNullOrEmpty(serviceReqs))
                {
                    List<long> serviceReqIds = serviceReqs.Select(s => s.ID).ToList();
                    string sqlSst = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "SELECT * FROM V_HIS_SERE_SERV_TEIN WHERE %IN_CLAUSE%", "TDL_SERVICE_REQ_ID");

                    List<V_HIS_SERE_SERV_TEIN> sereServTeins = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV_TEIN>(sqlSst);
                    if (IsNotNullOrEmpty(sereServTeins))
                    {
                        result = new List<TestResultI3DrugsTDO>();
                        foreach (var ssTein in sereServTeins)
                        {
                            TestResultI3DrugsTDO tdo = new TestResultI3DrugsTDO();
                            tdo.measurementName = ssTein.TEST_INDEX_NAME;
                            if (!String.IsNullOrWhiteSpace(ssTein.VALUE))
                            {
                                decimal teinValue = 0;
                                if (decimal.TryParse(ssTein.VALUE, System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), out teinValue))
                                {
                                    tdo.value = teinValue;
                                }
                            }

                            tdo.unit = ssTein.TEST_INDEX_UNIT_NAME;

                            HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == ssTein.TDL_SERVICE_REQ_ID);
                            if (serviceReq != null && serviceReq.FINISH_TIME.HasValue)
                            {
                                tdo.date = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.FINISH_TIME.Value);
                            }

                            result.Add(tdo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                param.Messages.Add(ex.Message);
                result = null;
            }

            return result;
        }
    }
}
