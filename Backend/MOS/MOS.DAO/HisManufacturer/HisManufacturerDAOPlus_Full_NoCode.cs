using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisManufacturer
{
    public partial class HisManufacturerDAO : EntityBase
    {
        public List<V_HIS_MANUFACTURER> GetView(HisManufacturerSO search, CommonParam param)
        {
            List<V_HIS_MANUFACTURER> result = new List<V_HIS_MANUFACTURER>();
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

        public V_HIS_MANUFACTURER GetViewById(long id, HisManufacturerSO search)
        {
            V_HIS_MANUFACTURER result = null;

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
