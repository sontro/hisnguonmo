using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBltyVolume
{
    public partial class HisBltyVolumeDAO : EntityBase
    {
        public List<V_HIS_BLTY_VOLUME> GetView(HisBltyVolumeSO search, CommonParam param)
        {
            List<V_HIS_BLTY_VOLUME> result = new List<V_HIS_BLTY_VOLUME>();
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

        public V_HIS_BLTY_VOLUME GetViewById(long id, HisBltyVolumeSO search)
        {
            V_HIS_BLTY_VOLUME result = null;

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
