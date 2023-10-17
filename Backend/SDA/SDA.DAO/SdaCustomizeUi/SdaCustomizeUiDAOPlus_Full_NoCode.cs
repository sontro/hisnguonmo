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
    }
}
