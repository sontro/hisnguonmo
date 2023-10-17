using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAgeType
{
    public partial class HisAgeTypeDAO : EntityBase
    {
        public List<V_HIS_AGE_TYPE> GetView(HisAgeTypeSO search, CommonParam param)
        {
            List<V_HIS_AGE_TYPE> result = new List<V_HIS_AGE_TYPE>();
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

        public V_HIS_AGE_TYPE GetViewById(long id, HisAgeTypeSO search)
        {
            V_HIS_AGE_TYPE result = null;

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
