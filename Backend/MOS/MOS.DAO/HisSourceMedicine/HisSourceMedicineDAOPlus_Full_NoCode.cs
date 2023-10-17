using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSourceMedicine
{
    public partial class HisSourceMedicineDAO : EntityBase
    {
        public List<V_HIS_SOURCE_MEDICINE> GetView(HisSourceMedicineSO search, CommonParam param)
        {
            List<V_HIS_SOURCE_MEDICINE> result = new List<V_HIS_SOURCE_MEDICINE>();
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

        public V_HIS_SOURCE_MEDICINE GetViewById(long id, HisSourceMedicineSO search)
        {
            V_HIS_SOURCE_MEDICINE result = null;

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
