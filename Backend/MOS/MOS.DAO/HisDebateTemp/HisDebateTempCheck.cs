using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebateTemp
{
    partial class HisDebateTempCheck : EntityBase
    {
        public HisDebateTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TEMP>();
        }

        private BridgeDAO<HIS_DEBATE_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
