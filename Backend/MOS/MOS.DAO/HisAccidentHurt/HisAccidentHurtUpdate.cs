using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurt
{
    partial class HisAccidentHurtUpdate : EntityBase
    {
        public HisAccidentHurtUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT> bridgeDAO;

        public bool Update(HIS_ACCIDENT_HURT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_HURT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
