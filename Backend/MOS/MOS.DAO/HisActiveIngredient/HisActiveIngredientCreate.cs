using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisActiveIngredient
{
    partial class HisActiveIngredientCreate : EntityBase
    {
        public HisActiveIngredientCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACTIVE_INGREDIENT>();
        }

        private BridgeDAO<HIS_ACTIVE_INGREDIENT> bridgeDAO;

        public bool Create(HIS_ACTIVE_INGREDIENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACTIVE_INGREDIENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
