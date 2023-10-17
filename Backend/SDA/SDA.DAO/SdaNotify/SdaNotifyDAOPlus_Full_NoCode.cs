using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaNotify
{
    public partial class SdaNotifyDAO : EntityBase
    {
        public List<V_SDA_NOTIFY> GetView(SdaNotifySO search, CommonParam param)
        {
            List<V_SDA_NOTIFY> result = new List<V_SDA_NOTIFY>();
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

        public V_SDA_NOTIFY GetViewById(long id, SdaNotifySO search)
        {
            V_SDA_NOTIFY result = null;

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
