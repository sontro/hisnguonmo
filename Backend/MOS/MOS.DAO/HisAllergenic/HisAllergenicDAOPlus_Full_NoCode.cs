using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAllergenic
{
    public partial class HisAllergenicDAO : EntityBase
    {
        public List<V_HIS_ALLERGENIC> GetView(HisAllergenicSO search, CommonParam param)
        {
            List<V_HIS_ALLERGENIC> result = new List<V_HIS_ALLERGENIC>();
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

        public V_HIS_ALLERGENIC GetViewById(long id, HisAllergenicSO search)
        {
            V_HIS_ALLERGENIC result = null;

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
