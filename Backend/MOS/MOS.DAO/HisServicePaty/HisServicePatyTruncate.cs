using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServicePaty
{
    partial class HisServicePatyTruncate : EntityBase
    {
        public HisServicePatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PATY>();
        }

        private BridgeDAO<HIS_SERVICE_PATY> bridgeDAO;

        public bool Truncate(HIS_SERVICE_PATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_PATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
