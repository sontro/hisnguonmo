using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceGroup
{
    partial class HisServiceGroupUpdate : EntityBase
    {
        public HisServiceGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_GROUP>();
        }

        private BridgeDAO<HIS_SERVICE_GROUP> bridgeDAO;

        public bool Update(HIS_SERVICE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
