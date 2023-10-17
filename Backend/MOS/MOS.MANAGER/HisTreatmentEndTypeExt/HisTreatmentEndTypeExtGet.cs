using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtGet : BusinessBase
    {
        internal HisTreatmentEndTypeExtGet()
            : base()
        {

        }

        internal HisTreatmentEndTypeExtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TREATMENT_END_TYPE_EXT> Get(HisTreatmentEndTypeExtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentEndTypeExtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_END_TYPE_EXT GetById(long id)
        {
            try
            {
                return GetById(id, new HisTreatmentEndTypeExtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_END_TYPE_EXT GetById(long id, HisTreatmentEndTypeExtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentEndTypeExtDAO.GetById(id, filter.Query());
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
