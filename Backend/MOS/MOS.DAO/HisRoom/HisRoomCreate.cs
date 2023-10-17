using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRoom
{
    partial class HisRoomCreate : EntityBase
    {
        public HisRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ROOM>();
        }

        private BridgeDAO<HIS_ROOM> bridgeDAO;

        public bool Create(HIS_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
