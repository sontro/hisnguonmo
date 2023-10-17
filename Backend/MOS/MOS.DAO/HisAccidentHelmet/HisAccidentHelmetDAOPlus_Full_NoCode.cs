using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHelmet
{
    public partial class HisAccidentHelmetDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_HELMET> GetView(HisAccidentHelmetSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_HELMET> result = new List<V_HIS_ACCIDENT_HELMET>();
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

        public V_HIS_ACCIDENT_HELMET GetViewById(long id, HisAccidentHelmetSO search)
        {
            V_HIS_ACCIDENT_HELMET result = null;

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
