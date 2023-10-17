using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisActiveIngredient
{
    partial class HisActiveIngredientTruncate : EntityBase
    {
        public HisActiveIngredientTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACTIVE_INGREDIENT>();
        }

        private BridgeDAO<HIS_ACTIVE_INGREDIENT> bridgeDAO;

        public bool Truncate(HIS_ACTIVE_INGREDIENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACTIVE_INGREDIENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
