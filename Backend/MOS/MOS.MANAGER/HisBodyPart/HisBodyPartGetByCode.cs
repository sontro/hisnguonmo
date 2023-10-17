using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBodyPart
{
    partial class HisBodyPartGet : BusinessBase
    {
        internal HIS_BODY_PART GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBodyPartFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BODY_PART GetByCode(string code, HisBodyPartFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBodyPartDAO.GetByCode(code, filter.Query());
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
