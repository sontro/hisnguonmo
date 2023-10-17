using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAtc
{
    partial class HisAtcGet : BusinessBase
    {
        internal HisAtcGet()
            : base()
        {

        }

        internal HisAtcGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ATC> Get(HisAtcFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAtcDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ATC GetById(long id)
        {
            try
            {
                return GetById(id, new HisAtcFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ATC GetById(long id, HisAtcFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAtcDAO.GetById(id, filter.Query());
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
