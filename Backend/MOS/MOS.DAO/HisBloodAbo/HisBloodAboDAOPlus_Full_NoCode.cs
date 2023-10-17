using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodAbo
{
    public partial class HisBloodAboDAO : EntityBase
    {
        public List<V_HIS_BLOOD_ABO> GetView(HisBloodAboSO search, CommonParam param)
        {
            List<V_HIS_BLOOD_ABO> result = new List<V_HIS_BLOOD_ABO>();
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

        public V_HIS_BLOOD_ABO GetViewById(long id, HisBloodAboSO search)
        {
            V_HIS_BLOOD_ABO result = null;

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
