using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaexVaer
{
    partial class HisVaexVaerGet : BusinessBase
    {
        internal HisVaexVaerGet()
            : base()
        {

        }

        internal HisVaexVaerGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VAEX_VAER> Get(HisVaexVaerFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaexVaerDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VAEX_VAER> GetByVaccinationExamId(long vaccinationExamId)
        {
            HisVaexVaerFilterQuery filter = new HisVaexVaerFilterQuery();
            filter.VACCINATION_EXAM_ID = vaccinationExamId;
            return this.Get(filter);
        }

        internal HIS_VAEX_VAER GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaexVaerFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VAEX_VAER GetById(long id, HisVaexVaerFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaexVaerDAO.GetById(id, filter.Query());
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
