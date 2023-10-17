using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisOweType
{
    public partial class HisOweTypeDAO : EntityBase
    {
        public List<V_HIS_OWE_TYPE> GetView(HisOweTypeSO search, CommonParam param)
        {
            List<V_HIS_OWE_TYPE> result = new List<V_HIS_OWE_TYPE>();
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

        public V_HIS_OWE_TYPE GetViewById(long id, HisOweTypeSO search)
        {
            V_HIS_OWE_TYPE result = null;

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
