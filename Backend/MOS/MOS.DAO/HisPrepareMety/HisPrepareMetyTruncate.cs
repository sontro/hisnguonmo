using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPrepareMety
{
    partial class HisPrepareMetyTruncate : EntityBase
    {
        public HisPrepareMetyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_METY>();
        }

        private BridgeDAO<HIS_PREPARE_METY> bridgeDAO;

        public bool Truncate(HIS_PREPARE_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PREPARE_METY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
