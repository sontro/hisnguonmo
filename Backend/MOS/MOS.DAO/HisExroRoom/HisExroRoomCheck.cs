using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExroRoom
{
    partial class HisExroRoomCheck : EntityBase
    {
        public HisExroRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXRO_ROOM>();
        }

        private BridgeDAO<HIS_EXRO_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
