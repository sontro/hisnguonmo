using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiimType
{
    partial class HisDiimTypeGet : BusinessBase
    {
        internal HisDiimTypeGet()
            : base()
        {

        }

        internal HisDiimTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DIIM_TYPE> Get(HisDiimTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDiimTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DIIM_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisDiimTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DIIM_TYPE GetById(long id, HisDiimTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDiimTypeDAO.GetById(id, filter.Query());
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
