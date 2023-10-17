using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMaty
{
    partial class HisPrepareMatyGet : BusinessBase
    {
        internal HisPrepareMatyGet()
            : base()
        {

        }

        internal HisPrepareMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PREPARE_MATY> Get(HisPrepareMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PREPARE_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisPrepareMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PREPARE_MATY GetById(long id, HisPrepareMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareMatyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PREPARE_MATY> GetByPrepareId(long prepareId)
        {
            try
            {
                HisPrepareMatyFilterQuery filter = new HisPrepareMatyFilterQuery();
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

        internal List<HIS_PREPARE_MATY> GetByPrepareIds(List<long> prepareIds)
        {
            try
            {
                HisPrepareMatyFilterQuery filter = new HisPrepareMatyFilterQuery();
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
