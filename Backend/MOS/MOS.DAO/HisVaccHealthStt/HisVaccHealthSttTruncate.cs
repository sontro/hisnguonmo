using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccHealthStt
{
    partial class HisVaccHealthSttTruncate : EntityBase
    {
        public HisVaccHealthSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_HEALTH_STT>();
        }

        private BridgeDAO<HIS_VACC_HEALTH_STT> bridgeDAO;

        public bool Truncate(HIS_VACC_HEALTH_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VACC_HEALTH_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
