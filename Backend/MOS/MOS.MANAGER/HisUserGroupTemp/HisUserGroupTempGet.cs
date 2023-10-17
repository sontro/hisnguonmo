using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTemp
{
    partial class HisUserGroupTempGet : BusinessBase
    {
        internal HisUserGroupTempGet()
            : base()
        {

        }

        internal HisUserGroupTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_USER_GROUP_TEMP> Get(HisUserGroupTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_GROUP_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisUserGroupTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_GROUP_TEMP GetById(long id, HisUserGroupTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDAO.GetById(id, filter.Query());
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
