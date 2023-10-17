using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    partial class HisStationGet : BusinessBase
    {
        internal HisStationGet()
            : base()
        {

        }

        internal HisStationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_STATION> Get(HisStationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisStationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STATION GetById(long id, HisStationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStationDAO.GetById(id, filter.Query());
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
