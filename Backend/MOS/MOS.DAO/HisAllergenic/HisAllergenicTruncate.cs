using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAllergenic
{
    partial class HisAllergenicTruncate : EntityBase
    {
        public HisAllergenicTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGENIC>();
        }

        private BridgeDAO<HIS_ALLERGENIC> bridgeDAO;

        public bool Truncate(HIS_ALLERGENIC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ALLERGENIC> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
