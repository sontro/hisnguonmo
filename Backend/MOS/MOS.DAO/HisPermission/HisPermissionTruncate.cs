using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPermission
{
    partial class HisPermissionTruncate : EntityBase
    {
        public HisPermissionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERMISSION>();
        }

        private BridgeDAO<HIS_PERMISSION> bridgeDAO;

        public bool Truncate(HIS_PERMISSION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PERMISSION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
