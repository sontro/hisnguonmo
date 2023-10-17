using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisActiveIngredient
{
    public partial class HisActiveIngredientDAO : EntityBase
    {
        public List<V_HIS_ACTIVE_INGREDIENT> GetView(HisActiveIngredientSO search, CommonParam param)
        {
            List<V_HIS_ACTIVE_INGREDIENT> result = new List<V_HIS_ACTIVE_INGREDIENT>();
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

        public V_HIS_ACTIVE_INGREDIENT GetViewById(long id, HisActiveIngredientSO search)
        {
            V_HIS_ACTIVE_INGREDIENT result = null;

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
