using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescGet : BusinessBase
    {
        internal HisEyeSurgryDescGet()
            : base()
        {

        }

        internal HisEyeSurgryDescGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EYE_SURGRY_DESC> Get(HisEyeSurgryDescFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEyeSurgryDescDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EYE_SURGRY_DESC GetById(long id)
        {
            try
            {
                return GetById(id, new HisEyeSurgryDescFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EYE_SURGRY_DESC GetById(long id, HisEyeSurgryDescFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEyeSurgryDescDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_EYE_SURGRY_DESC> GetByIds(List<long> ids)
        {
            try
            {
                HisEyeSurgryDescFilterQuery filter = new HisEyeSurgryDescFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
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
