using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccExamResult
{
    public partial class HisVaccExamResultDAO : EntityBase
    {
        public List<V_HIS_VACC_EXAM_RESULT> GetView(HisVaccExamResultSO search, CommonParam param)
        {
            List<V_HIS_VACC_EXAM_RESULT> result = new List<V_HIS_VACC_EXAM_RESULT>();
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

        public V_HIS_VACC_EXAM_RESULT GetViewById(long id, HisVaccExamResultSO search)
        {
            V_HIS_VACC_EXAM_RESULT result = null;

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
