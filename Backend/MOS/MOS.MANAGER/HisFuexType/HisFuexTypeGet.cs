using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFuexType
{
    partial class HisFuexTypeGet : BusinessBase
    {
        internal HisFuexTypeGet()
            : base()
        {

        }

        internal HisFuexTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_FUEX_TYPE> Get(HisFuexTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFuexTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FUEX_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisFuexTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FUEX_TYPE GetById(long id, HisFuexTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFuexTypeDAO.GetById(id, filter.Query());
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
