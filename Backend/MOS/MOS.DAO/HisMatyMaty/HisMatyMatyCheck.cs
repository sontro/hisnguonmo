using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMatyMaty
{
    partial class HisMatyMatyCheck : EntityBase
    {
        public HisMatyMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATY_MATY>();
        }

        private BridgeDAO<HIS_MATY_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
