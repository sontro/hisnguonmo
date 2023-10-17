using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestType
{
    class HisImpMestTypeCreate : BusinessBase
    {
        internal HisImpMestTypeCreate()
            : base()
        {

        }

        internal HisImpMestTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestTypeCheck checker = new HisImpMestTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_MEST_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisImpMestTypeDAO.Create(data);
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
