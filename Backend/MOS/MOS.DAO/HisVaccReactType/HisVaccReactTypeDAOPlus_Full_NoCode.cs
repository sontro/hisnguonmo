using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccReactType
{
    public partial class HisVaccReactTypeDAO : EntityBase
    {
        public List<V_HIS_VACC_REACT_TYPE> GetView(HisVaccReactTypeSO search, CommonParam param)
        {
            List<V_HIS_VACC_REACT_TYPE> result = new List<V_HIS_VACC_REACT_TYPE>();
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

        public V_HIS_VACC_REACT_TYPE GetViewById(long id, HisVaccReactTypeSO search)
        {
            V_HIS_VACC_REACT_TYPE result = null;

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
