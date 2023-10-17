using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDrugIntervention
{
    partial class HisDrugInterventionGet : BusinessBase
    {
        internal HisDrugInterventionGet()
            : base()
        {

        }

        internal HisDrugInterventionGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DRUG_INTERVENTION> Get(HisDrugInterventionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDrugInterventionDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DRUG_INTERVENTION GetById(long id)
        {
            try
            {
                return GetById(id, new HisDrugInterventionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DRUG_INTERVENTION GetById(long id, HisDrugInterventionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDrugInterventionDAO.GetById(id, filter.Query());
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
