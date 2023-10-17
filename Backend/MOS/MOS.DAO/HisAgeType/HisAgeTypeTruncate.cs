using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAgeType
{
    partial class HisAgeTypeTruncate : EntityBase
    {
        public HisAgeTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AGE_TYPE>();
        }

        private BridgeDAO<HIS_AGE_TYPE> bridgeDAO;

        public bool Truncate(HIS_AGE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_AGE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
