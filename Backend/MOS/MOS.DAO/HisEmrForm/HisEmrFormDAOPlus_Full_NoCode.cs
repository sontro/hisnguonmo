using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrForm
{
    public partial class HisEmrFormDAO : EntityBase
    {
        public List<V_HIS_EMR_FORM> GetView(HisEmrFormSO search, CommonParam param)
        {
            List<V_HIS_EMR_FORM> result = new List<V_HIS_EMR_FORM>();
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

        public V_HIS_EMR_FORM GetViewById(long id, HisEmrFormSO search)
        {
            V_HIS_EMR_FORM result = null;

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
