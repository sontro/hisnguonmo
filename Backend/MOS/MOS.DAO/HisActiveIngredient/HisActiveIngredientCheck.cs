using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;

namespace MOS.DAO.HisActiveIngredient
{
    partial class HisActiveIngredientCheck : EntityBase
    {
        public HisActiveIngredientCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACTIVE_INGREDIENT>();
        }

        private BridgeDAO<HIS_ACTIVE_INGREDIENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
