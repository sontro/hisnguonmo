using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBornType
{
    public partial class HisBornTypeDAO : EntityBase
    {
        public List<V_HIS_BORN_TYPE> GetView(HisBornTypeSO search, CommonParam param)
        {
            List<V_HIS_BORN_TYPE> result = new List<V_HIS_BORN_TYPE>();
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

        public V_HIS_BORN_TYPE GetViewById(long id, HisBornTypeSO search)
        {
            V_HIS_BORN_TYPE result = null;

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
