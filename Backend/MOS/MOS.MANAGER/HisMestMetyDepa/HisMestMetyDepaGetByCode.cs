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
        internal HIS_MEST_METY_DEPA GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMestMetyDepaFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_METY_DEPA GetByCode(string code, HisMestMetyDepaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyDepaDAO.GetByCode(code, filter.Query());
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
