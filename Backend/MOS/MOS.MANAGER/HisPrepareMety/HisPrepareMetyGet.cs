using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    partial class HisPrepareMetyGet : BusinessBase
    {
        internal HisPrepareMetyGet()
            : base()
        {

        }

        internal HisPrepareMetyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PREPARE_METY> Get(HisPrepareMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareMetyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PREPARE_METY GetById(long id)
        {
            try
            {
                return GetById(id, new HisPrepareMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PREPARE_METY GetById(long id, HisPrepareMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareMetyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PREPARE_METY> GetByPrepareId(long prepareId)
        {
            try
            {
                HisPrepareMetyFilterQuery filter = new HisPrepareMetyFilterQuery();
                filter.PREPARE_ID = prepareId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PREPARE_METY> GetByPrepareIds(List<long> prepareIds)
        {
            try
            {
                HisPrepareMetyFilterQuery filter = new HisPrepareMetyFilterQuery();
                filter.PREPARE_IDs = prepareIds;
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
