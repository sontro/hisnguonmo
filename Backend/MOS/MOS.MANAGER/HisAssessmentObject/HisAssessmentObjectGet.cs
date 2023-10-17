using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    partial class HisAssessmentObjectGet : BusinessBase
    {
        internal HisAssessmentObjectGet()
            : base()
        {

        }

        internal HisAssessmentObjectGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ASSESSMENT_OBJECT> Get(HisAssessmentObjectFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentObjectDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_OBJECT GetById(long id)
        {
            try
            {
                return GetById(id, new HisAssessmentObjectFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_OBJECT GetById(long id, HisAssessmentObjectFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentObjectDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_OBJECT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAssessmentObjectFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_OBJECT GetByCode(string code, HisAssessmentObjectFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentObjectDAO.GetByCode(code, filter.Query());
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
