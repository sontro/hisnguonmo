using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBltyVolume
{
    partial class HisBltyVolumeUpdate : EntityBase
    {
        public HisBltyVolumeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_VOLUME>();
        }

        private BridgeDAO<HIS_BLTY_VOLUME> bridgeDAO;

        public bool Update(HIS_BLTY_VOLUME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLTY_VOLUME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
