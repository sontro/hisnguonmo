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
        internal HIS_SESE_PTTT_METHOD GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSesePtttMethodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_PTTT_METHOD GetByCode(string code, HisSesePtttMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSesePtttMethodDAO.GetByCode(code, filter.Query());
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
