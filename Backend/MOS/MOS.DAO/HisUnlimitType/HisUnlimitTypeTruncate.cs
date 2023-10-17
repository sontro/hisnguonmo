using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUnlimitType
{
    partial class HisUnlimitTypeTruncate : EntityBase
    {
        public HisUnlimitTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_TYPE>();
        }

        private BridgeDAO<HIS_UNLIMIT_TYPE> bridgeDAO;

        public bool Truncate(HIS_UNLIMIT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_UNLIMIT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
