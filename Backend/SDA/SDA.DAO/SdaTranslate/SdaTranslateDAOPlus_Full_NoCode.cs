using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaTranslate
{
    public partial class SdaTranslateDAO : EntityBase
    {
        public List<V_SDA_TRANSLATE> GetView(SdaTranslateSO search, CommonParam param)
        {
            List<V_SDA_TRANSLATE> result = new List<V_SDA_TRANSLATE>();
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

        public V_SDA_TRANSLATE GetViewById(long id, SdaTranslateSO search)
        {
            V_SDA_TRANSLATE result = null;

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
