using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRoom
{
    partial class HisExecuteRoomCreate : EntityBase
    {
        public HisExecuteRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROOM>();
        }

        private BridgeDAO<HIS_EXECUTE_ROOM> bridgeDAO;

        public bool Create(HIS_EXECUTE_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXECUTE_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
