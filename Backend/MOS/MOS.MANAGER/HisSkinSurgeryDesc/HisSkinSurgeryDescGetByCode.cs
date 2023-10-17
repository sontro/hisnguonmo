using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : BusinessBase
    {
        internal HIS_SKIN_SURGERY_DESC GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSkinSurgeryDescFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SKIN_SURGERY_DESC GetByCode(string code, HisSkinSurgeryDescFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSkinSurgeryDescDAO.GetByCode(code, filter.Query());
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
