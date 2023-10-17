using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAtc
{
    partial class HisAtcUpdate : EntityBase
    {
        public HisAtcUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ATC>();
        }

        private BridgeDAO<HIS_ATC> bridgeDAO;

        public bool Update(HIS_ATC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ATC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
