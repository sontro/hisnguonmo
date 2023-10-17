using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPaanPosition
{
    partial class HisPaanPositionTruncate : EntityBase
    {
        public HisPaanPositionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_POSITION>();
        }

        private BridgeDAO<HIS_PAAN_POSITION> bridgeDAO;

        public bool Truncate(HIS_PAAN_POSITION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PAAN_POSITION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
