using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpUserTemp
{
    partial class HisImpUserTempTruncate : EntityBase
    {
        public HisImpUserTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP> bridgeDAO;

        public bool Truncate(HIS_IMP_USER_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_USER_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
