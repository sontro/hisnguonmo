using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPermission
{
    partial class HisPermissionGet : BusinessBase
    {
        internal HisPermissionGet()
            : base()
        {

        }

        internal HisPermissionGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PERMISSION> Get(HisPermissionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPermissionDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PERMISSION GetById(long id)
        {
            try
            {
                return GetById(id, new HisPermissionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PERMISSION GetById(long id, HisPermissionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPermissionDAO.GetById(id, filter.Query());
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
