using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocHoldType
{
    partial class HisDocHoldTypeGet : BusinessBase
    {
        internal HisDocHoldTypeGet()
            : base()
        {

        }

        internal HisDocHoldTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DOC_HOLD_TYPE> Get(HisDocHoldTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocHoldTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOC_HOLD_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisDocHoldTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOC_HOLD_TYPE GetById(long id, HisDocHoldTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocHoldTypeDAO.GetById(id, filter.Query());
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
