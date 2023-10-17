using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFuexType
{
    partial class HisFuexTypeTruncate : EntityBase
    {
        public HisFuexTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUEX_TYPE>();
        }

        private BridgeDAO<HIS_FUEX_TYPE> bridgeDAO;

        public bool Truncate(HIS_FUEX_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FUEX_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
