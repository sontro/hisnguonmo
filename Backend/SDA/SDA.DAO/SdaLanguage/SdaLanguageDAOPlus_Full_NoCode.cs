using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaLanguage
{
    public partial class SdaLanguageDAO : EntityBase
    {
        public List<V_SDA_LANGUAGE> GetView(SdaLanguageSO search, CommonParam param)
        {
            List<V_SDA_LANGUAGE> result = new List<V_SDA_LANGUAGE>();
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

        public V_SDA_LANGUAGE GetViewById(long id, SdaLanguageSO search)
        {
            V_SDA_LANGUAGE result = null;

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
