using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeedUnit
{
    partial class HisSpeedUnitGet : BusinessBase
    {
        internal HisSpeedUnitGet()
            : base()
        {

        }

        internal HisSpeedUnitGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SPEED_UNIT> Get(HisSpeedUnitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpeedUnitDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SPEED_UNIT GetById(long id)
        {
            try
            {
                return GetById(id, new HisSpeedUnitFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SPEED_UNIT GetById(long id, HisSpeedUnitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpeedUnitDAO.GetById(id, filter.Query());
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
