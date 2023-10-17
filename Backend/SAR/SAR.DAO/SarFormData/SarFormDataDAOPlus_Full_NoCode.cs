using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarFormData
{
    public partial class SarFormDataDAO : EntityBase
    {
        public List<V_SAR_FORM_DATA> GetView(SarFormDataSO search, CommonParam param)
        {
            List<V_SAR_FORM_DATA> result = new List<V_SAR_FORM_DATA>();
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

        public V_SAR_FORM_DATA GetViewById(long id, SarFormDataSO search)
        {
            V_SAR_FORM_DATA result = null;

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
