using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBltyVolume
{
    partial class HisBltyVolumeTruncate : EntityBase
    {
        public HisBltyVolumeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_VOLUME>();
        }

        private BridgeDAO<HIS_BLTY_VOLUME> bridgeDAO;

        public bool Truncate(HIS_BLTY_VOLUME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLTY_VOLUME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
