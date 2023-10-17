using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaCustomizaButton
{
    public partial class SdaCustomizaButtonDAO : EntityBase
    {
        public List<V_SDA_CUSTOMIZA_BUTTON> GetView(SdaCustomizaButtonSO search, CommonParam param)
        {
            List<V_SDA_CUSTOMIZA_BUTTON> result = new List<V_SDA_CUSTOMIZA_BUTTON>();

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

        public SDA_CUSTOMIZA_BUTTON GetByCode(string code, SdaCustomizaButtonSO search)
        {
            SDA_CUSTOMIZA_BUTTON result = null;

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
        
        public V_SDA_CUSTOMIZA_BUTTON GetViewById(long id, SdaCustomizaButtonSO search)
        {
            V_SDA_CUSTOMIZA_BUTTON result = null;

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

        public V_SDA_CUSTOMIZA_BUTTON GetViewByCode(string code, SdaCustomizaButtonSO search)
        {
            V_SDA_CUSTOMIZA_BUTTON result = null;

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

        public Dictionary<string, SDA_CUSTOMIZA_BUTTON> GetDicByCode(SdaCustomizaButtonSO search, CommonParam param)
        {
            Dictionary<string, SDA_CUSTOMIZA_BUTTON> result = new Dictionary<string, SDA_CUSTOMIZA_BUTTON>();
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
