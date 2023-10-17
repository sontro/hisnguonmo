using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNoneMediService
{
    partial class HisNoneMediServiceTruncate : EntityBase
    {
        public HisNoneMediServiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NONE_MEDI_SERVICE>();
        }

        private BridgeDAO<HIS_NONE_MEDI_SERVICE> bridgeDAO;

        public bool Truncate(HIS_NONE_MEDI_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_NONE_MEDI_SERVICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
