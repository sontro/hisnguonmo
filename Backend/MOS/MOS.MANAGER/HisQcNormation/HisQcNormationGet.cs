using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation
{
    partial class HisQcNormationGet : BusinessBase
    {
        internal HisQcNormationGet()
            : base()
        {

        }

        internal HisQcNormationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_QC_NORMATION> Get(HisQcNormationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcNormationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_QC_NORMATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisQcNormationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_QC_NORMATION GetById(long id, HisQcNormationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcNormationDAO.GetById(id, filter.Query());
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
