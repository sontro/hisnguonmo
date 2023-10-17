using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDesk
{
    partial class HisDeskUpdate : EntityBase
    {
        public HisDeskUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DESK>();
        }

        private BridgeDAO<HIS_DESK> bridgeDAO;

        public bool Update(HIS_DESK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DESK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
