using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAllergyCard
{
    partial class HisAllergyCardUpdate : EntityBase
    {
        public HisAllergyCardUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGY_CARD>();
        }

        private BridgeDAO<HIS_ALLERGY_CARD> bridgeDAO;

        public bool Update(HIS_ALLERGY_CARD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ALLERGY_CARD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
