using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSampleRoom
{
    partial class HisSampleRoomUpdate : EntityBase
    {
        public HisSampleRoomUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SAMPLE_ROOM>();
        }

        private BridgeDAO<HIS_SAMPLE_ROOM> bridgeDAO;

        public bool Update(HIS_SAMPLE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SAMPLE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
