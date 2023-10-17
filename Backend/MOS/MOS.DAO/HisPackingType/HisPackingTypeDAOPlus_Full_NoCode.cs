using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPackingType
{
    public partial class HisPackingTypeDAO : EntityBase
    {
        public List<V_HIS_PACKING_TYPE> GetView(HisPackingTypeSO search, CommonParam param)
        {
            List<V_HIS_PACKING_TYPE> result = new List<V_HIS_PACKING_TYPE>();
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

        public V_HIS_PACKING_TYPE GetViewById(long id, HisPackingTypeSO search)
        {
            V_HIS_PACKING_TYPE result = null;

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
