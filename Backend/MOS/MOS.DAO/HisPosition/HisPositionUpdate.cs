using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPosition
{
    partial class HisPositionUpdate : EntityBase
    {
        public HisPositionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_POSITION>();
        }

        private BridgeDAO<HIS_POSITION> bridgeDAO;

        public bool Update(HIS_POSITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_POSITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
