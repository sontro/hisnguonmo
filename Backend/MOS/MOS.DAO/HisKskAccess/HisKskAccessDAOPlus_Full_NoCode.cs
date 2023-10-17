using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskAccess
{
    public partial class HisKskAccessDAO : EntityBase
    {
        public List<V_HIS_KSK_ACCESS> GetView(HisKskAccessSO search, CommonParam param)
        {
            List<V_HIS_KSK_ACCESS> result = new List<V_HIS_KSK_ACCESS>();
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

        public V_HIS_KSK_ACCESS GetViewById(long id, HisKskAccessSO search)
        {
            V_HIS_KSK_ACCESS result = null;

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
