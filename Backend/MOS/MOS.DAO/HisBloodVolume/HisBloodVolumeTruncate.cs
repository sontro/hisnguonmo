using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodVolume
{
    partial class HisBloodVolumeTruncate : EntityBase
    {
        public HisBloodVolumeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_VOLUME>();
        }

        private BridgeDAO<HIS_BLOOD_VOLUME> bridgeDAO;

        public bool Truncate(HIS_BLOOD_VOLUME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD_VOLUME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
