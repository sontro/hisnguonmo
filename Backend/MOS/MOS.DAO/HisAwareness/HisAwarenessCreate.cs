using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAwareness
{
    partial class HisAwarenessCreate : EntityBase
    {
        public HisAwarenessCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AWARENESS>();
        }

        private BridgeDAO<HIS_AWARENESS> bridgeDAO;

        public bool Create(HIS_AWARENESS data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_AWARENESS> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
