using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBidType
{
    public partial class HisBidTypeDAO : EntityBase
    {
        public List<V_HIS_BID_TYPE> GetView(HisBidTypeSO search, CommonParam param)
        {
            List<V_HIS_BID_TYPE> result = new List<V_HIS_BID_TYPE>();
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

        public V_HIS_BID_TYPE GetViewById(long id, HisBidTypeSO search)
        {
            V_HIS_BID_TYPE result = null;

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
