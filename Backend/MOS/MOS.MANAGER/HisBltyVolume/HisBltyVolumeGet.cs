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
        internal HisBltyVolumeGet()
            : base()
        {

        }

        internal HisBltyVolumeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BLTY_VOLUME> Get(HisBltyVolumeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBltyVolumeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLTY_VOLUME GetById(long id)
        {
            try
            {
                return GetById(id, new HisBltyVolumeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLTY_VOLUME GetById(long id, HisBltyVolumeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBltyVolumeDAO.GetById(id, filter.Query());
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
