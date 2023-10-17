using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisUserRoom
{
    partial class HisUserRoomCreate : EntityBase
    {
        public HisUserRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_ROOM>();
        }

        private BridgeDAO<HIS_USER_ROOM> bridgeDAO;

        public bool Create(HIS_USER_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_USER_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
