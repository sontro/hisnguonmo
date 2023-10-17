using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOccupational
{
    partial class HisKskOccupationalTruncate : EntityBase
    {
        public HisKskOccupationalTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OCCUPATIONAL>();
        }

        private BridgeDAO<HIS_KSK_OCCUPATIONAL> bridgeDAO;

        public bool Truncate(HIS_KSK_OCCUPATIONAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_OCCUPATIONAL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
