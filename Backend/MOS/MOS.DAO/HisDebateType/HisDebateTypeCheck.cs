using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebateType
{
    partial class HisDebateTypeCheck : EntityBase
    {
        public HisDebateTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TYPE>();
        }

        private BridgeDAO<HIS_DEBATE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
