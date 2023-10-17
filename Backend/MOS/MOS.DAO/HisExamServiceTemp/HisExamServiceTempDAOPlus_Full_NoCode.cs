using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExamServiceTemp
{
    public partial class HisExamServiceTempDAO : EntityBase
    {
        public List<V_HIS_EXAM_SERVICE_TEMP> GetView(HisExamServiceTempSO search, CommonParam param)
        {
            List<V_HIS_EXAM_SERVICE_TEMP> result = new List<V_HIS_EXAM_SERVICE_TEMP>();
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

        public V_HIS_EXAM_SERVICE_TEMP GetViewById(long id, HisExamServiceTempSO search)
        {
            V_HIS_EXAM_SERVICE_TEMP result = null;

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
