using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisReceptionRoom
{
    partial class HisReceptionRoomCreate : EntityBase
    {
        public HisReceptionRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RECEPTION_ROOM>();
        }

        private BridgeDAO<HIS_RECEPTION_ROOM> bridgeDAO;

        public bool Create(HIS_RECEPTION_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_RECEPTION_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
