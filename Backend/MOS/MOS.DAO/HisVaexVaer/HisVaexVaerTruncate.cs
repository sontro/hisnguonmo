using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaexVaer
{
    partial class HisVaexVaerTruncate : EntityBase
    {
        public HisVaexVaerTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VAEX_VAER>();
        }

        private BridgeDAO<HIS_VAEX_VAER> bridgeDAO;

        public bool Truncate(HIS_VAEX_VAER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VAEX_VAER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
