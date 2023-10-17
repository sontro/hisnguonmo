using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaHideControl
{
    public partial class SdaHideControlDAO : EntityBase
    {
        public List<V_SDA_HIDE_CONTROL> GetView(SdaHideControlSO search, CommonParam param)
        {
            List<V_SDA_HIDE_CONTROL> result = new List<V_SDA_HIDE_CONTROL>();
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

        public V_SDA_HIDE_CONTROL GetViewById(long id, SdaHideControlSO search)
        {
            V_SDA_HIDE_CONTROL result = null;

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
