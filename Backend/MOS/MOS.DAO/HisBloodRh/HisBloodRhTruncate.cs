using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodRh
{
    partial class HisBloodRhTruncate : EntityBase
    {
        public HisBloodRhTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_RH>();
        }

        private BridgeDAO<HIS_BLOOD_RH> bridgeDAO;

        public bool Truncate(HIS_BLOOD_RH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD_RH> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
