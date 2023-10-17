using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAllergyCard
{
    partial class HisAllergyCardTruncate : EntityBase
    {
        public HisAllergyCardTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGY_CARD>();
        }

        private BridgeDAO<HIS_ALLERGY_CARD> bridgeDAO;

        public bool Truncate(HIS_ALLERGY_CARD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ALLERGY_CARD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
