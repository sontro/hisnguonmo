using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLocationStore
{
    partial class HisLocationStoreGet : BusinessBase
    {
        internal HisLocationStoreGet()
            : base()
        {

        }

        internal HisLocationStoreGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_LOCATION_STORE> Get(HisLocationStoreFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLocationStoreDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LOCATION_STORE GetById(long id)
        {
            try
            {
                return GetById(id, new HisLocationStoreFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LOCATION_STORE GetById(long id, HisLocationStoreFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLocationStoreDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LOCATION_STORE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisLocationStoreFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LOCATION_STORE GetByCode(string code, HisLocationStoreFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLocationStoreDAO.GetByCode(code, filter.Query());
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
