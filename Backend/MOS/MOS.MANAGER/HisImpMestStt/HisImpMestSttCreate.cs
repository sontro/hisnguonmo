using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestStt
{
    class HisImpMestSttCreate : BusinessBase
    {
        internal HisImpMestSttCreate()
            : base()
        {

        }

        internal HisImpMestSttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestSttCheck checker = new HisImpMestSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_MEST_STT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisImpMestSttDAO.Create(data);
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
