using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPosition
{
    partial class HisPositionCreate : EntityBase
    {
        public HisPositionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_POSITION>();
        }

        private BridgeDAO<HIS_POSITION> bridgeDAO;

        public bool Create(HIS_POSITION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_POSITION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
