using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverType
{
    partial class HisEmrCoverTypeGet : BusinessBase
    {
        internal HisEmrCoverTypeGet()
            : base()
        {

        }

        internal HisEmrCoverTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EMR_COVER_TYPE> Get(HisEmrCoverTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_COVER_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisEmrCoverTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_COVER_TYPE GetById(long id, HisEmrCoverTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverTypeDAO.GetById(id, filter.Query());
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
