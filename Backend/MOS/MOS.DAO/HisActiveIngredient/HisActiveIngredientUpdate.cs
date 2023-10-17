using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisActiveIngredient
{
    partial class HisActiveIngredientUpdate : EntityBase
    {
        public HisActiveIngredientUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACTIVE_INGREDIENT>();
        }

        private BridgeDAO<HIS_ACTIVE_INGREDIENT> bridgeDAO;

        public bool Update(HIS_ACTIVE_INGREDIENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACTIVE_INGREDIENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
