using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCareType
{
    public partial class HisCareTypeDAO : EntityBase
    {
        public List<V_HIS_CARE_TYPE> GetView(HisCareTypeSO search, CommonParam param)
        {
            List<V_HIS_CARE_TYPE> result = new List<V_HIS_CARE_TYPE>();
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

        public V_HIS_CARE_TYPE GetViewById(long id, HisCareTypeSO search)
        {
            V_HIS_CARE_TYPE result = null;

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
