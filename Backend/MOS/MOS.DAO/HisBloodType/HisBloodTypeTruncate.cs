using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodType
{
    partial class HisBloodTypeTruncate : EntityBase
    {
        public HisBloodTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_TYPE>();
        }

        private BridgeDAO<HIS_BLOOD_TYPE> bridgeDAO;

        public bool Truncate(HIS_BLOOD_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
