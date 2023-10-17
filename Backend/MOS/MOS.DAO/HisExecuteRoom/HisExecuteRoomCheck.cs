using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExecuteRoom
{
    partial class HisExecuteRoomCheck : EntityBase
    {
        public HisExecuteRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROOM>();
        }

        private BridgeDAO<HIS_EXECUTE_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
