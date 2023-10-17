using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentMember
{
    partial class HisAssessmentMemberGet : BusinessBase
    {
        internal HisAssessmentMemberGet()
            : base()
        {

        }

        internal HisAssessmentMemberGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ASSESSMENT_MEMBER> Get(HisAssessmentMemberFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentMemberDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_MEMBER GetById(long id)
        {
            try
            {
                return GetById(id, new HisAssessmentMemberFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ASSESSMENT_MEMBER GetById(long id, HisAssessmentMemberFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAssessmentMemberDAO.GetById(id, filter.Query());
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
