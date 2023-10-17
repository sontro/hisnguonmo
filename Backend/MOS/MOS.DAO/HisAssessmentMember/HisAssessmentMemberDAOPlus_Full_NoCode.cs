using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAssessmentMember
{
    public partial class HisAssessmentMemberDAO : EntityBase
    {
        public List<V_HIS_ASSESSMENT_MEMBER> GetView(HisAssessmentMemberSO search, CommonParam param)
        {
            List<V_HIS_ASSESSMENT_MEMBER> result = new List<V_HIS_ASSESSMENT_MEMBER>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_ASSESSMENT_MEMBER GetViewById(long id, HisAssessmentMemberSO search)
        {
            V_HIS_ASSESSMENT_MEMBER result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
