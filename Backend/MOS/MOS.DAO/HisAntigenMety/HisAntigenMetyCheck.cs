using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAntigenMety
{
    partial class HisAntigenMetyCheck : EntityBase
    {
        public HisAntigenMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIGEN_METY>();
        }

        private BridgeDAO<HIS_ANTIGEN_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
