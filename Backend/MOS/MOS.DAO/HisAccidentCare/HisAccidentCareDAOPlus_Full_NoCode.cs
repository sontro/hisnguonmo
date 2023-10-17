using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentCare
{
    public partial class HisAccidentCareDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_CARE> GetView(HisAccidentCareSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_CARE> result = new List<V_HIS_ACCIDENT_CARE>();
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

        public V_HIS_ACCIDENT_CARE GetViewById(long id, HisAccidentCareSO search)
        {
            V_HIS_ACCIDENT_CARE result = null;

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
