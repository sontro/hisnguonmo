using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStentConclude
{
    partial class HisStentConcludeTruncate : EntityBase
    {
        public HisStentConcludeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STENT_CONCLUDE>();
        }

        private BridgeDAO<HIS_STENT_CONCLUDE> bridgeDAO;

        public bool Truncate(HIS_STENT_CONCLUDE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_STENT_CONCLUDE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
