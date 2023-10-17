using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKsk
{
    public partial class HisKskDAO : EntityBase
    {
        public List<V_HIS_KSK> GetView(HisKskSO search, CommonParam param)
        {
            List<V_HIS_KSK> result = new List<V_HIS_KSK>();
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

        public V_HIS_KSK GetViewById(long id, HisKskSO search)
        {
            V_HIS_KSK result = null;

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
