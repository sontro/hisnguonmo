using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSevereIllnessInfo
{
    public partial class HisSevereIllnessInfoDAO : EntityBase
    {
        public List<V_HIS_SEVERE_ILLNESS_INFO> GetView(HisSevereIllnessInfoSO search, CommonParam param)
        {
            List<V_HIS_SEVERE_ILLNESS_INFO> result = new List<V_HIS_SEVERE_ILLNESS_INFO>();
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

        public V_HIS_SEVERE_ILLNESS_INFO GetViewById(long id, HisSevereIllnessInfoSO search)
        {
            V_HIS_SEVERE_ILLNESS_INFO result = null;

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
