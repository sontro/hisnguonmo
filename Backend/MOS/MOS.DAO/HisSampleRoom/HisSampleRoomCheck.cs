using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSampleRoom
{
    partial class HisSampleRoomCheck : EntityBase
    {
        public HisSampleRoomCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SAMPLE_ROOM>();
        }

        private BridgeDAO<HIS_SAMPLE_ROOM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
