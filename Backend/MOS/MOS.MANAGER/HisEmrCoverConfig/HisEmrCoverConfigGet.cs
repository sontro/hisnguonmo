using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigGet : BusinessBase
    {
        internal HisEmrCoverConfigGet()
            : base()
        {

        }

        internal HisEmrCoverConfigGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EMR_COVER_CONFIG> Get(HisEmrCoverConfigFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverConfigDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_COVER_CONFIG GetById(long id)
        {
            try
            {
                return GetById(id, new HisEmrCoverConfigFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_COVER_CONFIG GetById(long id, HisEmrCoverConfigFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverConfigDAO.GetById(id, filter.Query());
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
