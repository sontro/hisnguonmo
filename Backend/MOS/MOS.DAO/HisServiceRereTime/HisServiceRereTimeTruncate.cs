using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceRereTime
{
    partial class HisServiceRereTimeTruncate : EntityBase
    {
        public HisServiceRereTimeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RERE_TIME>();
        }

        private BridgeDAO<HIS_SERVICE_RERE_TIME> bridgeDAO;

        public bool Truncate(HIS_SERVICE_RERE_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_RERE_TIME> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
