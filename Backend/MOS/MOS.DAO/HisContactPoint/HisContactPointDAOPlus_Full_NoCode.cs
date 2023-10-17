using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisContactPoint
{
    public partial class HisContactPointDAO : EntityBase
    {
        public List<V_HIS_CONTACT_POINT> GetView(HisContactPointSO search, CommonParam param)
        {
            List<V_HIS_CONTACT_POINT> result = new List<V_HIS_CONTACT_POINT>();
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

        public V_HIS_CONTACT_POINT GetViewById(long id, HisContactPointSO search)
        {
            V_HIS_CONTACT_POINT result = null;

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
