using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedType
{
    partial class HisBedTypeGet : BusinessBase
    {
        internal V_HIS_BED_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBedTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BED_TYPE GetViewByCode(string code, HisBedTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedTypeDAO.GetViewByCode(code, filter.Query());
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
