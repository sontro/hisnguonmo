using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoTruncate : EntityBase
    {
        public HisSevereIllnessInfoTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SEVERE_ILLNESS_INFO>();
        }

        private BridgeDAO<HIS_SEVERE_ILLNESS_INFO> bridgeDAO;

        public bool Truncate(HIS_SEVERE_ILLNESS_INFO data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
