using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientClassify
{
    partial class HisPatientClassifyGet : BusinessBase
    {
        internal HisPatientClassifyGet()
            : base()
        {

        }

        internal HisPatientClassifyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PATIENT_CLASSIFY> Get(HisPatientClassifyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientClassifyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_CLASSIFY GetById(long id)
        {
            try
            {
                return GetById(id, new HisPatientClassifyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_CLASSIFY GetById(long id, HisPatientClassifyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientClassifyDAO.GetById(id, filter.Query());
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
