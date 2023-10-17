using Inventec.Core;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMachine
{
    class HisMachineGetSql : BusinessBase
    {
        internal HisMachineGetSql()
            : base()
        {

        }

        internal HisMachineGetSql(CommonParam param)
            : base(param)
        {

        }

        internal List<HisMachineCounterSDO> GetCounter(HisMachineCounterFilter filter)
        {
            List<HisMachineCounterSDO> result = null;
            try
            {

                long intructionData = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + "000000");

                if (filter.INTRUCTION_DATE.HasValue)
                {
                    intructionData = filter.INTRUCTION_DATE.Value;
                }
                List<long> ids = null;

                if (filter.ROOM_ID.HasValue)
                {
                    ids = HisMachineCFG.DATA.Where(o => !String.IsNullOrEmpty(o.ROOM_IDS) && String.Format(",{0},", o.ROOM_IDS).Contains(String.Format(",{0},", filter.ROOM_ID.Value))).Select(s => s.ID).ToList();
                }
                string isBHYTquery = "";
                if (filter.IS_BHYT.HasValue)
                {
                    if (filter.IS_BHYT.Value == true)
                    {
                        isBHYTquery = String.Format("AND SS.PATIENT_TYPE_ID = {0} ", HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                    }
                    else
                    {
                        isBHYTquery = String.Format("AND SS.PATIENT_TYPE_ID <> {0} ", HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                    }
                }


                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT M.*, ")
                    .Append("(SELECT COUNT(*) FROM HIS_SERE_SERV_EXT SSEX ")//TOTAL_PROCESSED_SERVICE
                    .Append("JOIN HIS_SERE_SERV SS ON SSEX.SERE_SERV_ID = SS.ID ")
                    .Append("WHERE SSEX.MACHINE_ID IS NOT NULL ")
                    .Append("AND (SS.IS_DELETE IS NULL OR SS.IS_DELETE <> 1) ")
                    .Append("AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1) ")
                    .Append("AND SS.SERVICE_REQ_ID IS NOT NULL ")
                    .Append("AND SSEX.MACHINE_ID = M.ID ")
                    .Append("AND SS.TDL_INTRUCTION_DATE = :param1 ")
                    .Append(isBHYTquery)
                    .Append(") AS TOTAL_PROCESSED_SERVICE, ")//TOTAL_PROCESSED_SERVICE
                    .Append("(SELECT COUNT(DISTINCT(SSTE.SERE_SERV_ID)) FROM HIS_SERE_SERV_TEIN SSTE ")//TOTAL_PROCESSED_SERVICE_TEIN
                    .Append("JOIN HIS_SERE_SERV SS ON SSTE.SERE_SERV_ID = SS.ID ")
                    .Append("WHERE SSTE.MACHINE_ID IS NOT NULL ")
                    .Append("AND (SS.IS_DELETE IS NULL OR SS.IS_DELETE <> 1) ")
                    .Append("AND (SS.IS_NO_EXECUTE IS NULL OR SS.IS_NO_EXECUTE <> 1) ")
                    .Append("AND SS.SERVICE_REQ_ID IS NOT NULL ")
                    .Append("AND SSTE.MACHINE_ID = M.ID ")
                    .Append("AND SS.TDL_INTRUCTION_DATE = :param1 ")
                    .Append(isBHYTquery)
                    .Append(") AS TOTAL_PROCESSED_SERVICE_TEIN ")//TOTAL_PROCESSED_SERVICE_TEIN
                    .Append("FROM HIS_MACHINE M ");
               

                if (ids != null)
                {
                    ids.Add(-1);
                    string machineIds = String.Join("','", ids);
                    sb.Append(String.Format("WHERE M.ID IN ('{0}') ", machineIds));
                }
                result = DAOWorker.SqlDAO.GetSql<HisMachineCounterSDO>(sb.ToString(), intructionData);
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
}
