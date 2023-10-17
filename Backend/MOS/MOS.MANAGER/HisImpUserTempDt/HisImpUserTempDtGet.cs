using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtGet : BusinessBase
    {
        internal HisImpUserTempDtGet()
            : base()
        {

        }

        internal HisImpUserTempDtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_IMP_USER_TEMP_DT> Get(HisImpUserTempDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_USER_TEMP_DT GetById(long id)
        {
            try
            {
                return GetById(id, new HisImpUserTempDtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_USER_TEMP_DT GetById(long id, HisImpUserTempDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDtDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_IMP_USER_TEMP_DT> GetByImpUserTempId(long impUserTempId)
        {
            HisImpUserTempDtFilterQuery filter = new HisImpUserTempDtFilterQuery();
            filter.IMP_USER_TEMP_ID = impUserTempId;
            return this.Get(filter);
        }
    }
}
