using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareType
{
    partial class HisCareTypeTruncate : EntityBase
    {
        public HisCareTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TYPE>();
        }

        private BridgeDAO<HIS_CARE_TYPE> bridgeDAO;

        public bool Truncate(HIS_CARE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
