using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Get
{
    class HisTreatmentGetByDepartment : GetBase
    {
        internal HisTreatmentGetByDepartment()
            : base()
        {

        }
        internal HisTreatmentGetByDepartment(CommonParam param)
            : base(param)
        {

        }

        internal List<string> GetCardServiceCode(string departmentCode)
        {
            List<string> result = null;
            try
            {
                string sql = "SELECT DISTINCT C.SERVICE_CODE "
                            + " FROM HIS_TREATMENT T " 
                            + " JOIN HIS_DEPARTMENT DT ON DT.ID = T.LAST_DEPARTMENT_ID "
                            + " JOIN HIS_CARD C ON C.PATIENT_ID = T.PATIENT_ID "
                            + " WHERE T.IS_PAUSE IS NULL AND C.IS_ACTIVE = 1 AND DT.DEPARTMENT_CODE = :param1";
                result = DAOWorker.SqlDAO.GetSql<string>(sql, departmentCode);
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
