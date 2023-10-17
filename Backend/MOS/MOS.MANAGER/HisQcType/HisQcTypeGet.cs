using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcType
{
    partial class HisQcTypeGet : BusinessBase
    {
        internal HisQcTypeGet()
            : base()
        {

        }

        internal HisQcTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_QC_TYPE> Get(HisQcTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_QC_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisQcTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_QC_TYPE GetById(long id, HisQcTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisQcTypeDAO.GetById(id, filter.Query());
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
