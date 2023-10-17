using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.HisDepartment
{
    class HisDepartmentGet : GetBase
    {
        internal HisDepartmentGet()
            : base()
        {

        }

        internal HisDepartmentGet(CommonParam param)
            : base(param)
        {

        }


        internal List<DepartmentDDO> Get(DepartmentFilter filter)
        {
            List<DepartmentDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                .Append("SELECT DEPA.ID, DEPA.IS_ACTIVE, DEPA.DEPARTMENT_CODE, DEPA.DEPARTMENT_NAME, DEPA.BRANCH_ID, DEPA.BHYT_CODE, DEPA.PHONE")
                .Append(" FROM HIS_DEPARTMENT DEPA")
                .Append(" WHERE DEPA.IS_ACTIVE = 1");

                if (filter.IsExam.HasValue)
                {
                    if (filter.IsExam.Value)
                    {
                        sb.Append(" AND EXISTS")
                        .Append(" (SELECT 1 FROM HIS_EXECUTE_ROOM EXRO")
                        .Append(" JOIN HIS_ROOM ROOM ON EXRO.ROOM_ID = ROOM.ID")
                        .Append(" WHERE ROOM.DEPARTMENT_ID = DEPA.ID AND EXRO.IS_EXAM = 1 AND EXRO.IS_ACTIVE = 1)");
                    }
                    else
                    {
                        sb.Append(" AND NOT EXISTS")
                        .Append(" (SELECT 1 FROM HIS_EXECUTE_ROOM EXRO")
                        .Append(" JOIN HIS_ROOM ROOM ON EXRO.ROOM_ID = ROOM.ID")
                        .Append(" WHERE ROOM.DEPARTMENT_ID = DEPA.ID AND EXRO.IS_EXAM = 1 AND EXRO.IS_ACTIVE = 1)");
                    }
                }

                string sql = sb.ToString();
                result = DAOWorker.SqlDAO.GetSql<DepartmentDDO>(sql);
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
}
