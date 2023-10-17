using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskDriver
{
    partial class HisKskDriverTruncate : EntityBase
    {
        public HisKskDriverTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_DRIVER> bridgeDAO;

        public bool Truncate(HIS_KSK_DRIVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_DRIVER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
