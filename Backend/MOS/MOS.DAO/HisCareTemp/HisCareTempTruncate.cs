using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareTemp
{
    partial class HisCareTempTruncate : EntityBase
    {
        public HisCareTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TEMP>();
        }

        private BridgeDAO<HIS_CARE_TEMP> bridgeDAO;

        public bool Truncate(HIS_CARE_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARE_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
