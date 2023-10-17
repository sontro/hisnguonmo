using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegGet : BusinessBase
    {
        internal HisAntibioticNewRegGet()
            : base()
        {

        }

        internal HisAntibioticNewRegGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ANTIBIOTIC_NEW_REG> Get(HisAntibioticNewRegFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticNewRegDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_NEW_REG GetById(long id)
        {
            try
            {
                return GetById(id, new HisAntibioticNewRegFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_NEW_REG GetById(long id, HisAntibioticNewRegFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticNewRegDAO.GetById(id, filter.Query());
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
