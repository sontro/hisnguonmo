using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaCustomizeUi
{
    public partial class SdaCustomizeUiDAO : EntityBase
    {
        public List<V_SDA_CUSTOMIZE_UI> GetView(SdaCustomizeUiSO search, CommonParam param)
        {
            List<V_SDA_CUSTOMIZE_UI> result = new List<V_SDA_CUSTOMIZE_UI>();

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

        public SDA_CUSTOMIZE_UI GetByCode(string code, SdaCustomizeUiSO search)
        {
            SDA_CUSTOMIZE_UI result = null;

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
        
        public V_SDA_CUSTOMIZE_UI GetViewById(long id, SdaCustomizeUiSO search)
        {
            V_SDA_CUSTOMIZE_UI result = null;

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

        public V_SDA_CUSTOMIZE_UI GetViewByCode(string code, SdaCustomizeUiSO search)
        {
            V_SDA_CUSTOMIZE_UI result = null;

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

        public Dictionary<string, SDA_CUSTOMIZE_UI> GetDicByCode(SdaCustomizeUiSO search, CommonParam param)
        {
            Dictionary<string, SDA_CUSTOMIZE_UI> result = new Dictionary<string, SDA_CUSTOMIZE_UI>();
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
