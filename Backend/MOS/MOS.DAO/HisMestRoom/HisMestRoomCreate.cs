using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestRoom
{
    partial class HisMestRoomCreate : EntityBase
    {
        public HisMestRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_ROOM>();
        }

        private BridgeDAO<HIS_MEST_ROOM> bridgeDAO;

        public bool Create(HIS_MEST_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
