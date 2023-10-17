using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodVolume
{
    public partial class HisBloodVolumeDAO : EntityBase
    {
        public List<V_HIS_BLOOD_VOLUME> GetView(HisBloodVolumeSO search, CommonParam param)
        {
            List<V_HIS_BLOOD_VOLUME> result = new List<V_HIS_BLOOD_VOLUME>();
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

        public V_HIS_BLOOD_VOLUME GetViewById(long id, HisBloodVolumeSO search)
        {
            V_HIS_BLOOD_VOLUME result = null;

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
