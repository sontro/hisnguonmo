using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAllergyCard
{
    partial class HisAllergyCardCheck : EntityBase
    {
        public HisAllergyCardCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGY_CARD>();
        }

        private BridgeDAO<HIS_ALLERGY_CARD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
