using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.SqlExecute
{
    public partial class SqlExecuteManager : BusinessBase
    {
        public SqlExecuteManager()
            : base()
        {

        }

        public SqlExecuteManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<bool> Run(ExecuteSqlSDO sdo)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new SqlExecutor(param).Run(sdo);
                }
                result = this.PackSuccess(resultData);
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
