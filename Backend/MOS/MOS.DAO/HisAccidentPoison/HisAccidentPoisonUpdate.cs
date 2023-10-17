using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentPoison
{
    partial class HisAccidentPoisonUpdate : EntityBase
    {
        public HisAccidentPoisonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_POISON>();
        }

        private BridgeDAO<HIS_ACCIDENT_POISON> bridgeDAO;

        public bool Update(HIS_ACCIDENT_POISON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_POISON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
