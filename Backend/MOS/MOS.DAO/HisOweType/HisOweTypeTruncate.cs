using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisOweType
{
    partial class HisOweTypeTruncate : EntityBase
    {
        public HisOweTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OWE_TYPE>();
        }

        private BridgeDAO<HIS_OWE_TYPE> bridgeDAO;

        public bool Truncate(HIS_OWE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_OWE_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
