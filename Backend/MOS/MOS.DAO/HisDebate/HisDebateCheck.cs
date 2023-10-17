using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDebate
{
    partial class HisDebateCheck : EntityBase
    {
        public HisDebateCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE>();
        }

        private BridgeDAO<HIS_DEBATE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
