using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRemuneration
{
    partial class HisRemunerationCheck : EntityBase
    {
        public HisRemunerationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REMUNERATION>();
        }

        private BridgeDAO<HIS_REMUNERATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
