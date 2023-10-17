using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceRereTime
{
    partial class HisServiceRereTimeUpdate : EntityBase
    {
        public HisServiceRereTimeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RERE_TIME>();
        }

        private BridgeDAO<HIS_SERVICE_RERE_TIME> bridgeDAO;

        public bool Update(HIS_SERVICE_RERE_TIME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_RERE_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
