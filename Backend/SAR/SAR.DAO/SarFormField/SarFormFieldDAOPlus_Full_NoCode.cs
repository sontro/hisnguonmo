using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarFormField
{
    public partial class SarFormFieldDAO : EntityBase
    {
        public List<V_SAR_FORM_FIELD> GetView(SarFormFieldSO search, CommonParam param)
        {
            List<V_SAR_FORM_FIELD> result = new List<V_SAR_FORM_FIELD>();
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

        public V_SAR_FORM_FIELD GetViewById(long id, SarFormFieldSO search)
        {
            V_SAR_FORM_FIELD result = null;

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
