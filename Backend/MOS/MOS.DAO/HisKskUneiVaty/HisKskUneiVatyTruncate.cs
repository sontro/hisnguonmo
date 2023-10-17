using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskUneiVaty
{
    partial class HisKskUneiVatyTruncate : EntityBase
    {
        public HisKskUneiVatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNEI_VATY>();
        }

        private BridgeDAO<HIS_KSK_UNEI_VATY> bridgeDAO;

        public bool Truncate(HIS_KSK_UNEI_VATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_UNEI_VATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
