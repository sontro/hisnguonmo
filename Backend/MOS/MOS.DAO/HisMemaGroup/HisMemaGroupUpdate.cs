using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMemaGroup
{
    partial class HisMemaGroupUpdate : EntityBase
    {
        public HisMemaGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEMA_GROUP>();
        }

        private BridgeDAO<HIS_MEMA_GROUP> bridgeDAO;

        public bool Update(HIS_MEMA_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEMA_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
