using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceRati
{
    partial class HisServiceRatiUpdate : EntityBase
    {
        public HisServiceRatiUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RATI>();
        }

        private BridgeDAO<HIS_SERVICE_RATI> bridgeDAO;

        public bool Update(HIS_SERVICE_RATI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_RATI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
