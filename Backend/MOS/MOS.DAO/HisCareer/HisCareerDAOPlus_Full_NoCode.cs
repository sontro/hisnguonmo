using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCareer
{
    public partial class HisCareerDAO : EntityBase
    {
        public List<V_HIS_CAREER> GetView(HisCareerSO search, CommonParam param)
        {
            List<V_HIS_CAREER> result = new List<V_HIS_CAREER>();
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

        public V_HIS_CAREER GetViewById(long id, HisCareerSO search)
        {
            V_HIS_CAREER result = null;

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
