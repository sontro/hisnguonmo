using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRegisterGate
{
    class HisRegisterGateGetSql : GetBase
    {
        internal HisRegisterGateGetSql()
            : base()
        {

        }

        internal HisRegisterGateGetSql(CommonParam param)
            : base(param)
        {

        }

        internal List<HisRegisterGateSDO> GetCurrentNumOrder(HisRegisterGateCurrentNumOrderFilter filter)
        {
            List<HisRegisterGateSDO> result = null;
            try
            {
                long date = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + "000000");
                if (filter.REGISTER_DATE.HasValue)
                {
                    date = filter.REGISTER_DATE.Value;
                }
                string sql = "SELECT A.ID, "
                + "A.IS_ACTIVE, "
                + "A.REGISTER_GATE_CODE, "
                + "A.REGISTER_GATE_NAME, "
                + "A.IS_RESET_AFTER_NOON, "
                + "(SELECT B.NUM_ORDER FROM HIS_REGISTER_REQ B WHERE B.REGISTER_GATE_ID = A.ID AND B.REGISTER_DATE = :param1 ORDER BY REGISTER_TIME DESC FETCH FIRST ROWS ONLY) AS CURRENT_NUM_ORDER "
                + "FROM HIS_REGISTER_GATE A";

                List<DataGate> gates = DAOWorker.SqlDAO.GetSql<DataGate>(sql, date);
                if (filter.ID.HasValue)
                {
                    gates = gates != null ? gates.Where(o => o.ID == filter.ID.Value).ToList() : null;
                }
                if (filter.IDs != null)
                {
                    gates = gates != null ? gates.Where(o => filter.IDs.Contains(o.ID)).ToList() : null;
                }
                if (filter.IS_ACTIVE.HasValue)
                {
                    gates = gates != null ? gates.Where(o => o.IS_ACTIVE == filter.IS_ACTIVE.Value).ToList() : null;
                }
                result = new List<HisRegisterGateSDO>();
                if (IsNotNullOrEmpty(gates))
                {
                    result = (from r in gates select new HisRegisterGateSDO() { Id = r.ID, RegisterGateCode = r.REGISTER_GATE_CODE, RegisterGateName = r.REGISTER_GATE_NAME, IsResetAfterNoon = r.IS_RESET_AFTER_NOON, CurrentNumOrder = r.CURRENT_NUM_ORDER }).ToList();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }

        internal List<RegisterGateDepartmentSDO> GetDepartment()
        {
            List<RegisterGateDepartmentSDO> result = null;
            try
            {
                List<HIS_DEPARTMENT> departments = DAOWorker.SqlDAO.GetSql<HIS_DEPARTMENT>("SELECT DEPA.* FROM HIS_DEPARTMENT DEPA WHERE DEPA.IS_ACTIVE = 1 AND EXISTS (SELECT 1 FROM HIS_REGISTER_GATE WHERE DEPARTMENT_ID = DEPA.ID)");

                result = new List<RegisterGateDepartmentSDO>();
                if (IsNotNullOrEmpty(departments))
                {
                    departments.OrderBy(o => o.NUM_ORDER ?? 0).ThenBy(t => t.DEPARTMENT_NAME).ToList();
                    foreach (HIS_DEPARTMENT depa in departments)
                    {
                        RegisterGateDepartmentSDO sdo = new RegisterGateDepartmentSDO();
                        sdo.DepartmentId = depa.ID;
                        sdo.DepartmentName = depa.DEPARTMENT_NAME;
                        result.Add(sdo);
                    }
                }
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

    public class DataGate
    {
        public long ID { get; set; }
        public short? IS_ACTIVE { get; set; }
        public string REGISTER_GATE_CODE { get; set; }
        public string REGISTER_GATE_NAME { get; set; }
        public short? IS_RESET_AFTER_NOON { get; set; }
        public long? CURRENT_NUM_ORDER { get; set; }
    }
}
