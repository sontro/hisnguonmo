using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisStentConclude
{
    partial class HisStentConcludeCheck : EntityBase
    {
        public HisStentConcludeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STENT_CONCLUDE>();
        }

        private BridgeDAO<HIS_STENT_CONCLUDE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
