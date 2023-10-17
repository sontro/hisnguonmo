using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticRequest
{
    partial class HisAntibioticRequestGet : BusinessBase
    {
        internal HisAntibioticRequestGet()
            : base()
        {

        }

        internal HisAntibioticRequestGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ANTIBIOTIC_REQUEST> Get(HisAntibioticRequestFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticRequestDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_REQUEST GetById(long id)
        {
            try
            {
                return GetById(id, new HisAntibioticRequestFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_REQUEST GetById(long id, HisAntibioticRequestFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticRequestDAO.GetById(id, filter.Query());
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
