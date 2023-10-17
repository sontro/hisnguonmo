using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.HisSereServ
{
    class HisSereServGet : GetBase
    {
        internal HisSereServGet()
            : base()
        {

        }

        internal HisSereServGet(CommonParam param)
            : base(param)
        {

        }


        internal List<SereServGeneralByDepaDDO> GetGeneralByDepartment(SereServGeneralByDepaFilter filter)
        {
            List<SereServGeneralByDepaDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                    .Append("SELECT SESE.ID, SESE.SERVICE_ID, SESE.PATIENT_TYPE_ID, SESE.AMOUNT, SESE.TDL_INTRUCTION_DATE, SESE.TDL_SERVICE_CODE, SESE.TDL_SERVICE_NAME, SESE.TDL_SERVICE_TYPE_ID, SESE.TDL_HEIN_SERVICE_TYPE_ID, SESE.TDL_REQUEST_ROOM_ID, SESE.TDL_REQUEST_DEPARTMENT_ID, SESE.TDL_SERVICE_REQ_TYPE_ID, SETY.SERVICE_TYPE_CODE, SETY.SERVICE_TYPE_NAME ")
                    .Append(" FROM HIS_SERE_SERV SESE")
                    .Append(" JOIN HIS_SERVICE_TYPE SETY ON SESE.TDL_SERVICE_TYPE_ID = SETY.ID")
                    .Append(" WHERE SESE.IS_DELETE = 0 AND SESE.SERVICE_REQ_ID IS NOT NULL")
                    .Append(" AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> 1) AND SESE.AMOUNT > 0")
                    .Append(" AND SESE.TDL_HEIN_SERVICE_TYPE_ID =: param1")
                    .Append(" AND SESE.TDL_INTRUCTION_DATE =: param2")
                    .Append(" AND SESE.TDL_REQUEST_DEPARTMENT_ID =: param3");

                string sql = sb.ToString();

                List<SereServExtServiceType> datas = DAOWorker.SqlDAO.GetSql<SereServExtServiceType>(sql, IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CDHA, filter.IntructionDate, filter.DepartmentId);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<SereServGeneralByDepaDDO>();
                    var Groups = datas.GroupBy(g => g.TDL_SERVICE_TYPE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        SereServExtServiceType first = group.FirstOrDefault();
                        SereServGeneralByDepaDDO ddo = new SereServGeneralByDepaDDO();
                        ddo.Amount = group.Sum(s => s.AMOUNT);
                        ddo.GroupCode = first.SERVICE_TYPE_CODE;
                        ddo.GroupName = first.SERVICE_TYPE_NAME;
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


        internal List<SereServGeneralByDepaDDO> GetGeneralTestByDepartment(SereServGeneralByDepaFilter filter)
        {
            List<SereServGeneralByDepaDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                    .Append("SELECT SESE.ID, SESE.SERVICE_ID, SESE.PATIENT_TYPE_ID, SESE.AMOUNT, SESE.TDL_INTRUCTION_DATE, SESE.TDL_SERVICE_CODE, SESE.TDL_SERVICE_NAME, SESE.TDL_SERVICE_TYPE_ID, SESE.TDL_HEIN_SERVICE_TYPE_ID, SESE.TDL_REQUEST_ROOM_ID, SESE.TDL_REQUEST_DEPARTMENT_ID, SESE.TDL_SERVICE_REQ_TYPE_ID, SERV.PARENT_ID AS PARENT_SERVICE_ID, PARE.SERVICE_CODE AS PARENT_SERVICE_CODE, PARE.SERVICE_NAME AS PARENT_SERVICE_NAME ")
                    .Append(" FROM HIS_SERE_SERV SESE")
                    .Append(" JOIN HIS_SERVICE SERV ON SESE.SERVICE_ID = SERV.ID")
                    .Append(" LEFT JOIN HIS_SERVICE PARE ON SERV.PARENT_ID = PARE.ID")
                    .Append(" WHERE SESE.IS_DELETE = 0 AND SESE.SERVICE_REQ_ID IS NOT NULL")
                    .Append(" AND (SESE.IS_NO_EXECUTE IS NULL OR SESE.IS_NO_EXECUTE <> 1) AND SESE.AMOUNT > 0")
                    .Append(" AND SESE.TDL_SERVICE_TYPE_ID =: param1")
                    .Append(" AND SESE.TDL_INTRUCTION_DATE =: param2")
                    .Append(" AND SESE.TDL_REQUEST_DEPARTMENT_ID =: param3");

                string sql = sb.ToString();

                List<SereServExtParent> datas = DAOWorker.SqlDAO.GetSql<SereServExtParent>(sql, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN, filter.IntructionDate, filter.DepartmentId);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<SereServGeneralByDepaDDO>();
                    var Groups = datas.GroupBy(g => g.PARENT_SERVICE_ID ?? 0).ToList();
                    foreach (var group in Groups)
                    {
                        if (group.Key == 0)
                        {
                            var GroupByService = group.GroupBy(g => g.SERVICE_ID).ToList();
                            foreach (var g in GroupByService)
                            {
                                SereServExtParent first = g.FirstOrDefault();
                                SereServGeneralByDepaDDO ddo = new SereServGeneralByDepaDDO();
                                ddo.Amount = g.Sum(s => s.AMOUNT);
                                ddo.GroupCode = first.TDL_SERVICE_CODE;
                                ddo.GroupName = first.TDL_SERVICE_NAME;
                                result.Add(ddo);
                            }
                        }
                        else
                        {
                            SereServExtParent first = group.FirstOrDefault();
                            SereServGeneralByDepaDDO ddo = new SereServGeneralByDepaDDO();
                            ddo.Amount = group.Sum(s => s.AMOUNT);
                            ddo.GroupCode = first.PARENT_SERVICE_CODE;
                            ddo.GroupName = first.PARENT_SERVICE_NAME;
                            result.Add(ddo);
                        }
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

    class SereServData
    {
        public long ID { get; set; }
        public long SERVICE_ID { get; set; }
        public long PATIENT_TYPE_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public long TDL_INTRUCTION_DATE { get; set; }
        public string TDL_SERVICE_CODE { get; set; }
        public string TDL_SERVICE_NAME { get; set; }
        public long TDL_SERVICE_TYPE_ID { get; set; }
        public Nullable<long> TDL_HEIN_SERVICE_TYPE_ID { get; set; }
        public long TDL_REQUEST_ROOM_ID { get; set; }
        public long TDL_REQUEST_DEPARTMENT_ID { get; set; }
        public long TDL_SERVICE_REQ_TYPE_ID { get; set; }
    }

    class SereServExtServiceType : SereServData
    {
        public string SERVICE_TYPE_CODE { get; set; }
        public string SERVICE_TYPE_NAME { get; set; }
    }

    class SereServExtParent : SereServData
    {
        public long? PARENT_SERVICE_ID { get; set; }
        public string PARENT_SERVICE_CODE { get; set; }
        public string PARENT_SERVICE_NAME { get; set; }
    }
}
