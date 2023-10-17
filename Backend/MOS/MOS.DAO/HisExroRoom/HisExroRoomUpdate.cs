using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExroRoom
{
    partial class HisExroRoomUpdate : EntityBase
    {
        public HisExroRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXRO_ROOM>();
        }

        private BridgeDAO<HIS_EXRO_ROOM> bridgeDAO;

        public bool Update(HIS_EXRO_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXRO_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
