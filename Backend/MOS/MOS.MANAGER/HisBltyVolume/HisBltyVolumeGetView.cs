using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyVolume
{
    partial class HisBltyVolumeGet : BusinessBase
    {
        internal List<V_HIS_BLTY_VOLUME> GetView(HisBltyVolumeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBltyVolumeDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
