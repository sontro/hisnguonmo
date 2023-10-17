using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHospitalizeReason
{
    partial class HisHospitalizeReasonTruncate : EntityBase
    {
        public HisHospitalizeReasonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOSPITALIZE_REASON>();
        }

        private BridgeDAO<HIS_HOSPITALIZE_REASON> bridgeDAO;

        public bool Truncate(HIS_HOSPITALIZE_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HOSPITALIZE_REASON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
