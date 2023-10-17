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

        public HIS_ASSESSMENT_OBJECT GetByCode(string code, HisAssessmentObjectSO search)
        {
            HIS_ASSESSMENT_OBJECT result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_ASSESSMENT_OBJECT GetViewByCode(string code, HisAssessmentObjectSO search)
        {
            V_HIS_ASSESSMENT_OBJECT result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_ASSESSMENT_OBJECT> GetDicByCode(HisAssessmentObjectSO search, CommonParam param)
        {
            Dictionary<string, HIS_ASSESSMENT_OBJECT> result = new Dictionary<string, HIS_ASSESSMENT_OBJECT>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
