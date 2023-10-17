using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserGroupTemp
{
    partial class HisUserGroupTempTruncate : EntityBase
    {
        public HisUserGroupTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP> bridgeDAO;

        public bool Truncate(HIS_USER_GROUP_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_USER_GROUP_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
