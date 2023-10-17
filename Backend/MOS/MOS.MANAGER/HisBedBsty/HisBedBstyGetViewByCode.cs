using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedBsty
{
    partial class HisBedBstyGet : BusinessBase
    {
        internal V_HIS_BED_BSTY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBedBstyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BED_BSTY GetViewByCode(string code, HisBedBstyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedBstyDAO.GetViewByCode(code, filter.Query());
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
