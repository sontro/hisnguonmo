using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInteractiveGrade
{
    public partial class HisInteractiveGradeDAO : EntityBase
    {
        public List<V_HIS_INTERACTIVE_GRADE> GetView(HisInteractiveGradeSO search, CommonParam param)
        {
            List<V_HIS_INTERACTIVE_GRADE> result = new List<V_HIS_INTERACTIVE_GRADE>();
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

        public V_HIS_INTERACTIVE_GRADE GetViewById(long id, HisInteractiveGradeSO search)
        {
            V_HIS_INTERACTIVE_GRADE result = null;

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
