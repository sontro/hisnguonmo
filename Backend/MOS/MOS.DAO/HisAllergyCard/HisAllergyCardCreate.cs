using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAllergyCard
{
    partial class HisAllergyCardCreate : EntityBase
    {
        public HisAllergyCardCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGY_CARD>();
        }

        private BridgeDAO<HIS_ALLERGY_CARD> bridgeDAO;

        public bool Create(HIS_ALLERGY_CARD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ALLERGY_CARD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
