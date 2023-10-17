using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBornPosition
{
    partial class HisBornPositionUpdate : EntityBase
    {
        public HisBornPositionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_POSITION>();
        }

        private BridgeDAO<HIS_BORN_POSITION> bridgeDAO;

        public bool Update(HIS_BORN_POSITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BORN_POSITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
