using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskService
{
    partial class HisKskServiceTruncate : EntityBase
    {
        public HisKskServiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_SERVICE>();
        }

        private BridgeDAO<HIS_KSK_SERVICE> bridgeDAO;

        public bool Truncate(HIS_KSK_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_SERVICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
