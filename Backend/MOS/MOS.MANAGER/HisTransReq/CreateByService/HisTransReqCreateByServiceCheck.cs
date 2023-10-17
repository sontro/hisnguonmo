using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.License;
using MOS.SDO;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.CreateByService
{
    class HisTransReqCreateByServiceCheck : BusinessBase
    {
        internal HisTransReqCreateByServiceCheck()
            : base()
        {

        }

        internal HisTransReqCreateByServiceCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckConfig(HIS_TREATMENT data, ref List<HIS_SERE_SERV> executeSereServ)
        {
            bool valid = true;
            try
            {
                valid = valid && IsNotNull(data);
                valid = valid && HisTransReqCFG.AUTO_CREATE_OPTION;
                valid = valid && data.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;
                if (valid)
                {
                    List<long> NOT_IN_TYPE_IDs = new List<long>()
                    {
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN
                    };

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM HIS_SERE_SERV SESE WHERE SESE.TDL_TREATMENT_ID = :param1");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL B WHERE SESE.ID = B.SERE_SERV_ID AND NVL(B.IS_CANCEL,0) = 0) ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_DEPOSIT C WHERE SESE.ID = C.SERE_SERV_ID AND NVL(C.IS_CANCEL,0) = 0 ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SESE_DEPO_REPAY D WHERE D.SERE_SERV_DEPOSIT_ID = C.ID AND NVL(D.IS_CANCEL,0) = 0)) ");
                    sb.Append(" AND NOT EXISTS (SELECT 1 FROM HIS_SERE_SERV_DEBT E WHERE SESE.ID = E.SERE_SERV_ID AND NVL(E.IS_CANCEL,0) = 0) ");
                    sb.Append(" AND SESE.VIR_TOTAL_PATIENT_PRICE > 0 ");
                    sb.Append(" AND SESE.SERVICE_REQ_ID IS NOT NULL ");
                    sb.Append(" AND NVL(SESE.IS_DELETE,0) = 0 ");
                    sb.AppendFormat(" AND SESE.PATIENT_TYPE_ID != {0} ", HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                    sb.AppendFormat(" AND SESE.TDL_SERVICE_TYPE_ID NOT IN ({0}) ", string.Join(",", NOT_IN_TYPE_IDs));

                    executeSereServ = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sb.ToString(), data.ID);
                    valid = valid && IsNotNullOrEmpty(executeSereServ);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}
