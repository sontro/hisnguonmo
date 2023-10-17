using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.HisServiceReq
{
    class HisServiceReqGet : GetBase
    {
        internal HisServiceReqGet()
            : base()
        {

        }

        internal HisServiceReqGet(CommonParam param)
            : base(param)
        {

        }

        internal List<ServiceReqGeneralByDepaDDO> GetGeneralByDepartment(ServiceReqGeneralByDepaFilter filter)
        {
            List<ServiceReqGeneralByDepaDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                    .Append("SELECT SERV.ID, SERV.SERVICE_REQ_CODE, SERV.SERVICE_REQ_TYPE_ID, SERV.SERVICE_REQ_STT_ID, SERV.TREATMENT_ID, SERV.INTRUCTION_DATE, SERV.EXECUTE_ROOM_ID, SERV.EXECUTE_DEPARTMENT_ID, SERV.START_TIME, SERV.FINISH_TIME, SERV.IS_WAIT_CHILD, ROOM.ROOM_CODE AS EXECUTE_ROOM_CODE, ROOM.ROOM_NAME AS EXECUTE_ROOM_NAME")
                    //.Append(", (SELECT COUNT(CHIL.*) FROM HIS_SERVICE_REQ CHIL WHERE CHIL.IS_DELETE = 0 AND CHIL.PARENT_ID = SERV.ID AND ").Append(IN_ANCHOR).Append(" ) AS COUNT_SUBCLINICAL")
                    .Append(" FROM HIS_SERVICE_REQ SERV ")
                    .Append(" JOIN V_HIS_ROOM ROOM ON SERV.EXECUTE_ROOM_ID = ROOM.ID")
                    .Append(" WHERE SERV.IS_DELETE = 0 AND (SERV.IS_NO_EXECUTE IS NULL OR SERV.IS_NO_EXECUTE <> 1) AND SERV.SERVICE_REQ_TYPE_ID = 1 AND SERV.EXECUTE_DEPARTMENT_ID =: param1 AND SERV.INTRUCTION_DATE =: param2");
                string sql = sb.ToString();

                List<ServiceReqExtExeRoom> datas = DAOWorker.SqlDAO.GetSql<ServiceReqExtExeRoom>(sql, filter.DepartmentId, filter.IntructionDate);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<ServiceReqGeneralByDepaDDO>();
                    Dictionary<long, ServiceReqGeneralByDepaDDO> details = new Dictionary<long, ServiceReqGeneralByDepaDDO>();
                    foreach (var item in datas)
                    {
                        ServiceReqGeneralByDepaDDO dt = new ServiceReqGeneralByDepaDDO();
                        if (details.ContainsKey(item.EXECUTE_ROOM_ID))
                        {
                            dt = details[item.EXECUTE_ROOM_ID];
                        }
                        else
                        {
                            dt.ExecuteRoomCode = item.EXECUTE_ROOM_CODE;
                            dt.ExecuteRoomName = item.EXECUTE_ROOM_NAME;
                            details[item.EXECUTE_ROOM_ID] = dt;
                        }
                        if (item.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                        {
                            dt.NoProcessAmount += 1;
                        }
                        else if (item.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                        {
                            dt.DoneAmount += 1;
                        }
                        else
                        {
                            if (item.IS_WAIT_CHILD == Constant.IS_FALSE)
                            {
                                dt.ProcessingAmount += 1;
                            }
                            else
                            {
                                dt.ResultSubclinicalAmount += 1;
                            }
                        }
                    }
                    result = details.Values.ToList();
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

        internal List<ServiceReqExamDateDDO> GetExamDate(ServiceReqExamDateFilter filter)
        {
            List<ServiceReqExamDateDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                    .Append("SELECT SERV.ID, SERV.SERVICE_REQ_CODE, SERV.SERVICE_REQ_TYPE_ID, SERV.SERVICE_REQ_STT_ID, SERV.TREATMENT_ID, SERV.INTRUCTION_DATE, SERV.EXECUTE_ROOM_ID, SERV.EXECUTE_DEPARTMENT_ID, SERV.START_TIME, SERV.FINISH_TIME, SERV.IS_WAIT_CHILD")
                    .Append(" FROM HIS_SERVICE_REQ SERV")
                    .Append(" WHERE SERV.IS_DELETE = 0 AND (SERV.IS_NO_EXECUTE IS NULL OR SERV.IS_NO_EXECUTE <> 1)")
                    .Append(" AND SERV.SERVICE_REQ_TYPE_ID = 1 AND SERV.SERVICE_REQ_STT_ID = 3")
                    .Append(" AND SERV.EXECUTE_DEPARTMENT_ID =: param1")
                    .Append(" AND SERV.INTRUCTION_DATE BETWEEN : param2 AND : param3");

                string sql = sb.ToString();

                List<ServiceReqData> datas = DAOWorker.SqlDAO.GetSql<ServiceReqData>(sql, filter.DepartmentId, filter.IntructionDateFrom, filter.IntructionDateTo);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<ServiceReqExamDateDDO>();
                    var Groups = datas.GroupBy(g => g.INTRUCTION_DATE).ToList();
                    foreach (var group in Groups)
                    {
                        ServiceReqExamDateDDO ddo = new ServiceReqExamDateDDO();
                        ddo.IntructionDate = group.Key;
                        ddo.Amount = (decimal)group.Count();
                        result.Add(ddo);
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



    class ServiceReqData
    {
        public long ID { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public long SERVICE_REQ_TYPE_ID { get; set; }
        public long SERVICE_REQ_STT_ID { get; set; }
        public long TREATMENT_ID { get; set; }
        public long INTRUCTION_DATE { get; set; }
        public long EXECUTE_ROOM_ID { get; set; }
        public long EXECUTE_DEPARTMENT_ID { get; set; }
        public long? START_TIME { get; set; }
        public long? FINISH_TIME { get; set; }
        public short? IS_WAIT_CHILD { get; set; }
    }

    class ServiceReqExtExeRoom : ServiceReqData
    {
        public string EXECUTE_ROOM_CODE { get; set; }
        public string EXECUTE_ROOM_NAME { get; set; }
    }

    class ServiceReqExtSubClinical : ServiceReqData
    {
        public long INTRUCTION_TIME { get; set; }
        public short? IS_HAS_TEST { get; set; }
        public short? IS_HAS_CDHA { get; set; }
        public short? IS_HAS_TDCN { get; set; }
    }
}
