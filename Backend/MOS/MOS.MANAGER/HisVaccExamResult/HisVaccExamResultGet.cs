using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccExamResult
{
    partial class HisVaccExamResultGet : BusinessBase
    {
        internal HisVaccExamResultGet()
            : base()
        {

        }

        internal HisVaccExamResultGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACC_EXAM_RESULT> Get(HisVaccExamResultFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccExamResultDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_EXAM_RESULT GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccExamResultFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_EXAM_RESULT GetById(long id, HisVaccExamResultFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccExamResultDAO.GetById(id, filter.Query());
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
