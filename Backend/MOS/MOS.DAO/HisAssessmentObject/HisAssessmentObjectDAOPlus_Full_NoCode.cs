using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAssessmentObject
{
    public partial class HisAssessmentObjectDAO : EntityBase
    {
        public List<V_HIS_ASSESSMENT_OBJECT> GetView(HisAssessmentObjectSO search, CommonParam param)
        {
            List<V_HIS_ASSESSMENT_OBJECT> result = new List<V_HIS_ASSESSMENT_OBJECT>();
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

        public V_HIS_ASSESSMENT_OBJECT GetViewById(long id, HisAssessmentObjectSO search)
        {
            V_HIS_ASSESSMENT_OBJECT result = null;

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
