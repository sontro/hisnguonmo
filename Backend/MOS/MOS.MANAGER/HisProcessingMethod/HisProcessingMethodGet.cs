using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    partial class HisProcessingMethodGet : BusinessBase
    {
        internal HisProcessingMethodGet()
            : base()
        {

        }

        internal HisProcessingMethodGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PROCESSING_METHOD> Get(HisProcessingMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProcessingMethodDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PROCESSING_METHOD GetById(long id)
        {
            try
            {
                return GetById(id, new HisProcessingMethodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PROCESSING_METHOD GetById(long id, HisProcessingMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProcessingMethodDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PROCESSING_METHOD GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisProcessingMethodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PROCESSING_METHOD GetByCode(string code, HisProcessingMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisProcessingMethodDAO.GetByCode(code, filter.Query());
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
