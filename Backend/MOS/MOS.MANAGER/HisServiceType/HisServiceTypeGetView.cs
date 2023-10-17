using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceType
{
    partial class HisServiceTypeGet : GetBase
    {
        internal List<V_HIS_SERVICE_TYPE> GetView(HisServiceTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceTypeDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_TYPE GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisServiceTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_TYPE GetViewById(long id, HisServiceTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceTypeDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisServiceTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_TYPE GetViewByCode(string code, HisServiceTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceTypeDAO.GetViewByCode(code, filter.Query());
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
