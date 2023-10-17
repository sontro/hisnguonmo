using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiGet : BusinessBase
    {
        internal HisAntibioticMicrobiGet()
            : base()
        {

        }

        internal HisAntibioticMicrobiGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ANTIBIOTIC_MICROBI> Get(HisAntibioticMicrobiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticMicrobiDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_MICROBI GetById(long id)
        {
            try
            {
                return GetById(id, new HisAntibioticMicrobiFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_MICROBI GetById(long id, HisAntibioticMicrobiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticMicrobiDAO.GetById(id, filter.Query());
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
