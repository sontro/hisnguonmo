using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineService
{
    public partial class HisMedicineServiceDAO : EntityBase
    {
        public List<V_HIS_MEDICINE_SERVICE> GetView(HisMedicineServiceSO search, CommonParam param)
        {
            List<V_HIS_MEDICINE_SERVICE> result = new List<V_HIS_MEDICINE_SERVICE>();
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

        public V_HIS_MEDICINE_SERVICE GetViewById(long id, HisMedicineServiceSO search)
        {
            V_HIS_MEDICINE_SERVICE result = null;

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
