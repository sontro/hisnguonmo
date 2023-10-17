using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverGet : BusinessBase
    {
        internal List<V_HIS_KSK_DRIVER> GetView(HisKskDriverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_DRIVER GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisKskDriverViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_KSK_DRIVER GetViewById(long id, HisKskDriverViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverDAO.GetViewById(id, filter.Query());
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
