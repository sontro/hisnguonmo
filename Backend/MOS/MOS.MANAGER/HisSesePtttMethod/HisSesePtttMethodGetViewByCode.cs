using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    partial class HisSesePtttMethodGet : BusinessBase
    {
        internal V_HIS_SESE_PTTT_METHOD GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSesePtttMethodViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SESE_PTTT_METHOD GetViewByCode(string code, HisSesePtttMethodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSesePtttMethodDAO.GetViewByCode(code, filter.Query());
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
