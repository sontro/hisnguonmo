using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAnticipateBlty
{
    partial class HisAnticipateBltyTruncate : EntityBase
    {
        public HisAnticipateBltyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTICIPATE_BLTY>();
        }

        private BridgeDAO<HIS_ANTICIPATE_BLTY> bridgeDAO;

        public bool Truncate(HIS_ANTICIPATE_BLTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
