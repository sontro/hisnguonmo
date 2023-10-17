using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAwareness
{
    partial class HisAwarenessUpdate : EntityBase
    {
        public HisAwarenessUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AWARENESS>();
        }

        private BridgeDAO<HIS_AWARENESS> bridgeDAO;

        public bool Update(HIS_AWARENESS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_AWARENESS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
