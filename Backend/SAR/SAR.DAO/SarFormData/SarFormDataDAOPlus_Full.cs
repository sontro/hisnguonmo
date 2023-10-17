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

        public SAR_FORM_DATA GetByCode(string code, SarFormDataSO search)
        {
            SAR_FORM_DATA result = null;

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

        public V_SAR_FORM_DATA GetViewByCode(string code, SarFormDataSO search)
        {
            V_SAR_FORM_DATA result = null;

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

        public Dictionary<string, SAR_FORM_DATA> GetDicByCode(SarFormDataSO search, CommonParam param)
        {
            Dictionary<string, SAR_FORM_DATA> result = new Dictionary<string, SAR_FORM_DATA>();
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
