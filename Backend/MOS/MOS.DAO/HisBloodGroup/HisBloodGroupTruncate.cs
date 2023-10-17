using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodGroup
{
    partial class HisBloodGroupTruncate : EntityBase
    {
        public HisBloodGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GROUP>();
        }

        private BridgeDAO<HIS_BLOOD_GROUP> bridgeDAO;

        public bool Truncate(HIS_BLOOD_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
