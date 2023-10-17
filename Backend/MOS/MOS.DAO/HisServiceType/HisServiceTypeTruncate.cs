using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceType
{
    partial class HisServiceTypeTruncate : EntityBase
    {
        public HisServiceTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_TYPE>();
        }

        private BridgeDAO<HIS_SERVICE_TYPE> bridgeDAO;

        public bool Truncate(HIS_SERVICE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
