using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    partial class HisTransfusionGet : BusinessBase
    {
        internal HisTransfusionGet()
            : base()
        {

        }

        internal HisTransfusionGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRANSFUSION> Get(HisTransfusionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSFUSION GetById(long id)
        {
            try
            {
                return GetById(id, new HisTransfusionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSFUSION GetById(long id, HisTransfusionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_TRANSFUSION> GetByTransfusionSumId(long transfusionSumId)
        {
            try
            {
                HisTransfusionFilterQuery filter = new HisTransfusionFilterQuery();
                filter.TRANSFUSION_SUM_ID = transfusionSumId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
