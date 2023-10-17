using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntigen
{
    public partial class HisAntigenDAO : EntityBase
    {
        public List<V_HIS_ANTIGEN> GetView(HisAntigenSO search, CommonParam param)
        {
            List<V_HIS_ANTIGEN> result = new List<V_HIS_ANTIGEN>();
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

        public V_HIS_ANTIGEN GetViewById(long id, HisAntigenSO search)
        {
            V_HIS_ANTIGEN result = null;

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
