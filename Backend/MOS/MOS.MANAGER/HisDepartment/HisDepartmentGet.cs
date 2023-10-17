using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisDepartment
{
    partial class HisDepartmentGet : GetBase
    {
        internal HisDepartmentGet()
            : base()
        {

        }

        internal HisDepartmentGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEPARTMENT> Get(HisDepartmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepartmentDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPARTMENT GetById(long id)
        {
            try
            {
                return GetById(id, new HisDepartmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPARTMENT GetById(long id, HisDepartmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepartmentDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPARTMENT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDepartmentFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPARTMENT GetByCode(string code, HisDepartmentFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepartmentDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisDeparmentTDO> GetTdo()
        {
            try
            {
                List<HIS_DEPARTMENT> departments = this.Get(new HisDepartmentFilterQuery());

                string reqDepartmentCodePrefix = LisLabconnCFG.REQUEST_DEPRTMENT_CODE_PREFIX != null ? LisLabconnCFG.REQUEST_DEPRTMENT_CODE_PREFIX : "";

                if (IsNotNullOrEmpty(departments))
                {
                    return departments.Select(o => new HisDeparmentTDO
                    {
                        DepartmentCode = reqDepartmentCodePrefix + o.DEPARTMENT_CODE,
                        DepartmentName = o.DEPARTMENT_NAME
                    }).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_DEPARTMENT> GetByBranchId(long id)
        {
            try
            {
                HisDepartmentFilterQuery filter = new HisDepartmentFilterQuery();
                filter.BRANCH_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
