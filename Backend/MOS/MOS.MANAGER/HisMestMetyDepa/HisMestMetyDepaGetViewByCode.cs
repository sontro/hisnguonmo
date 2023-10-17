using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyDepa
{
    partial class HisMestMetyDepaGet : BusinessBase
    {
        internal V_HIS_MEST_METY_DEPA GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMestMetyDepaViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEST_METY_DEPA GetViewByCode(string code, HisMestMetyDepaViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyDepaDAO.GetViewByCode(code, filter.Query());
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
