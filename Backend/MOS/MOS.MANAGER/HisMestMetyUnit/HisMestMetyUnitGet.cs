using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    partial class HisMestMetyUnitGet : BusinessBase
    {
        internal HisMestMetyUnitGet()
            : base()
        {

        }

        internal HisMestMetyUnitGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEST_METY_UNIT> Get(HisMestMetyUnitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyUnitDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_METY_UNIT GetById(long id)
        {
            try
            {
                return GetById(id, new HisMestMetyUnitFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_METY_UNIT GetById(long id, HisMestMetyUnitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMetyUnitDAO.GetById(id, filter.Query());
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
