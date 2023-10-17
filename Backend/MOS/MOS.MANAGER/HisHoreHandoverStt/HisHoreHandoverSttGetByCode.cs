using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttGet : BusinessBase
    {
        internal HIS_HORE_HANDOVER_STT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisHoreHandoverSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HANDOVER_STT GetByCode(string code, HisHoreHandoverSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverSttDAO.GetByCode(code, filter.Query());
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
