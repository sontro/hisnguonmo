using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegGet : BusinessBase
    {
        internal HisAntibioticOldRegGet()
            : base()
        {

        }

        internal HisAntibioticOldRegGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ANTIBIOTIC_OLD_REG> Get(HisAntibioticOldRegFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticOldRegDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_OLD_REG GetById(long id)
        {
            try
            {
                return GetById(id, new HisAntibioticOldRegFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIBIOTIC_OLD_REG GetById(long id, HisAntibioticOldRegFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntibioticOldRegDAO.GetById(id, filter.Query());
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
