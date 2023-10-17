using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHtu
{
    public partial class HisHtuDAO : EntityBase
    {
        public List<V_HIS_HTU> GetView(HisHtuSO search, CommonParam param)
        {
            List<V_HIS_HTU> result = new List<V_HIS_HTU>();
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

        public V_HIS_HTU GetViewById(long id, HisHtuSO search)
        {
            V_HIS_HTU result = null;

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
