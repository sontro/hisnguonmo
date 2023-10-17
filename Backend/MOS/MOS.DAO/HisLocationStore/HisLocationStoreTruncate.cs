using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisLocationStore
{
    partial class HisLocationStoreTruncate : EntityBase
    {
        public HisLocationStoreTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LOCATION_STORE>();
        }

        private BridgeDAO<HIS_LOCATION_STORE> bridgeDAO;

        public bool Truncate(HIS_LOCATION_STORE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_LOCATION_STORE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
