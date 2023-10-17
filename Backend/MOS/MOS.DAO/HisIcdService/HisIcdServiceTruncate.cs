using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdService
{
    partial class HisIcdServiceTruncate : EntityBase
    {
        public HisIcdServiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_SERVICE>();
        }

        private BridgeDAO<HIS_ICD_SERVICE> bridgeDAO;

        public bool Truncate(HIS_ICD_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ICD_SERVICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
