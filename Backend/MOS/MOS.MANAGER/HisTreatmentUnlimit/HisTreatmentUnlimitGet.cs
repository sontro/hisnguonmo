using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitGet : BusinessBase
    {
        internal HisTreatmentUnlimitGet()
            : base()
        {

        }

        internal HisTreatmentUnlimitGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TREATMENT_UNLIMIT> Get(HisTreatmentUnlimitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentUnlimitDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_UNLIMIT GetById(long id)
        {
            try
            {
                return GetById(id, new HisTreatmentUnlimitFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_UNLIMIT GetById(long id, HisTreatmentUnlimitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentUnlimitDAO.GetById(id, filter.Query());
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
