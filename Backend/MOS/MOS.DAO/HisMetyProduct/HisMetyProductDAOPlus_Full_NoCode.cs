using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyProduct
{
    public partial class HisMetyProductDAO : EntityBase
    {
        public List<V_HIS_METY_PRODUCT> GetView(HisMetyProductSO search, CommonParam param)
        {
            List<V_HIS_METY_PRODUCT> result = new List<V_HIS_METY_PRODUCT>();
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

        public V_HIS_METY_PRODUCT GetViewById(long id, HisMetyProductSO search)
        {
            V_HIS_METY_PRODUCT result = null;

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
