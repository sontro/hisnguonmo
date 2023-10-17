using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreDhty
{
    partial class HisHoreDhtyTruncate : EntityBase
    {
        public HisHoreDhtyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_DHTY>();
        }

        private BridgeDAO<HIS_HORE_DHTY> bridgeDAO;

        public bool Truncate(HIS_HORE_DHTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HORE_DHTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
