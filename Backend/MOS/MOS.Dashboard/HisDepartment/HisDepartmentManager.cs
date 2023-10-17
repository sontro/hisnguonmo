using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.Dashboard.HisDepartment
{
    public partial class HisDepartmentManager : BusinessBase
    {
        public HisDepartmentManager()
            : base()
        {

        }

        public HisDepartmentManager(CommonParam param)
            : base(param)
        {

        }

        public ApiResultObject<List<DepartmentDDO>> Get(DepartmentFilter filter)
        {
            ApiResultObject<List<DepartmentDDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<DepartmentDDO> resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            this.Logger(result, filter);
            return result;
        }
    }
}
