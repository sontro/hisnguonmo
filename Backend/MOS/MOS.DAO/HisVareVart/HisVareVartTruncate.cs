using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVareVart
{
    partial class HisVareVartTruncate : EntityBase
    {
        public HisVareVartTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VARE_VART>();
        }

        private BridgeDAO<HIS_VARE_VART> bridgeDAO;

        public bool Truncate(HIS_VARE_VART data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VARE_VART> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
