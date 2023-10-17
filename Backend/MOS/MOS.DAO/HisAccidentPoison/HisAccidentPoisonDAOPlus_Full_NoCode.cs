using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentPoison
{
    public partial class HisAccidentPoisonDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_POISON> GetView(HisAccidentPoisonSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_POISON> result = new List<V_HIS_ACCIDENT_POISON>();
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

        public V_HIS_ACCIDENT_POISON GetViewById(long id, HisAccidentPoisonSO search)
        {
            V_HIS_ACCIDENT_POISON result = null;

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
