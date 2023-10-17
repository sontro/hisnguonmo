using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExroRoom
{
    partial class HisExroRoomCreate : EntityBase
    {
        public HisExroRoomCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXRO_ROOM>();
        }

        private BridgeDAO<HIS_EXRO_ROOM> bridgeDAO;

        public bool Create(HIS_EXRO_ROOM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXRO_ROOM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
