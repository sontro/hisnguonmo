using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPrepareMaty
{
    partial class HisPrepareMatyTruncate : EntityBase
    {
        public HisPrepareMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_MATY>();
        }

        private BridgeDAO<HIS_PREPARE_MATY> bridgeDAO;

        public bool Truncate(HIS_PREPARE_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PREPARE_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
