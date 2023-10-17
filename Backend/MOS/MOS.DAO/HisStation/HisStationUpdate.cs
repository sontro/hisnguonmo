using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStation
{
    partial class HisStationUpdate : EntityBase
    {
        public HisStationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STATION>();
        }

        private BridgeDAO<HIS_STATION> bridgeDAO;

        public bool Update(HIS_STATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_STATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
