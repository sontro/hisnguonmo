using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisArea
{
    partial class HisAreaGet : BusinessBase
    {
        internal HisAreaGet()
            : base()
        {

        }

        internal HisAreaGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_AREA> Get(HisAreaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAreaDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_AREA GetById(long id)
        {
            try
            {
                return GetById(id, new HisAreaFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_AREA GetById(long id, HisAreaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAreaDAO.GetById(id, filter.Query());
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
