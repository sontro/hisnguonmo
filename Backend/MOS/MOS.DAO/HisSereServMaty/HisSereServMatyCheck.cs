using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServMaty
{
    partial class HisSereServMatyCheck : EntityBase
    {
        public HisSereServMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_MATY>();
        }

        private BridgeDAO<HIS_SERE_SERV_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
