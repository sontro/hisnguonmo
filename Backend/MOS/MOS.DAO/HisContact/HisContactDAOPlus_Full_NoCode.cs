using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisContact
{
    public partial class HisContactDAO : EntityBase
    {
        public List<V_HIS_CONTACT> GetView(HisContactSO search, CommonParam param)
        {
            List<V_HIS_CONTACT> result = new List<V_HIS_CONTACT>();
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

        public V_HIS_CONTACT GetViewById(long id, HisContactSO search)
        {
            V_HIS_CONTACT result = null;

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
