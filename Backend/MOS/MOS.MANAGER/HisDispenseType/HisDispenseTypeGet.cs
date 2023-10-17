using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispenseType
{
    partial class HisDispenseTypeGet : BusinessBase
    {
        internal HisDispenseTypeGet()
            : base()
        {

        }

        internal HisDispenseTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DISPENSE_TYPE> Get(HisDispenseTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDispenseTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISPENSE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisDispenseTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISPENSE_TYPE GetById(long id, HisDispenseTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDispenseTypeDAO.GetById(id, filter.Query());
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
