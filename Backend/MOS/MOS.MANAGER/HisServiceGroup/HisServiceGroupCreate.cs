using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceGroup
{
    class HisServiceGroupCreate : BusinessBase
    {
        internal HisServiceGroupCreate()
            : base()
        {

        }

        internal HisServiceGroupCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceGroupCheck checker = new HisServiceGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERVICE_GROUP_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisServiceGroupDAO.Create(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
