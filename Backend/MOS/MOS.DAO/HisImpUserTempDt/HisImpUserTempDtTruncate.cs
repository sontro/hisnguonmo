using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpUserTempDt
{
    partial class HisImpUserTempDtTruncate : EntityBase
    {
        public HisImpUserTempDtTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP_DT>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP_DT> bridgeDAO;

        public bool Truncate(HIS_IMP_USER_TEMP_DT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_USER_TEMP_DT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
