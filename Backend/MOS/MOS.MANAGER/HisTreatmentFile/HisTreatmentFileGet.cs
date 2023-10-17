using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentFile
{
    partial class HisTreatmentFileGet : BusinessBase
    {
        internal HisTreatmentFileGet()
            : base()
        {

        }

        internal HisTreatmentFileGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TREATMENT_FILE> Get(HisTreatmentFileFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentFileDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_FILE GetById(long id)
        {
            try
            {
                return GetById(id, new HisTreatmentFileFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_FILE GetById(long id, HisTreatmentFileFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentFileDAO.GetById(id, filter.Query());
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
