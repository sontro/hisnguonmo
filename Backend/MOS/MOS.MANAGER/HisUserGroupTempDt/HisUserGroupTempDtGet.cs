using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtGet : BusinessBase
    {
        internal HisUserGroupTempDtGet()
            : base()
        {

        }

        internal HisUserGroupTempDtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_USER_GROUP_TEMP_DT> Get(HisUserGroupTempDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_GROUP_TEMP_DT GetById(long id)
        {
            try
            {
                return GetById(id, new HisUserGroupTempDtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_GROUP_TEMP_DT GetById(long id, HisUserGroupTempDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserGroupTempDtDAO.GetById(id, filter.Query());
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
