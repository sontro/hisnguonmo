using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiseaseType
{
    partial class HisDiseaseTypeGet : BusinessBase
    {
        internal HisDiseaseTypeGet()
            : base()
        {

        }

        internal HisDiseaseTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DISEASE_TYPE> Get(HisDiseaseTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDiseaseTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISEASE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisDiseaseTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DISEASE_TYPE GetById(long id, HisDiseaseTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDiseaseTypeDAO.GetById(id, filter.Query());
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
