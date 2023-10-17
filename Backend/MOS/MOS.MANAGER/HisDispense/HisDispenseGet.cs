using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispense
{
    partial class HisDispenseGet : BusinessBase
    {
        internal HisDispenseGet()
            : base()
        {

        }

        internal HisDispenseGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DISPENSE> Get(HisDispenseFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDispenseDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISPENSE GetById(long id)
        {
            try
            {
                return GetById(id, new HisDispenseFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISPENSE GetById(long id, HisDispenseFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDispenseDAO.GetById(id, filter.Query());
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
