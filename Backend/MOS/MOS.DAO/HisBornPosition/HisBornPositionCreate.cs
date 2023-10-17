using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBornPosition
{
    partial class HisBornPositionCreate : EntityBase
    {
        public HisBornPositionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_POSITION>();
        }

        private BridgeDAO<HIS_BORN_POSITION> bridgeDAO;

        public bool Create(HIS_BORN_POSITION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BORN_POSITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
