using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPetroleum
{
    partial class HisPetroleumTruncate : EntityBase
    {
        public HisPetroleumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PETROLEUM>();
        }

        private BridgeDAO<HIS_PETROLEUM> bridgeDAO;

        public bool Truncate(HIS_PETROLEUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PETROLEUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
