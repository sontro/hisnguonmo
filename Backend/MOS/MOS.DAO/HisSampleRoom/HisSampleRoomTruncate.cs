using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSampleRoom
{
    partial class HisSampleRoomTruncate : EntityBase
    {
        public HisSampleRoomTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SAMPLE_ROOM>();
        }

        private BridgeDAO<HIS_SAMPLE_ROOM> bridgeDAO;

        public bool Truncate(HIS_SAMPLE_ROOM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SAMPLE_ROOM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
