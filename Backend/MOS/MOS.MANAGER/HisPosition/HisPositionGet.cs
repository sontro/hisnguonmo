using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPosition
{
    partial class HisPositionGet : BusinessBase
    {
        internal HisPositionGet()
            : base()
        {

        }

        internal HisPositionGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_POSITION> Get(HisPositionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPositionDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_POSITION GetById(long id)
        {
            try
            {
                return GetById(id, new HisPositionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_POSITION GetById(long id, HisPositionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPositionDAO.GetById(id, filter.Query());
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
