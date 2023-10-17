using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    partial class HisWorkingShiftGet : BusinessBase
    {
        internal HisWorkingShiftGet()
            : base()
        {

        }

        internal HisWorkingShiftGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_WORKING_SHIFT> Get(HisWorkingShiftFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWorkingShiftDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WORKING_SHIFT GetById(long id)
        {
            try
            {
                return GetById(id, new HisWorkingShiftFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WORKING_SHIFT GetById(long id, HisWorkingShiftFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWorkingShiftDAO.GetById(id, filter.Query());
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
