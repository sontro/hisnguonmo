using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipTemp
{
    partial class HisEkipTempTruncate : EntityBase
    {
        public HisEkipTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_TEMP>();
        }

        private BridgeDAO<HIS_EKIP_TEMP> bridgeDAO;

        public bool Truncate(HIS_EKIP_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EKIP_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
